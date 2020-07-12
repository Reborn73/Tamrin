using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tamrin.Common;
using Tamrin.Entities.User;
using Tamrin.Services.Services.Contracts;

namespace Tamrin.Services.Services.Implementation
{
    public class AccessToken
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }

        public AccessToken(JwtSecurityToken securityToken)
        {
            access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            token_type = "Bearer";
            expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }


    public class JwtService : IJwtService, IScopedDependency
    {
        #region Constructor

        private readonly SiteSettings _siteSettings;

        public JwtService(IOptionsSnapshot<SiteSettings> siteSettings)
        {
            _siteSettings = siteSettings.Value;
        }

        #endregion

        public AccessToken Generate(User user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.SecretKey);
            var encryptionKey = Encoding.UTF8.GetBytes(_siteSettings.JwtSettings.EncryptionKey);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = GetClaims(user);

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
            var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);
            //var jwt = tokenHandler.WriteToken(securityToken);

            return new AccessToken(securityToken);
        }

        private IEnumerable<Claim> GetClaims(User user)
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
