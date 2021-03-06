namespace eGymClass.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<eGymClass.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(eGymClass.Models.ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole { Name = "admin" });
                
            }

            if (!context.Users.Any(u => u.UserName == "admin@gymbooking.se"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser { UserName = "admin@gymbooking.se",
                    Email = "admin@gymbooking.se",
                    TimeOfRegistration = DateTime.Now,
                    FirstName = "Admin",
                    LastName = "User"
                };
                var result = userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "admin");
            }

            if (!context.Users.Any(u => u.UserName == "member@email.se"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser
                {
                    UserName = "member@email.se",
                    Email = "member@email.se",
                    TimeOfRegistration = DateTime.Now,
                    FirstName = "Member",
                    LastName = "User"
                };
                var result = userManager.Create(user, "password");
            }

            context.GymClasses.AddOrUpdate(c => c.Name,
                new GymClass { Name = "Day Class 1", Description = "Morning Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(45) },
                new GymClass { Name = "Day Class 2", Description = "Morning Class", StartTime = DateTime.Now.AddHours(5), Duration = TimeSpan.FromMinutes(30) },
                new GymClass { Name = "Day Class 3", Description = "Morning Class", StartTime = DateTime.Now.AddDays(2), Duration = TimeSpan.FromMinutes(60) },
                new GymClass { Name = "Evening Class 1", Description = "Evening Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(50) },
                new GymClass { Name = "Evening Class 2", Description = "Evening Class", StartTime = DateTime.Now.AddHours(5), Duration = TimeSpan.FromMinutes(90) },
                new GymClass { Name = "Evening Class 3", Description = "Evening Class", StartTime = DateTime.Now.AddDays(2), Duration = TimeSpan.FromMinutes(70) }

                );
        }
    }
}
