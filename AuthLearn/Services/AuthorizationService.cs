using AuthLearn.Models.Enum;
using AuthLearn.Repository;
using Microsoft.AspNetCore.Authorization;

namespace AuthLearn.Services {
    public class AuthorizationService : IAuthorizationService {
        private readonly IUserRepository _userRepository;
        public AuthorizationService( IUserRepository userRepository ) {
            _userRepository = userRepository;
        }

        public async Task<HashSet<PermissionEnum>> GetPermissionsAsync( Guid userId ) {
            return await _userRepository.GetUserPermissionsAsync( userId );
        }

        public HashSet<GroupEnum> GetGroupsAsync( Guid userId ) {
            return _userRepository.GetUserGroupsAsync( userId );
        }
    }
}
