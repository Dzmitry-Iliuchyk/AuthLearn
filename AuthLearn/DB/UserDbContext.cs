using AuthLearn.Configuration;
using AuthLearn.Models;
using AuthLearn.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthLearn.DB {
    public class UserDbContext : DbContext {
        private readonly AuthorizationOptions _authOptions;

        public UserDbContext( DbContextOptions<UserDbContext> options, IOptions<AuthorizationOptions> authOptions ) : base( options ) {
            _authOptions = authOptions.Value;
            //Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating( ModelBuilder modelBuilder ) {
            modelBuilder
                .Entity<User>()
                .HasMany( t => t.Groups )
                .WithMany( t => t.Users )
                .UsingEntity<UserGroup>(
                    l => l.HasOne<Group>( e => e.Group ).WithMany( e => e.UserGroups ).HasForeignKey( e => e.GroupId ),
                    r => r.HasOne<User>( e => e.User ).WithMany( e => e.UserGroups ).HasForeignKey( e => e.UserId ),
                    j => {
                        j.HasKey( x => new { x.GroupId, x.UserId } );
                        j.Property( e => e.CreatedOn ).HasDefaultValueSql( "CURRENT_TIMESTAMP" );
                    } );
            modelBuilder
                .Entity<Group>()
                .HasMany( t => t.Permissions )
                .WithMany( t => t.Groups )
                .UsingEntity<Models.GroupPermission>(
                    l => l.HasOne<Permission>( e => e.Permission ).WithMany( e => e.GroupPermissions ).HasForeignKey( e => e.PermissionId ),
                    r => r.HasOne<Group>( e => e.Group ).WithMany( e => e.GroupPermissions ).HasForeignKey( e => e.GroupId ),
                    j => {
                        j.HasKey( x => new { x.GroupId, x.PermissionId } );
                        j.Property( e => e.CreatedOn ).HasDefaultValueSql( "CURRENT_TIMESTAMP" );
                    } );

            var permissions = Enum
                .GetValues<PermissionEnum>()
                .Select( x => new Permission {
                    Id = (int)x,
                    Name = x.ToString(),
                } );
            modelBuilder.Entity<Permission>().HasData( permissions );

            var groups = Enum
                .GetValues<GroupEnum>()
                .Select( x => new Group {
                    Id = (int)x,
                    Name = x.ToString(),
                } );
            modelBuilder.Entity<Group>().HasData( groups );

            var groupPermissions = _authOptions.GroupPermissions.SelectMany( gp => gp.Permissions
                .Select( p => new Models.GroupPermission {
                    GroupId = (int)Enum.Parse<GroupEnum>( gp.Group ),
                    PermissionId = (int)Enum.Parse<PermissionEnum>( p )
                } ) )
                .ToArray();

            modelBuilder.Entity<Models.GroupPermission>().HasData( groupPermissions );
            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>().HasData( new User() {
                Id = Guid.NewGuid(),
                Email = "admin@admin.com",
                Name = "admin",
                PasswordHash = hasher.HashPassword( null, "admin" ),
            } );
            base.OnModelCreating( modelBuilder );
        }
    }
}
