﻿using Domain;
using Domain.Template;
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
        private readonly IUserRepository UserRepository;
        private readonly ITownRepository TownRepository;
        private readonly ITownService TownService;
        private readonly IAppSettings AppSettings;

        public UserService(IUserRepository userRepository, ITownRepository townRepository,
            ITownService townService, IAppSettings appSettings)
        {
            UserRepository = userRepository;
            TownRepository = townRepository;

            TownService = townService;

            AppSettings = appSettings;
        }

        public async Task<User> Authenticate(User user, string password)
        {
            var passwordHasher = new PasswordHasher();
            if (!passwordHasher.VerifyHashedPassword(user.Password, password))
            {
                return null; // invalid password
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
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

            // load town data
            var town = await TownService.GetTown(user.Town.Id);
            user.Town = town;

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<JSONFormatter> CheckAvailability(string email)
        {
            var availability = new JSONFormatter();
            var user = await UserRepository.GetByEmailAsync(email);

            if (user == null)
            {
                availability.AddField("Availability", true);
            }
            else
            {
                availability.AddField("Availability", false);
            }

            return availability;
        }

        public async Task<User> CreateUserWithTown(User user, string townName)
        {
            var userdb = await UserRepository.CreateAsync(user);
            var town = TownTemplate.Template();
            town.Name = townName;
            town.User = userdb;
            town = await TownService.CreateFromTemplate(town);
            userdb.Town = town;
            return userdb;
        }

        public async Task Logout(string token)
        {
            var user = await UserRepository.GetByTokenAsync(token);

            if (user == null)
            {
                return;
            }

            user.Token = null;

            await UserRepository.UpdateAsync(user);

            return;
        }
    }
}