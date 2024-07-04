using AuthLearn.AuthPolicy;
using AuthLearn.Models.Enum;
using AuthLearn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthLearn.Controllers {
    [Route( "api/[controller]" )]
    [Authorize(Policy = CustomPolicyNames.AdminOnly)]
    [ApiController]
    public class AdminController : ControllerBase {
        private readonly IAdminService _adminService;

        public AdminController( IAdminService adminService ) {
            _adminService = adminService;
        }

        [HttpPost]
        [Route( "AddUserToGroup" )]
        public async Task<IResult> AddUserToGroup( string userEmail, GroupEnum group ) {
            var result = await _adminService.AddUserToGroup( userEmail, group );
            return Results.Ok(result);
        }
        [HttpPost]
        [Route( "RemoveUserFromGroup" )]
        public async Task<IResult> RemoveUserFromGroup( string userEmail, GroupEnum group ) {
            var result = await _adminService.RemoveUserFromGroup( userEmail, group );
            return Results.Ok( result );
        }
    }
}
