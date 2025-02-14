namespace Infrastructure
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }
        public bool Verify(string password, string hashpassword) 
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hashpassword);
        }
    }
}
