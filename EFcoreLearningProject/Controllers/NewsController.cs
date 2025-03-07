﻿using Core.Abstractions.ForServices;
using Core.DTO;
using Core.Enums;
using Core.Exceptions;
using Core.Models;
using EFcoreLearningProject.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreLearningProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly string _picturesPath = Path.Combine(Directory.GetCurrentDirectory(), "Pictures");
        private readonly INewsService _newsService;
        private readonly IImageService _imageService;
        public NewsController(INewsService newsService, IImageService imageService)
        {
            _newsService = newsService;
            _imageService = imageService;
        }
        [HttpPost]
        [Authorize(Policy = "Create")]
        public async Task<ActionResult> Create([FromForm] RequestNews request)
        {
            if (request == null)
            {
                return BadRequest("request can`t be null");
            }
            var image = await _imageService.CreateImage(request.Image!, _picturesPath, true);
            if (image.IsFailure)
            {
                return BadRequest(image.Error);
            }

            var news = News.Create(_newsService.GetNewId(), request.Title, request.TextData, image.Value, null);

            if (news.IsFailure)
            {
                return BadRequest(news.Error);
            }
            await _newsService.WriteNews(news.Value);
            var responseNews = new ResponseNews
            {
                Title = news.Value.Title,
                TextData = news.Value.TextData,
                Views = news.Value.Views,
                PathOfTitleImage = image.Value.FileName,
                CreatedDate = news.Value.CreatedDate
            };
            return Ok(responseNews);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "Read")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                var news = await _newsService.GetNews(id);
                var fileName = news.TitleImage == null ? "" : news.TitleImage.FileName;

                var responseNews = new ResponseNews
                {
                    Title = news.Title,
                    TextData = news.TextData,
                    Views = news.Views,
                    PathOfTitleImage = fileName,
                    CreatedDate = news.CreatedDate
                };
                return Ok(responseNews);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Policy = "Read")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var listOfNews = await _newsService.GetAllNews();
                List<ResponseNews> responsesNews = listOfNews.Select(lon => new ResponseNews()
                {
                    Title = lon.Title,
                    TextData = lon.TextData,
                    CreatedDate = lon.CreatedDate,
                    Views = lon.Views,
                    PathOfTitleImage = lon.TitleImage!.FileName
                }).ToList();
                return Ok(responsesNews);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Policy = "Delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _newsService.DeleteNews(id);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deleting are invalid, `cause {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id:int}")]
        [Authorize(Policy = "Update")]
        public async Task<ActionResult> Update(int id, [FromForm] RequestNews requestNews)
        {
            try
            {
                var img = await _imageService.CreateImage(requestNews.Image!, _picturesPath, false);
                if (img.IsFailure)
                {
                    return BadRequest(img.Error);
                }
                NewsUpdateDTO news = new NewsUpdateDTO() { Id = id, Title = requestNews.Title, TextData = requestNews.TextData, TitleImage = img.Value };
                await _newsService.UpdateNews(news);

                return Ok();
            }
            catch (ImageCreationException imgc)
            {
                return BadRequest(imgc.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

