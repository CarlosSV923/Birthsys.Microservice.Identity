namespace Birthsys.Identity.Application.Services.PasswordHasher
{
    public interface IPasswordHasherProvider
    {
        string HashPassword(string password, out string saltBase64);
        bool VerifyPassword(string password, string hashedPassword, string saltBase64);
    }
}