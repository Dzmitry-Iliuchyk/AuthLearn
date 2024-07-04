using Microsoft.AspNetCore.Authorization;

namespace AuthLearn.AuthPolicy {
    public class GroupAuthorizationHandler : AuthorizationHandler<GroupRequirement> {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public GroupAuthorizationHandler( IServiceScopeFactory serviceScopeFactory ) {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            GroupRequirement requirement ) {
            
            var parsingResult = Guid.TryParse( context.User.Claims
                .FirstOrDefault( x => x.Type == CustomClaims.UserId )?.Value, out Guid userId );
            if (parsingResult) {
                using var scope = _serviceScopeFactory.CreateScope();
                var authService = scope.ServiceProvider.GetRequiredService<Services.IAuthorizationService>();
                var roles = authService.GetGroupsAsync( userId );
                if (requirement.Role.Any( r => roles.Contains( r ) )) {
                    context.Succeed( requirement );
                    return Task.CompletedTask;
                }
            }
            context.Fail(new AuthorizationFailureReason( this, "User doesn't have required role!"));
            return Task.CompletedTask;
        }
    }
}
