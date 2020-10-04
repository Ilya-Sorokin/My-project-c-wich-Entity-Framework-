namespace Hotel.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HotelDB : DbContext
    {
        public HotelDB()
            : base("name=HotelDB")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Contract_> Contract_ { get; set; }
        public virtual DbSet<Free_Rooms> Free_Rooms { get; set; }
        public virtual DbSet<Service_> Service_ { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Contract_)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.Service_)
                .WithMany(e => e.Clients)
                .Map(m => m.ToTable("Service_Clients").MapLeftKey("ID_Client").MapRightKey("ID_Service"));

            modelBuilder.Entity<Free_Rooms>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Free_Rooms)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service_>()
                .HasMany(e => e.Staff)
                .WithMany(e => e.Service_)
                .Map(m => m.ToTable("Service_Staff").MapLeftKey("ID_Service").MapRightKey("ID_Staff"));
        }
    }
}
