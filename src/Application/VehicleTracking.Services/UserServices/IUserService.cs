using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using VehicleTracking.Domain.DTO;

namespace Test.Services.UserServices
{
    public interface IUserService
    {
        Task<User> Get(UserLoginDto dto);
    }
}
