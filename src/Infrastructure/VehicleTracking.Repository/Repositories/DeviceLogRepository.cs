using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.DB;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;
using VehicleTracking.Domain.Entities.Query;

namespace Test.Repository.Repositories
{
    public class DeviceLogRepository : BaseRepository<DeviceLog> , IDeviceLogRepository
    {
        private readonly DataContext _context;
        public DeviceLogRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DeviceLog> GetDeviceLogByID(Guid deviceUUID)
        {
           return await _context.DevicesLog
                .AsNoTracking()
                .Include(x => x.DeviceMapping)
                .Where(x => x.DeviceUUID == deviceUUID)
                .OrderByDescending(x => x.CreatedAt)
                .Take(1).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<DeviceLog>> GetDeviceLogByTime(Guid deviceUUID, VehicleQuery query)
        {
            var result = new List<DeviceLog>().AsQueryable();

            result = _context.DevicesLog
                .AsNoTracking()
                .Include(x => x.DeviceMapping)
                .Where(x => x.DeviceUUID == deviceUUID && (x.CreatedAt >= query.StartTime && x.CreatedAt <= query.EndTime))
                .OrderByDescending(x=>x.CreatedAt);

            if (query.page > 0)
                return await result.Skip((query.page - 1) * query.limit).Take(query.limit).ToListAsync();
            else
                return await result.Take(query.limit).ToListAsync();


        }
    }
}
