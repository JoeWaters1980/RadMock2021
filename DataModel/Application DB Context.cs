using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RadMock2021.DataModel
{
    public class Application_DB_Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            var connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Rad302Mock_2021";
            optionBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionBuilder);
        }
    }

}
