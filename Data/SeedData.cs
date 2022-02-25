using Microsoft.AspNetCore.Identity;
using web_api.Authentication;

namespace web_api.Data
{
    public class SeedData
    {

        public static void Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "admin@localhost.com",
                    UserName = "admin@localhost.com"
                };

                var result = userManager.CreateAsync(user, "P@ssword123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("facilityhead").Result == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "facilityhead@localhost.com",
                    UserName = "facilityhead@localhost.com"
                };

                var result = userManager.CreateAsync(user, "P@ssword123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "FacilityHead").Wait();
                }
            }

            if (userManager.FindByNameAsync("assignees").Result == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "assignees@localhost.com",
                    UserName = "assignees@localhost.com"
                };

                var result = userManager.CreateAsync(user, "P@ssword123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Assignees").Wait();
                }
            }

            if (userManager.FindByNameAsync("user").Result == null)
            {
                var user = new ApplicationUser()
                {
                    Email = "user@localhost.com",
                    UserName = "user@localhost.com"
                };

                var result = userManager.CreateAsync(user, "P@ssword123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }

            }

        }
        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Administrator"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("FacilityHead").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "FacilityHead"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
            if (!roleManager.RoleExistsAsync("Assignees").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "Assignees"
                };
                var result = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole()
                {
                    Name = "User"
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
