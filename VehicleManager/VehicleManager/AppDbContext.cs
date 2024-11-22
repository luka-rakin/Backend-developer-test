using Microsoft.EntityFrameworkCore;
using VehicleManager.Models;

namespace VehicleManager
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

        }


        public DbSet<VehicleMake> vehicleMakes { get; set; }
        public DbSet<VehicleModel> vehicleModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleModel>()
                .HasOne(vm => vm.VehicleMake)
                .WithMany()
                .HasForeignKey(vm => vm.VehicleMakeId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
