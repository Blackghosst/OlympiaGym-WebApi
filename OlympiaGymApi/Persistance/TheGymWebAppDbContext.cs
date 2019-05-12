using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using OlympiaGymApi.Core.Models;

namespace OlympiaGymApi.Persistance
{
    public class OlympiaGymApiDbContext : DbContext
    {
        private IHttpContextAccessor httpContextAccessor;

        public DbSet<Member> Members { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public OlympiaGymApiDbContext(DbContextOptions<OlympiaGymApiDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Member>()
                .HasQueryFilter(m => (!m.IsDeleted && m.IsActive && !m.IsBlackListed))
                .HasAlternateKey(c => c.Cin);

            modelbuilder.Entity<Member>()
                .Property(m => m.IsActive)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();
            modelbuilder.Entity<Member>()
                .Property(m => m.IsBlackListed)
                .HasDefaultValue(false);
            modelbuilder.Entity<Member>()
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false)
                .ValueGeneratedOnAdd();
            modelbuilder.Entity<Member>()
                .Property(m => m.IsDeleted)
                .HasDefaultValue(false);
            //modelbuilder.Entity<Member>()
            //    .Property(m => m.CreatedDate)
            //    .HasDefaultValue(DateTime.Now)
            //    .ValueGeneratedOnAdd();
            //modelbuilder.Entity<Member>()
            //    .Property(m => m.LastUpdated)
            //    .HasDefaultValue(DateTime.Now)
            //    .ValueGeneratedOnUpdate();
        }

        //public override int SaveChanges()
        //{
        //    AddTimestamps();
        //    return base.SaveChanges();
        //}

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {
            var context = httpContextAccessor.HttpContext;
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(context?.User?.Identity?.Name)
                ? context.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedBy = currentUsername;
                }

                ((BaseEntity)entity.Entity).LastUpdated = DateTime.Now;
                ((BaseEntity)entity.Entity).UpdatedBy = currentUsername;
            }
        }
    }
}
