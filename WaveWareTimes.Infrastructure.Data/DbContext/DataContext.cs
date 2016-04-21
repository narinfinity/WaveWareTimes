using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using WaveWareTimes.Core.Entities.Domain;
using WaveWareTimes.Core.Interfaces.Common;
using WaveWareTimes.Infrastructure.Data.EntityConfiguration;

namespace WaveWareTimes.Infrastructure.Data
{
    public class DataContext : IdentityDbContext<User>, IDataContext
    {
        static DataContext() { Database.SetInitializer<DataContext>(null); }
        public DataContext() : base("IdentityDbConnection", throwIfV1Schema: false)
        {
            Configuration.LazyLoadingEnabled = true;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            Configuration.UseDatabaseNullSemantics = false;
        }

        //DbSets
        public DbSet<WorkTimeRecord> WorkTimeRecords { get; set; }

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


        public IQueryable<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            this.Set<TEntity>().Attach(entity);
            base.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (this.Set<TEntity>().Local.Contains(entity))
                ((IObjectContextAdapter)this).ObjectContext.Detach(entity);
        }

        public TEntity Create<TEntity>(TEntity entity = null) where TEntity : class
        {
            if (entity != null)
            {
                this.Set<TEntity>().Attach(entity);
                base.Entry<TEntity>(entity).State = EntityState.Added;
            }
            else
                entity = this.Set<TEntity>().Create();

            return entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            this.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            base.SaveChanges();
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
        ~DataContext()
        {
            Dispose(false);
        }

    }
}
