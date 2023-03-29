namespace Users.Application.Utils.PasswordEncryptor
{
    public interface IPasswordEncryptor
    {
        public string GeneratePassword(string password);
    }
}
