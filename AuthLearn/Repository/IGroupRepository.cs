using AuthLearn.Models;
using AuthLearn.Models.Enum;

namespace AuthLearn.Repository {
    public interface IGroupRepository {
        Task AddUserToGroup( GroupEnum group, User user );
        Task<IEnumerable<Group>> GetGroups();
        Task<Group> GetGroupsByEnum( GroupEnum group );
        Task RemoveUserFromGroup( GroupEnum group, User user );
    }
}