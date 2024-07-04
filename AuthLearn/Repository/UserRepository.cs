using AuthLearn.DB;
using AuthLearn.Models;
using AuthLearn.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace AuthLearn.Repository
{
    public class UserRepository : IUserRepository {
        private readonly UserDbContext _context;
        public UserRepository( UserDbContext userDb ) {
            _context = userDb;
        }
        public async Task<User?> GetUserAsync( Guid userId ) {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync( u => u.Id == userId );
        }
        public async Task<User?> GetUserByEmailAsync( string email ) {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync( u => u.Email == email );
        }
        public async Task AddUserAsync( User user ) {
            await _context.Users.AddAsync( user );
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUserAsync( User user ) {
            _context.Users.Update( user );
            await _context.SaveChangesAsync();
        }

        public async Task<HashSet<PermissionEnum>> GetUserPermissionsAsync( Guid userId ) {
            var groups = await _context.Users
                .AsNoTracking()
                .Include(u=>u.Groups)
                .ThenInclude(r=>r.Permissions)
                .Where( x => x.Id == userId ).Select(x=>x.Groups).ToListAsync();

            var permissions = groups.SelectMany( x => x ).SelectMany(r=>r.Permissions).Select(p=> (PermissionEnum)p.Id).ToHashSet();
            return permissions;
        }

        public HashSet<GroupEnum> GetUserGroupsAsync( Guid userId ) {
            var groups = _context.Users
               .AsNoTracking()
               .Include( u => u.Groups )
               .ThenInclude( r => r.Permissions )
               .Where( x => x.Id == userId )
               .SelectMany(x=>x.Groups)
               .Select(x=>(GroupEnum)x.Id)
               .ToHashSet();
            return groups;
        }
    }
}
