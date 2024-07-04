using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthLearn.Models {
    public class UserGroup {
        public Guid UserId { get; set; }
        public User? User { get; set; } 
        public int GroupId { get; set; }
        public Group? Group { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}