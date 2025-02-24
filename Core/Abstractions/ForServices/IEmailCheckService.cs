namespace Infrastructure
{
    public interface IEmailCheckService
    {
        bool EmailIsValid(string email);
    }
}