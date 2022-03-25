using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RadMock2021.DataModel
{

    public class ApplicationDbSeeder
    {
        private readonly Application_DB_Context _ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbSeeder(Application_DB_Context ctx, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();
            // seed the Roles
            if (_roleManager.Roles.Count() < 1)
            {
                await _roleManager.CreateAsync(new IdentityRole("Purchase Manager"));
                await _roleManager.CreateAsync(new IdentityRole("Empoylee"));
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            }
            // seed the main user
            if (_ctx.Users.Count() < 1)
            {
                var PurchaseManager = new ApplicationUser()
                {

                    SecondName = "Bragg",
                    FirstName = "Billy",
                    Email = "bbragg@sligo.ie",
                    UserName = "bbragg@sligo.ie",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var Empoylee = new ApplicationUser
                {
                    FirstName = "Fred",
                    SecondName = "Spectre",
                    Email = "fspectre@itsligo.ie",
                    UserName = "fspectre@itsligo.ie",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var Admin = new ApplicationUser
                {
                    FirstName = "Martha",
                    SecondName = "Ruddy",
                    Email = "mruddy@itsligo.ie",
                    UserName = "mruddy@itsligo.ie",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                // add users
                await CreateUser(PurchaseManager, "Services$1");
                await CreateUser(Empoylee, "Services$1");
                await CreateUser(Admin, "Services$1");
                // add Roles
                await _userManager.CreateAsync(PurchaseManager, "PurchaseManager");
                await _userManager.CreateAsync(Empoylee, "Empoylee");
                await _userManager.CreateAsync(Admin, "Admin");
                _ctx.SaveChanges();

            }
        }

        private async Task CreateUser(ApplicationUser PurchaseManager, string role)
        {
            var result = await _userManager.CreateAsync(PurchaseManager, role);
            if (result != IdentityResult.Success)
            {
                throw new InvalidOperationException("Could not create user in Seeding");
            }
        }
    }
}
