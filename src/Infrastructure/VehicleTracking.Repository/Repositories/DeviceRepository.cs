using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.DB;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;

namespace Test.Repository.Repositories
{
    public class DeviceRepository : BaseRepository<Device>, IDeviceRepository
    {
        private readonly DataContext _context;
        public DeviceRepository(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<Device> GetDevice(string DeviceNo)
        {
            return await _context.Devices
                        .AsNoTracking()
                        .Include(x => x.RoleMapping)
                        .SingleOrDefaultAsync(x => x.DeviceNo == DeviceNo);
        }
    }
}
