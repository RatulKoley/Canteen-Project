using Microsoft.EntityFrameworkCore;

namespace youtubetuto
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<Supply> Supply { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Purchase> Purchase { get; set; }
        public virtual DbSet<FoodMenu> FoodMenu { get; set; }
        public virtual DbSet<FoodMapping> FoodMapping { get; set; }
        public virtual DbSet<KitchenFood> KitchenFood { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasOne(_ => _.Unit)
                .WithMany(_ => _.Item)
                .HasForeignKey(_ => _.UnitId);

            modelBuilder.Entity<Stock>()
                .HasOne(_ => _.Item)
                .WithOne(_ => _.Stock)
                .HasForeignKey<Stock>(_ => _.ItemId);

            modelBuilder.Entity<Purchase>()
                .HasOne(_ => _.Item)
                .WithMany(_ => _.Purchase)
                .HasForeignKey(_ => _.ItemId);

            modelBuilder.Entity<Purchase>()
                .HasOne(_ => _.Supply)
                .WithMany(_ => _.Purchase)
                .HasForeignKey(_ => _.SupplyId);

            modelBuilder.Entity<FoodMapping>()
                .HasOne(_ => _.FoodMenu)
                .WithMany(_ => _.FoodMapping)
                .HasForeignKey(_ => _.FoodID);

            modelBuilder.Entity<FoodMapping>()
                .HasOne(_ => _.Item)
                .WithMany(_ => _.FoodMapping)
                .HasForeignKey(_ => _.ItemId);

            modelBuilder.Entity<KitchenFood>()
                .HasOne(_ => _.FoodMenu)
                .WithOne(_ => _.KitchenFood)
                .HasForeignKey<KitchenFood>(_ => _.FoodID);

            modelBuilder.Entity<Sales>()
               .HasOne(_ => _.KitchenFood)
               .WithMany(_ => _.Sales)
               .HasForeignKey(_ => _.KitchenFoodID);


        }
    }
}
