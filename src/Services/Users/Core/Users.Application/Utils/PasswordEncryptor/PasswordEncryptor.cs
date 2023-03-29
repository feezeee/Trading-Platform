using Users.Application.Utils.PasswordEncryptor;

namespace Users.Application.Utils.PasswordEncrypter
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        public string GeneratePassword(string password)
        {
            var resPassword = password;
            return resPassword;
        }
    }
}
