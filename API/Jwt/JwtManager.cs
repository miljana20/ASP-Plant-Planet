﻿using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace API.Jwt
{
    public class JwtManager
    {
        private readonly PlantPlanetContext _context;
        private readonly string _issuer;
        private readonly int _seconds;
        private readonly ITokenStorage _storage;
        private readonly string _secretKey;

        public JwtManager(
            PlantPlanetContext context, 
            string issuer, 
            string secretKey, 
            int seconds,
            ITokenStorage storage)
        {
            _context = context;
            _issuer = issuer;
            _secretKey = secretKey;
            _seconds = seconds;
            _storage = storage;
        }

        public string MakeToken(string email, string password)
        {
            var user = _context.Users
                               .Include(x => x.Role)
                               .ThenInclude(x => x.RoleUseCases)
                               .FirstOrDefault(x => x.Email == email && 
                                                    x.Password == BCrypt.Net.BCrypt.HashPassword(password) && x.IsActive);

            if (user == null || user.Role == null || !user.Role.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            int id = user.Id;
            string username = user.Username;
            List<int> useCases = user.Role.RoleUseCases.Select(x => x.UseCaseId).ToList();

            var tokenId = Guid.NewGuid().ToString();

            _storage.AddToken(tokenId);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, tokenId, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iss, _issuer, ClaimValueTypes.String, _issuer),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64, _issuer),
                new Claim("Id", id.ToString()),
                new Claim("Username", username),
                new Claim("Email", user.Email),
                new Claim("UseCases", JsonConvert.SerializeObject(useCases))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(_seconds),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }


}
