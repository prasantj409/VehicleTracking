using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.DB;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Get(string username, string password)
        {
            
            return  await _context.Users                
                .Include(x=>x.RoleMapping)
                .SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
