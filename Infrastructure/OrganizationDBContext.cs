using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Organization.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Infrastructure
{
    public class OrganizationDBContext : DbContext
    {
        public OrganizationDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many to many relationship 
            modelBuilder.Entity<User_Entity>().HasKey(sc => new { sc.UserId, sc.EntityId });
            modelBuilder.Entity<User_Entity>().HasOne<User>(sc => sc.user).WithMany(s => s.UserEntitees).HasForeignKey(sc => sc.UserId);
            modelBuilder.Entity<User_Entity>().HasOne<Entity>(sc => sc.Entity).WithMany(s => s.Employees).HasForeignKey(sc => sc.EntityId);
            modelBuilder.Entity<OKRPermission>().HasOne(a => a.user).WithMany(a => a.okrPermission).HasForeignKey(sc => sc.userId); ;//.HasForeignKey<OKRPermission>(c => c.userId);
            modelBuilder.Entity<WallPermission>().HasOne(a => a.user).WithMany(a => a.wallPermission).HasForeignKey(sc => sc.userId); ;//.HasForeignKey<OKRPermission>(c => c.userId);
            modelBuilder.Entity<OrganizationPermission>().HasOne(a => a.user).WithMany(a => a.organizationPermission).HasForeignKey(sc => sc.UserId); //.HasForeignKey<OKRPermission>(c => c.userId);
            modelBuilder.Entity<NotificationPermission>().HasOne(a => a.user).WithMany(a => a.notifPermission).HasForeignKey(sc => sc.UserId); //.HasForeignKey<OKRPermission>(c => c.userId);
            //modelBuilder.Entity<Entity>().Property(a => a.Id).ValueGeneratedNever();
            //modelBuilder.Entity<User>().HasOne(a => a.okrPermission).WithOne(b => b.user).HasForeignKey<OKRPermission>(b => b.user.Id); //one to one relationship
            //modelBuilder.Entity<User>().HasOne(a => a.wallPermission).WithOne(b => b.user);//.HasForeignKey<WallPermission>(b => b.userId); //one to one relationship
            //modelBuilder.Entity<User>().HasOne(a => a.organizationPermission).WithOne(b => b.user).HasForeignKey<OrganizationPermission>(b => b.UserId); //one to one relationship
            //modelBuilder.Entity<User>().HasOne(a => a.notifPermission).WithOne(b => b.user).HasForeignKey<NotificationPermission>(b => b.UserId); //one to one relationship
            // modelBuilder.Entity<User_Entity>().Property(u => u.Role).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
             modelBuilder.Entity<User_Entity>().Property(u => u.Id).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<ChildEntity> ChildEntities { get; set; }
        //public DbSet<AffectedPermissions> Affectedpermission { get; set; }
        public DbSet<NotificationPermission> NotificationPermission { get; set; }
        public DbSet<OKRPermission> OKRPermission { get; set; }
        public DbSet<OrganizationPermission> OrganizationPermission { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<User_Entity> User_Entity { get; set; }
        public DbSet<WallPermission> WallPermission { get; set; }
    }
}
