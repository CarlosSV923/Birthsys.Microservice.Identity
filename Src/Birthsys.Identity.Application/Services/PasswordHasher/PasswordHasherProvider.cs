using System.Security.Cryptography;
using System.Text;
using Birthsys.Identity.Application.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace Birthsys.Identity.Application.Services.PasswordHasher
{
    public sealed class PasswordHasherProvider(
        IOptions<HashingOptions> hashingOptions
    ) : IPasswordHasherProvider
    {

        private readonly HashingOptions _hashingOptions = hashingOptions.Value;

        private static byte[] GenerateSalt(int length = 16)
        {
            var salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public string HashPassword(string password, out string saltBase64)
        {
            var salt = GenerateSalt(_hashingOptions.SaltSize);
            saltBase64 = Convert.ToBase64String(salt);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                MemorySize = _hashingOptions.MemorySize, // in KB
                DegreeOfParallelism = _hashingOptions.DegreeOfParallelism,
                Iterations = _hashingOptions.Iterations
            };

            var hash = argon2.GetBytes(_hashingOptions.HashSize); // 32 bytes = 256 bits
            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string password, string hashedPassword, string saltBase64)
        {
            var salt = Convert.FromBase64String(saltBase64);
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                MemorySize = _hashingOptions.MemorySize, // in KB
                DegreeOfParallelism = _hashingOptions.DegreeOfParallelism,
                Iterations = _hashingOptions.Iterations
            };

            var hash = argon2.GetBytes(32); // 32 bytes = 256 bits
            var computedHash = Convert.ToBase64String(hash);

            return computedHash == hashedPassword;
        }
    }
}