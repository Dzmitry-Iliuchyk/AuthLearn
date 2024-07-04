using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AuthLearn.Models
{
    public class User
    {
        public User()
        {
        }

        public User(Guid id, string name, string email, ICollection<Group> groups, string passwordHash)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;

            Groups = groups;
        }
        public Guid Id { get; set; }
        [MaxLength(80)]
        public string Name { get; set; }
        [MaxLength(80)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Group> Groups { get; set; } = [];
        public ICollection<UserGroup> UserGroups { get; set; } = [];

    }
}
