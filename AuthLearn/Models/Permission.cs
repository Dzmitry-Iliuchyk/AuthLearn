using System.ComponentModel.DataAnnotations;

namespace AuthLearn.Models {
    public class Permission {
        public int Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        public ICollection<Group>? Groups { get; set;}
        public ICollection<GroupPermission>? GroupPermissions { get; set; } = [];
    }
}