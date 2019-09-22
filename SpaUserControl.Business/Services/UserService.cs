using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validation;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Domain.Models;
using System;
using System.Collections.Generic;

namespace SpaUserControl.Business.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _repository;
        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User Authenticate(string email, string password)
        {
            User user = GetByEmail(email);
            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }

        public void ChangeInformation(string email, string name)
        {
            User user = GetByEmail(email);

            user.ChangeName(name);
            user.Validate();

            _repository.Update(user);

        }

        public void ChangePassword(string email, string password, string newPassword, string confirmNewPassword)
        {
            User user = Authenticate(email, password);

            user.SetPassword(newPassword, confirmNewPassword);
            user.Validate();

            _repository.Update(user);
        }


        public User GetByEmail(string email)
        {
            User user = _repository.Get(email);
            if (user == null)
                throw new Exception(Errors.UserNotFound);

            return user;
        }


        public void Register(string name, string email, string password, string confirmPassword)
        {
            var hasUser = GetByEmail(email);
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            User user = new User(name, email);
            user.SetPassword(password, confirmPassword);
            user.Validate();

            _repository.Create(user);
        }

        public string ResetPassword(string email)
        {
            User user = GetByEmail(email);
            string password = user.ResetPassword();
            user.Validate();

            _repository.Update(user);

            return password;
        }

        public List<User> GetByRange(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
