using AuthLearn.Models;
using AuthLearn.Repository;
using Microsoft.AspNetCore.Identity;

namespace AuthLearn.Services
{
    public class UserService : IUserService {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IJWTService _JWTService;
        public UserService( IPasswordHasher<User> passwordHasher, IGroupRepository groupRepository, IUserRepository userRepository, IJWTService jwtService ) {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _groupRepository = groupRepository;
            _JWTService = jwtService;
        }

        public async Task Register( string userName, string userEmail, string password ) {

            var hashedPassword = _passwordHasher.HashPassword( null, password );

            var user = new User( Guid.NewGuid(), userName, userEmail, [], hashedPassword );

            await _userRepository.AddUserAsync( user );
        }
        public async Task<string> Login( string userEmail, string password ) {

            var user = await _userRepository.GetUserByEmailAsync( userEmail );
            if (user == null) {
                // To do: обработать случай не верного email
                return "";
            }
            var result = _passwordHasher.VerifyHashedPassword( null, user.PasswordHash, password );

            if (result == PasswordVerificationResult.Failed) {
                // To do: обработать случай не верного пароля
                return "";
            }
            var token = _JWTService.GenerateToken( user );
            return token;
        }
        public async Task ActivateAdmin( ) {
            var admin = await _userRepository.GetUserByEmailAsync( "admin@admin.com");
            await _groupRepository.AddUserToGroup(Models.Enum.GroupEnum.Admin, admin );
        }

    }
}
