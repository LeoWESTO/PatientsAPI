using Microsoft.EntityFrameworkCore;
using PatientsAPI.Database.Models;

namespace PatientsAPI.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Specialization> Specializations { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Area)
                .WithMany()
                .HasForeignKey(p => p.AreaId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Cabinet)
                .WithMany()
                .HasForeignKey(d => d.CabinetId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Specialization)
                .WithMany()
                .HasForeignKey(d => d.SpecializationId);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Area)
                .WithMany()
                .HasForeignKey(d => d.AreaId);
        }
    }
}
