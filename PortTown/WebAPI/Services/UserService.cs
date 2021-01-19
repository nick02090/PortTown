using Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly string secret = "P0rtT0wn15Th3835t4ppl1c4t10n3v3rM4d38yHum4n8r41n";

        private readonly IUserRepository UserRepository;

        public UserService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<User> Authenticate(string email, string password)
        {
            var user = await UserRepository.GetByEmailAsync(email);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher();
            if (!passwordHasher.VerifyHashedPassword(user.Password, password))
            {
                return null; // invalid password
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await UserRepository.UpdateAsync(user);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<bool> CheckAvailability(string email)
        {
            throw new NotImplementedException();
        }

        public async Task Logout(string token)
        {
            throw new NotImplementedException();
        }
    }
}