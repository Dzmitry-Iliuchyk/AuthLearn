using AuthLearn.Services;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AuthLearn.Controllers {
    [Route( "api/[Controller]" )]
    [ApiController]
    public class UserController : ControllerBase {
        
        private readonly IUserService _userService;
        public UserController( 
            IUserService userService) {
            _userService = userService;
        }

        [HttpPost]
        [Route( "LogIn" )]
        public async Task<IResult> Login(string email, string password) {

            var token = await _userService.Login(email, password);
            if (token.IsNullOrEmpty()) {
                //затычка
                return Results.Problem(detail: "Login was failed!");
            }

            base.Response.Cookies.Append( "Auth-Cookies", token );

            return Results.Ok(token);
        }

        [HttpGet]
        [Authorize]
        [Route( "LogOut" )]
        public async Task<IResult> LogOut() {
            Response.Cookies.Delete( "Auth-Cookies" );
            return Results.Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route( "ActivateAdminProfile" )]
        public async Task<IResult> ActivateAdminProfile() {
            await _userService.ActivateAdmin();
            return Results.Ok();
        }

        [HttpPost]
        [Route( "Register" )]
        public async Task<IResult> Register( string userName, string userEmail, string password ) {
            await _userService.Register( userName, userEmail, password );
            return Results.Ok();
        }

    }
}
