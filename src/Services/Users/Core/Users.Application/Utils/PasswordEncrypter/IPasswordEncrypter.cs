namespace Users.Application.Utils.PasswordEncrypter
{
    public interface IPasswordEncrypter
    {
        public string GeneratePassword(string password);
    }
}
