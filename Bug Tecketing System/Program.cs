
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using BugTicketingSystem.BL;
using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Bug_Tecketing_System
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<UserManager>();

            builder.Services.AddBusinessServices();

            builder.Services.AddDAServices(builder.Configuration);

            builder.Services.AddTransient<AttachmentUploadDtoValidator>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var secretKey = builder.Configuration.GetValue<string>("SecretKey")!;
                var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
                var key = new SymmetricSecurityKey(secretKeyBytes);

                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key,
                    RoleClaimType = ClaimTypes.Role
                };
            });

            //builder.Services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretKey"]))
            //    };
            //});

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                        Constants.Policies.ForManagerOnly,
                        builder => builder
                        .RequireClaim(ClaimTypes.Role, "Manager")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                    );

                options.AddPolicy(
                        Constants.Policies.ForDevelopingOnly,
                        builder => builder
                        .RequireClaim(ClaimTypes.Role, "Developer")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                    );

                options.AddPolicy(
                        Constants.Policies.ForTestingOnly,
                        builder => builder
                        .RequireClaim(ClaimTypes.Role, "Tester")
                        .RequireClaim(ClaimTypes.NameIdentifier)
                    );
            });

            builder.Services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
