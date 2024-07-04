using AuthLearn.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace AuthLearn.AuthPolicy {
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement> {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler( IServiceScopeFactory serviceScopeFactory ) {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement ) {

            var parsingResult = Guid.TryParse( context.User.Claims
                .FirstOrDefault( x => x.Type == CustomClaims.UserId )?.Value, out Guid userId );

            if (parsingResult) {
                using var scope = _serviceScopeFactory.CreateScope();
                var authService = scope.ServiceProvider.GetRequiredService<Services.IAuthorizationService>();
                var permissions = await authService.GetPermissionsAsync( userId );
                if (requirement.Permissions.All( p => permissions.Contains( p ) )) {
                    context.Succeed(requirement);
                }
            }
        }
    }
}
