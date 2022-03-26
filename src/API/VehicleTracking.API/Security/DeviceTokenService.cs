using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Test.Domain.Entities;
using TestAPI.Setting;


namespace TestAPI.Security
{
    public static class DeviceTokenService
    {
        private const double EXPIRE_HOURS = 1.0;
        /// <summary>
        /// Generate bearer token for device using a secret key.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static string CreateToken(Device device)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, device.DeviceNo),
                new Claim(ClaimTypes.Role, device.RoleMapping.Name.ToString()),
                 new Claim(ClaimTypes.Sid, device.UUID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
