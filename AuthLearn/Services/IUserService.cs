
namespace AuthLearn.Services {
    public interface IUserService {
        Task ActivateAdmin();
        Task<string> Login( string userEmail, string password );
        Task Register( string userName, string userEmail, string password );
    }
}