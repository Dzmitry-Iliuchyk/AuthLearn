using AuthLearn.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace AuthLearn.AuthPolicy {
    public class GroupRequirement : IAuthorizationRequirement {
        public GroupRequirement( GroupEnum[] role ) {
            Role = role;
        }

        public GroupEnum[] Role { get; set; } = [];
    }
}
