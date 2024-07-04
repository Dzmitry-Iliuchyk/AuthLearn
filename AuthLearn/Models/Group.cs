using System.ComponentModel.DataAnnotations;

namespace AuthLearn.Models
{
    public class Group
    {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission>? Permissions { get; set; } = [];
        public ICollection<User>? Users { get; set; } = [];
        public ICollection<UserGroup>? UserGroups { get; set; } = [];
        public ICollection<GroupPermission>? GroupPermissions { get; set; } = [];
    }
}