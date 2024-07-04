using AuthLearn.Models.Enum;

namespace AuthLearn.Services {
    public interface IAdminService {
        Task<bool> AddUserToGroup( string userEmail, GroupEnum group );
        Task<bool> RemoveUserFromGroup( string userEmail, GroupEnum group );
    }
}