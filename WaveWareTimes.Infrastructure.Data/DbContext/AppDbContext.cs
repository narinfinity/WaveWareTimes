using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using WaveWareTimes.Core.Entities.Domain;
using WaveWareTimes.Infrastructure.Data.EntityConfiguration;

namespace WaveWareTimes.Infrastructure.Data.DbContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        static AppDbContext() { Database.SetInitializer<AppDbContext>(null); }
        public AppDbContext() : base("IdentityDbConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            Configuration.UseDatabaseNullSemantics = false;
        }
        //OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            //EntityConfiguration
            modelBuilder.Configurations.Add(new WorkTimeRecordEntityConfiguration());

        }

        protected override void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {

            }
            //Clean up unmanagable resources

            base.Dispose(disposing);
        }
        ~AppDbContext()
        {
            Dispose(false);
        }

    }
}
