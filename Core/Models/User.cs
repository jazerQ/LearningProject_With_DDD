using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace Core.Models
{
    public class User
    {
        public const int MAX_SIZE_OF_USERNAME = 30;
        private User(Guid id, string username, string passwordHash, string email)
        {
            this.Id = id;
            this.Username = username;
            this.PasswordHash = passwordHash;
            this.Email = email;
        }
        public Guid Id { get; set; }
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public static Result<User> Create(Guid id, string username, string passwordHash, string email) 
        {
            if (string.IsNullOrEmpty(username) || username.Length > MAX_SIZE_OF_USERNAME) 
            {
                return Result.Failure<User>("Your username is empty or very long (max size of usernames is 30)");
            }
            if (string.IsNullOrEmpty(passwordHash)) 
            {
                return Result.Failure<User>("password can`t be empty");
            }
            if (string.IsNullOrEmpty(email))
            {
                return Result.Failure<User>("email can`t be empty");
            }
            var user = new User(id, username, passwordHash, email);

            return Result.Success<User>(user);
        }
    }
}
