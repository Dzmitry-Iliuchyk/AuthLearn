using AuthLearn.Models;
using AuthLearn.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace AuthLearn.AuthPolicy {
    public class PermissionRequirement : IAuthorizationRequirement{
        public PermissionRequirement( PermissionEnum[] permissions ) {
            Permissions = permissions;
        }

        public PermissionEnum[] Permissions { get; set; } = [];
    }
}
