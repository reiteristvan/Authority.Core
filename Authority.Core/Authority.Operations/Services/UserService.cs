﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Authority.DomainModel;
using Authority.EntityFramework;
using Authority.Operations.Account;

namespace Authority.Operations.Services
{
    public interface IUserService
    {
        
    }

    public interface IAsyncUserService
    {
        Task<User> RegisterAsync(string email, string username, string password, bool needToActivate = false, Guid domainId = new Guid());
    }

    public sealed class UserService : IUserService, IAsyncUserService
    {
        private readonly IAuthorityContext _context;

        public UserService()
        {
            _context = new AuthorityContext();
        }

        public async Task<User> RegisterAsync(string email, string username, string password, bool needToActivate = false, Guid domainId = new Guid())
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("One or more argument is invalid");
            }

            // it is single domain mode OR the user's domain will be the first one
            if (domainId == Guid.Empty)
            {
                Domain domain = _context.Domains.FirstOrDefault();

                if (domain == null)
                {
                    throw new InvalidOperationException("No domain exists");
                }

                domainId = domain.Id;
            }

            RegisterUser registerOperation = new RegisterUser(_context, domainId, email, username, password, needToActivate);
            User user = await registerOperation.Do();
            await registerOperation.CommitAsync();

            return user;
        }
    }
}
