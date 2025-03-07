﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Abstractions.ForRepositories;
using Core.Enums;
using Core.Exceptions;
using Core.Models;
using Infrastructure;

namespace Application
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasherService _passwordHasherService;
        private readonly IJwtProvider _jwtProvider;
        public UserService(IUserRepository userRepository, IPasswordHasherService passwordHasherService, IJwtProvider jwtProvider) 
        {
            _userRepository = userRepository;
            _passwordHasherService = passwordHasherService;
            _jwtProvider = jwtProvider;
        }
        public async Task Register(string username, string email, string password,params Role[] role) 
        {
            try
            {
                var user = User.Create(Guid.NewGuid(), username, _passwordHasherService.Generate(password), email, role);
                if (user.IsFailure)
                {
                    throw new Exception(user.Error);
                }
                await _userRepository.Add(user.Value);
            }
            catch (NotValidEmailException ex)
            {
                throw;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
        public async Task<string> Login(string email, string password) 
        {
            User user = await _userRepository.GetByEmail(email);
            bool IsValidPassword = _passwordHasherService.Verify(password, user.PasswordHash);
            if (IsValidPassword == false) 
            {
                throw new Exception("Failed to login");
            }
            var token = await _jwtProvider.GenerateToken(user);

            return token;

        }
    }
}
