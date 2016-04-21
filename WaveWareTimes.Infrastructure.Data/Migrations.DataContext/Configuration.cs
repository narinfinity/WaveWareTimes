namespace WaveWareTimes.Infrastructure.Data.Migrations.DataContext
{
    using Core.Entities.Domain;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WaveWareTimes.Infrastructure.Data.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations.DataContext";
        }

        protected override void Seed(WaveWareTimes.Infrastructure.Data.DataContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = "User" };

            if (!context.Set<IdentityRole>().Any(r => r.Name == "User"))
            {
                roleManager.Create(role);
                context.Save();
            }
            if (!context.Set<IdentityRole>().Any(r => r.Name == "Admin"))
            {
                role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);
                context.Save();
            }

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var newUser = new User { UserName = "test_admin", Email = "admin@test.com", FirstName = "AdminFirstName", LastName = "AdminLastName" };
                        
            if (!context.Set<User>().Any(u => u.UserName == "test_admin"))
            {                
                userManager.Create(newUser, "Test123#");
                userManager.AddToRole(newUser.Id, "Admin");
                context.Save();
            }
            if (!context.Set<User>().Any(u => u.UserName == "test_user"))
            {
                newUser = new User { UserName = "test_user", Email = "user@test.com", FirstName = "UserFirstName", LastName = "UserLastName" };
                userManager.Create(newUser, "Test123#");
                userManager.AddToRole(newUser.Id, "User");
                context.Save();
            }
            var user = context.Set<User>().FirstOrDefault(e => e.UserName == "test_user");
            if (user != null)
            {
                var workTimeRecord = new WorkTimeRecord
                {
                    Start = DateTime.Now.AddHours(-8),
                    End = DateTime.Now,
                    Description = "test_user's working time recording",
                    User = user,
                    UserId = user.Id
                };

                context.Create(workTimeRecord);
                context.Save();
            }
            base.Seed(context);
        }
    }
}
