using AuthLearn.DB;
using AuthLearn.Models;
using AuthLearn.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace AuthLearn.Repository {
    public class GroupRepository : IGroupRepository {
        private readonly UserDbContext _context;
        public GroupRepository( UserDbContext userDb ) {
            _context = userDb;
        }
        public async Task<IEnumerable<Group>> GetGroups() {
            return await _context.Groups.Include( r => r.Permissions ).ToListAsync();
        }
        public async Task<Group> GetGroupsByEnum(GroupEnum group) {
            return await _context.Groups.AsNoTracking().FirstOrDefaultAsync(r=>r.Id == (int)group);
        }
        public async Task AddUserToGroup( GroupEnum group, User user ) {
            var searchedGroup = await _context.Groups.FirstOrDefaultAsync( r => r.Id == (int)group );
            searchedGroup.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public Task RemoveUserFromGroup( GroupEnum group, User user ) {
            throw new NotImplementedException();
        }
    }
}
