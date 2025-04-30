using BugTicketingSystem.BL;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.BL
{
    public static class BusinessExtension
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IBugManager, BugManager>();
            services.AddScoped<IProjectManager, ProjectManager>();
            services.AddScoped<IAttachmentManager, AttachmentManager>();
            services.AddScoped<IUserBugManager, UserBugManager>();
            services.AddScoped<IAuthManager, AuthManager>();

            services.AddValidatorsFromAssembly(typeof(BusinessExtension).Assembly);
        }
    }
}
