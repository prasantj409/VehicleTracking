using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;

namespace Test.Repository.RepositoryInerface
{
    public interface IDeviceRepository : IBaseRepository<Device>
    {
        Task<Device> GetDevice(string DeviceNo);
    }
}
