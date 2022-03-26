using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using Test.Repository.RepositoryInerface;
using VehicleTracking.Domain.DTO;

namespace Test.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Get(UserLoginDto dto)
        {
            return await _userRepository.Get(dto.userName, dto.password);
        }
    }
}
 