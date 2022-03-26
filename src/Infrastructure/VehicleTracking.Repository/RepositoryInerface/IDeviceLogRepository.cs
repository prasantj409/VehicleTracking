using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using VehicleTracking.Domain.Entities.Query;

namespace Test.Repository.RepositoryInerface
{
    public interface IDeviceLogRepository : IBaseRepository<DeviceLog>
    {
        Task<DeviceLog> GetDeviceLogByID(Guid deviceUUID);
        Task<IEnumerable<DeviceLog>> GetDeviceLogByTime(Guid deviceUUID, VehicleQuery query);
    }
}
