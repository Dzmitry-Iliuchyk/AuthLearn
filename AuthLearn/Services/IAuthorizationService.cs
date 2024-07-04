using AuthLearn.Models.Enum;

namespace AuthLearn.Services {
    public interface IAuthorizationService {
        Task<HashSet<PermissionEnum>> GetPermissionsAsync( Guid userId );
        HashSet<GroupEnum> GetGroupsAsync( Guid userId );
    }
}