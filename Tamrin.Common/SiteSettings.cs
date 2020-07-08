using System;

namespace Tamrin.Common
{
    public class SiteSettings
    {
        public IdentitySettings IdentitySettings { get; set; }
        public JwtSettings JwtSettings { get; set; }
        public EmailSettings EmailSettings { get; set; }
    }

    public class IdentitySettings
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
        public int LockoutMaxFailedAccessAttempts { get; set; }
        public int LockoutDefaultLockoutTimeSpan { get; set; }
        public bool SignInRequireConfirmedEmail { get; set; }
        public bool SignInRequireConfirmedPhoneNumber { get; set; }
        public bool SignInRequireConfirmedAccount { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncryptionKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int IssuedAtMinute { get; set; }
        public int NotBeforeMinute { get; set; }
        public int ExpiresMinute { get; set; }
    }

    public class EmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public bool EnableSsl { get; set; }
    }
}
