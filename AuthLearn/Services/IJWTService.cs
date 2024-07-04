using AuthLearn.Models;

namespace AuthLearn.Services {
    public interface IJWTService {
        string GenerateToken( User user );
    }
}