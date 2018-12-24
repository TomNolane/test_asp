using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VueWebApplication.Models
{
    public class MyDbContext : IdentityDbContext<AppUser>
    {
        public static MyDbContext Create() { return new MyDbContext(); }

        static MyDbContext() { Database.SetInitializer<MyDbContext>(new MyDbInitializer()); }

        public MyDbContext() : base("name=Users", false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AppUser>().Ignore(x => x.PhoneNumber);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.PhoneNumberConfirmed);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.Email);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.EmailConfirmed);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.Claims);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.TwoFactorEnabled);
            //modelBuilder.Entity<AppUser>().Ignore(x => x.SecurityStamp);
        }

        //public static void init() { Create().Database.Initialize(true); } 

        public DbSet<Emails> emails { get; set; }

        public DbSet<Phones> phones { get; set; }

        public DbSet<Address> addresses { get; set; }

        public DbSet<Street> streets { get; set; }

        public DbSet<ChangesProfile> Changes { get; set; }
    }

}