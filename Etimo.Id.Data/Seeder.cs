using Etimo.Id.Abstractions;
using Etimo.Id.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Etimo.Id.Data
{
    public class Seeder
    {
        public static async Task SeedAsync(IEtimoIdDbContext context, IServiceProvider services)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var passwordHasher = services.GetService(typeof(IPasswordHasher)) as IPasswordHasher;

            Console.WriteLine("Start seeding the database.");

            var adminUserId = new Guid("b7b229a3-c84d-495f-b4ac-da3a4fd0757f");
            var adminUser = new User
            {
                UserId = adminUserId,
                Username = "admin",
                Password = "etimo"
            };
            context.Users.Add(adminUser);

            var adminClientId = new Guid("11111111-1111-1111-1111-111111111111");
            var adminClientSecret = passwordHasher.Hash("etimo");
            context.Applications.Add(new Application
            {
                ApplicationId = 1,
                Name = "etimo-default",
                Description = "Automatically generated.",
                HomepageUri = "https://localhost:5010",
                RedirectUri = "https://localhost:5010/oauth2/callback",
                ClientId = adminClientId,
                ClientSecret = adminClientSecret,
                UserId = adminUserId,
                Type = "confidential"
            });

            context.Roles.Add(new Role
            {
                Name = "admin",
                Description = "Automatically generated.",
                ApplicationId = 1,
                Users = new List<User> { adminUser }
            });

            await context.SaveChangesAsync();

            Console.WriteLine("Finished seeding the database.");
        }
    }
}