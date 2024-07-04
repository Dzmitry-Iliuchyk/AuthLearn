
using AuthLearn.AuthPolicy;
using AuthLearn.Configuration;
using AuthLearn.DB;
using AuthLearn.Models;
using AuthLearn.Models.Enum;
using AuthLearn.Repository;
using AuthLearn.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthLearn {
    public class Program {
        public static void Main( string[] args ) {
            var builder = WebApplication.CreateBuilder( args );
            var jwtOptions = builder.Configuration.GetRequiredSection( nameof( JwtOptions ) );
            var authOptions = builder.Configuration.GetRequiredSection( nameof( Configuration.AuthorizationOptions ) );
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString( "SqlliteConnectionString" );
            builder.Services.Configure<JwtOptions>( jwtOptions );
            builder.Services.Configure<Configuration.AuthorizationOptions>( authOptions );
            builder.Services.AddScoped<IJWTService, JWTService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<Services.IAuthorizationService, AuthorizationService>();
            builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            builder.Services.AddScoped<IAuthorizationHandler, GroupAuthorizationHandler>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddDbContext<UserDbContext>( opt => opt.UseSqlite( connectionString ) );
            builder.Services.AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
                .AddJwtBearer( JwtBearerDefaults.AuthenticationScheme, options => {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() {
                        ValidateIssuer = false,
                        ValidIssuer = jwtOptions.GetValue<string>( nameof( JwtOptions.Issuer ) ),
                        ValidateAudience = false,
                        ValidAudience = jwtOptions.GetValue<string>( nameof( JwtOptions.Audience ) ),
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(
                           jwtOptions.GetValue<string>( nameof( JwtOptions.Secret ) ) ) )
                    };

                    options.Events = new JwtBearerEvents {
                        OnMessageReceived = ( context ) => {
                            context.Token = context.Request.Cookies[ "Auth-Cookies" ];
                            return Task.CompletedTask;
                        }
                    };
                } );
            builder.Services.AddAuthorization( opt => {
                opt.AddPolicy( CustomPolicyNames.CanReadWeather, p => p.AddRequirements( new PermissionRequirement( [ PermissionEnum.ReadWeather ] ) ) );
                opt.AddPolicy( CustomPolicyNames.AdminOnly, p => p.AddRequirements( new GroupRequirement( [ GroupEnum.Admin ] ) ) );
                opt.AddPolicy( CustomPolicyNames.AdminOrDeveloper, p => p.AddRequirements( new GroupRequirement( [ GroupEnum.Admin, GroupEnum.Developer ] ) ) );
            } );
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
