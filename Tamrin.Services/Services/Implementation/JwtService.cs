using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tamrin.Common;
using Tamrin.Entities.User;
using Tamrin.Services.Services.Contracts;

namespace Tamrin.Services.Services.Implementation
{
    public class JwtService : IJwtService
    {
        #region Constructor

        private readonly SiteSettings _siteSettings;

        public JwtService(IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _siteSettings = siteSettings.Value;
        }

        #endregion

        public async Task<string> GenerateAsync(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncryptionKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = await GetClaimsAsync(user);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = _siteSettings.JwtSettings.Issuer,
                Audience = _siteSettings.JwtSettings.Audience,
                IssuedAt = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.IssuedAtMinute),
                NotBefore = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.NotBeforeMinute),
                Expires = DateTime.Now.AddMinutes(_siteSettings.JwtSettings.ExpiresMinute),
                EncryptingCredentials = encryptingCredentials,
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(descriptor);
            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;
        }

        private async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
        {
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            //JtwClaimType =>    JwtRegisteredClaimNames.Email
            //DotNetClaimType => ClaimTypes.NameIdentifier
            return new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.Name),
                new Claim(securityStampClaimType,user.SecurityStamp.ToString())
            };
        }
    }
}
