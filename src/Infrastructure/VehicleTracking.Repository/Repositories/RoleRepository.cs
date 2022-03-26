using System;
using System.Collections.Generic;
using System.Text;
using Test.DB;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;

namespace Test.Repository.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context): base(context)
        {
            _context = context;
        }
        
    }
}
