﻿using PreferenceCenterAPI.Models;
using System.Text.RegularExpressions;

namespace PreferenceCenterAPI.Services
{
    public class UserService : IUserService
    {
        List<UserPreference> _users;
        public UserService()
        {
            _users = new List<UserPreference>();
        }

        public UserPreference Get(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public UserPreference Get(string email)
        {
            return _users.FirstOrDefault(x => x.Email == email);
        }

        public UserPreference Add(string email)
        {
            if(!IsEmailValid(email))
                throw new FormatException("Email is wrong formated.");

            if (Get(email) != null)
                throw new ArgumentException("email already exist.", nameof(email));

            var newUser = new UserPreference()
            {
                Id = Guid.NewGuid(),
                Email = email,
            };

            _users.Add(newUser);

            return newUser;
        }

        private bool IsEmailValid(string email)
        {
            var emailValidator = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return emailValidator.IsMatch(email);
        }

        public void Delete(Guid id)
        {
            _users.RemoveAll(x => x.Id == id);
        }

        public void Delete(string email)
        {
            _users.RemoveAll(x => x.Email == email);
        }
    }
}