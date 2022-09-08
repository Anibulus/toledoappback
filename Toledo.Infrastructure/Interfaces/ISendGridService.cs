
namespace Toledo.Infrastructure.Interfaces
{
    public interface ISendGridService
    {
        Task RecoverPassword(string email, string name, string url, string projectName, string projectImage);
        Task ConfirmationEmail(string email, string name, string password, string url, string projectName, string projectImage);
        Task WelcomeEmail(string email, string name, string password, string url);
        Task ConfirmationEmailJiro(string email, string name, string password, string url, string projectName, string projectImage);

    }
}
