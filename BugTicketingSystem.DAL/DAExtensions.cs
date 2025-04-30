using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BugTicketingSystem.DAL
{
    public static class DAExtensions
    {
        public static void AddDAServices(this IServiceCollection services, IConfiguration config)
        {
            var conStr = config.GetConnectionString("default");
            services.AddDbContext<DatabaseContext>(options =>
            options
                    //.LogTo(Console.WriteLine)
                    .UseSqlServer(conStr)
                    .UseSeeding((context, _) =>
                    {
                        if (context.Set<User>().Any())
                            return;


                        var users = new List<User>
                    {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Marina Mansour",
                                Salary = 25000,
                                Email = "marina.mansour@gmail.com",
                                Password = "012345678",
                                Age = 23,
                                Role = UserRole.Developer
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Basem Mohamed",
                                Salary = 22000,
                                Email = "basem.m1337@gmail.com",
                                Password = "012345678",
                                Age = 23,
                                Role = UserRole.Developer
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Kholoud Ahmed",
                                Salary = 23000,
                                Email = "lilksaby@gmail.com",
                                Password = "012345678",
                                Age = 21,
                                Role = UserRole.Tester
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Hany Abdou",
                                Salary = 25000,
                                Email = "hanyabdoustd@gmail.com",
                                Password = "012345678",
                                Age = 25,
                                Role = UserRole.Tester
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Mohamed Hatem",
                                Salary = 30000,
                                Email = "m.hatem@gmail.com",
                                Password = "012345678",
                                Age = 27,
                                Role = UserRole.Manager
                            },
                    };

                        var projects = new List<Project>
                    {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "OpenAI",
                                Status = ProjectStatus.Cancelled
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Azure",
                                Status = ProjectStatus.Completed
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "CRM",
                                Status = ProjectStatus.InProgress
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "PowerBI",
                                Status = ProjectStatus.Completed
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "UI/UX",
                                Status = ProjectStatus.Completed
                            },
                    };

                        var bugs = new List<Bug>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "We need more money",
                                Risk = BugRisk.Critical,
                                ProjectID = projects[0].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Authentication",
                                Risk = BugRisk.Critical,
                                ProjectID = projects[1].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Cart",
                                Risk = BugRisk.Medium,
                                ProjectID = projects[2].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Profile",
                                Risk = BugRisk.Normal,
                                ProjectID = projects[3].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "Profile Picture",
                                Risk = BugRisk.Low,
                                ProjectID = projects[4].Id
                            },
                        };

                        var userBugs = new List<UserBug>
                        {
                            new()
                            {
                                UserId = users[0].Id,
                                BugId = bugs[2].Id
                            },
                            new()
                            {
                                UserId = users[1].Id,
                                BugId = bugs[3].Id
                            },
                            new()
                            {
                                UserId = users[3].Id,
                                BugId = bugs[2].Id
                            },
                        };

                        //var atts = new List<Attachment>
                        //{
                        //    new()
                        //    {
                        //        Id = Guid.NewGuid(),
                        //        ImgLink = "No Attachment",
                        //        BugID = bugs[0].Id
                        //    },
                        //    new()
                        //    {
                        //        Id = Guid.NewGuid(),
                        //        ImgLink = "No Attachment",
                        //        BugID = bugs[1].Id
                        //    },
                        //    new()
                        //    {
                        //        Id = Guid.NewGuid(),
                        //        ImgLink = "No Attachment",
                        //        BugID = bugs[2].Id
                        //    },
                        //};

                        var atts = new List<Attachment>
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "None",
                                Type = "None",
                                FileUrl = "No Attachment",
                                BugID = bugs[0].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "None",
                                Type = "None",
                                FileUrl = "No Attachment",
                                BugID = bugs[1].Id
                            },
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = "None",
                                Type = "None",
                                FileUrl = "No Attachment",
                                BugID = bugs[2].Id
                            },
                        };



                        context.AddRange(users);
                        context.AddRange(projects);
                        context.AddRange(bugs);
                        context.AddRange(atts);
                        context.AddRange(userBugs);

                        context.SaveChanges();
                    }));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IUserBugRepository, UserBugRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
