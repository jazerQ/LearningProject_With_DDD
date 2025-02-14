namespace Infrastructure
{
    public interface IPasswordHasherService
    {
        string Generate(string password);
    }
}