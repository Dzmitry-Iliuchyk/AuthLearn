

using AuthLearn.Models;

namespace AuthLearn.Configuration {
    public class AuthorizationOptions {
        public GroupPermission[] GroupPermissions { get; set; } = [];
    }
    public class GroupPermission {
        public string Group { get; set; } = string.Empty;
        public string[] Permissions { get; set; } = [];
    }
}
