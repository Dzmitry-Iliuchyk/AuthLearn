
using AuthLearn.Models;
using AuthLearn.Models.Enum;

namespace AuthLearn.Repository
{
    public interface IUserRepository {
        Task AddUserAsync( User user );
        Task<User?> GetUserAsync( Guid userId );
        Task<User?> GetUserByEmailAsync( string email );
        Task<HashSet<PermissionEnum>> GetUserPermissionsAsync( Guid userId );
        HashSet<GroupEnum> GetUserGroupsAsync( Guid userId );
        Task UpdateUserAsync( User user );
    }
}