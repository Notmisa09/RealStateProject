using Microsoft.EntityFrameworkCore;
using RealStateApp.Core.Domain.Common;
using RealStateApp.Core.Domain.Entities;

namespace RealStateApp.Infrastructure.Persistence.Context
{
    public class RealStateContext : DbContext
    {
        public RealStateContext(DbContextOptions<RealStateContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Improvements> Improvements { get; set;}
        public DbSet<Properties> Properties { get; set;}
        public DbSet<PropertyImprovements> PropertyImprovements { get; set;}
        public DbSet<SellingType> SellingType { get; set;}  
        public DbSet<PropertyType> PropertyType { get; set;}
        public DbSet<PropertyImages> PropertyImages { get; set;}

        protected override void OnModelCreating(ModelBuilder mb )
        {
            base.OnModelCreating(mb);

            mb.Entity<Improvements>().HasKey(i => i.Id);
            mb.Entity<Properties>().HasKey(p => p.Id);
            mb.Entity<SellingType>().HasKey(s => s.Id);
            mb.Entity<PropertyImages>().HasKey(s => s.Id);
            mb.Entity<PropertyType>().HasKey(p => p.Id);
            mb.Entity<ClientPropertyFav>().HasKey(p => p.Id);

            mb.Entity<Improvements>().ToTable("Improvements");
            mb.Entity<Properties>().ToTable("Property");
            mb.Entity<PropertyImprovements>().ToTable("PropertyImprovements");
            mb.Entity<SellingType>().ToTable("SellingType");
            mb.Entity<PropertyImages>().ToTable("PropertyImages");
            mb.Entity<PropertyType>().ToTable("PropertyType");
            mb.Entity<ClientPropertyFav>().ToTable("ClientPropertyFav");


            //PROPERTY IMRPOVEMENTS TABLE INTERMEDIA

            mb.Entity<PropertyImprovements>()
                .HasOne(p => p.Property)
                .WithMany(p => p.PropertyImprovements)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PropertyImprovements>()
                .HasOne(p => p.Improvements)
                .WithMany(p => p.PropertyImprovements)
                .HasForeignKey(p => p.ImprovementId) 
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<PropertyImprovements>()
                .HasKey(p => new { p.PropertyId, p.ImprovementId });

            //PROPERTY TYPE
            mb.Entity<PropertyType>()
                .HasMany(p => p.Property)
                .WithOne(p => p.PropertyType)
                .HasForeignKey(p => p.PropertyTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            //SELLING TYPE
            mb.Entity<SellingType>()
                .HasMany(p => p.Properties)
                .WithOne(p => p.SellingType)
                .HasForeignKey(p => p.SellingTypeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
