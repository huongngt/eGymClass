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

            if (!context.Users.Any(u => u.UserName == "admin@gymbooking.se"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole { Name = "admin" });

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser { UserName = "admin@gymbooking.se", Email = "admin@gymbooking.se" };
                var result = userManager.Create(user, "password");
                userManager.AddToRole(user.Id, "admin");
            }

            context.GymClasses.AddOrUpdate(c => c.Name,
                new GymClass { Name = "Morning Class", Description = "Morning Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(45) },
                new GymClass { Name = "Evening Class", Description = "Evening Class", StartTime = DateTime.Now, Duration = TimeSpan.FromMinutes(60) }

                );
        }
    }
}
