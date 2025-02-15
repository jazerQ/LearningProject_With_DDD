namespace Infrastructure
{
    public interface IPasswordHasherService
    {
        string Generate(string password);
        bool Verify(string password, string hashpassword);
    }
}