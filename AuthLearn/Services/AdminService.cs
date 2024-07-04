using AuthLearn.Models.Enum;
using AuthLearn.Repository;

namespace AuthLearn.Services {
    public class AdminService : IAdminService {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;

        public AdminService( IUserRepository userRepository, IGroupRepository groupRepository ) {
            _userRepository = userRepository;
            _groupRepository = groupRepository;
        }
        public async Task<bool> AddUserToGroup( string userEmail, GroupEnum group ) {
            var user = await _userRepository.GetUserByEmailAsync( userEmail );
            if (user == null) {
                return false;
            }
            await _groupRepository.AddUserToGroup( group, user );
            return true;
        }
        public async Task<bool> RemoveUserFromGroup( string userEmail, GroupEnum group ) {
            var user = await _userRepository.GetUserByEmailAsync( userEmail );
            if (user == null) {
                return false;
            }
            await _groupRepository.RemoveUserFromGroup( group, user );
            return true;
        }

    }
}
