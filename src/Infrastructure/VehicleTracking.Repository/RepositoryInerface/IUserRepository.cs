using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Repository.RepositoryInerface
{
    public interface IUserRepository
    {
        Task<User> Get(string username, string password);
    }
}
