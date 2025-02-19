﻿using Core.Abstractions.ForServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EFcoreLearningProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id) 
        {
            try
            {
                await _imageService.Delete(id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
