using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shopping.Models
{
    public partial class MyShoppingDBContext : DbContext
    {
        public MyShoppingDBContext()
        {
        }

        public MyShoppingDBContext(DbContextOptions<MyShoppingDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<InventoryList> InventoryLists { get; set; }
        public virtual DbSet<Shoping> Shopings { get; set; }
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=MyShoppingDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .HasColumnName("barcode");

                entity.Property(e => e.CurrentQuantity).HasColumnName("Current_quantity");

                entity.Property(e => e.Dateposted)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1025)
                    .HasColumnName("Image_url");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Product_name");

                entity.Property(e => e.ProductPrice).HasColumnName("Product_price");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ToUserTable");
            });

            modelBuilder.Entity<InventoryList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("InventoryList");

                entity.Property(e => e.AvailableQuantity).HasColumnName("Available_quantity");

                entity.Property(e => e.Barcode)
                    .HasMaxLength(50)
                    .HasColumnName("barcode");

                entity.Property(e => e.CurrentQuantity).HasColumnName("Current_quantity");

                entity.Property(e => e.Dateposted).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1025)
                    .HasColumnName("Image_url");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .HasColumnName("Product_name");

                entity.Property(e => e.ProductPrice).HasColumnName("Product_price");

                entity.Property(e => e.TotalItemCost).HasColumnName("total_item_cost");

                entity.Property(e => e.UserId).HasColumnName("User_id");
            });

            modelBuilder.Entity<Shoping>(entity =>
            {
                entity.HasKey(e => e.ItemId)
                    .HasName("PK__Shoping__3FB403ACE3F680DE");

                entity.ToTable("Shoping");

                entity.Property(e => e.ItemId).HasColumnName("Item_id");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1024)
                    .HasColumnName("Image_url");

                entity.Property(e => e.ItemBarcode)
                    .HasMaxLength(50)
                    .HasColumnName("Item_barcode");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Item_name");

                entity.Property(e => e.ItemPrice).HasColumnName("Item_price");

                entity.Property(e => e.ItemQuantity).HasColumnName("Item_quantity");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shopings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shopping_ToUserTable");
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ShoppingList");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1024)
                    .HasColumnName("Image_url");

                entity.Property(e => e.IsConfirmed).HasColumnName("isConfirmed");

                entity.Property(e => e.ItemBarcode)
                    .HasMaxLength(50)
                    .HasColumnName("Item_barcode");

                entity.Property(e => e.ItemId).HasColumnName("Item_id");

                entity.Property(e => e.ItemName)
                    .HasMaxLength(50)
                    .HasColumnName("Item_name");

                entity.Property(e => e.ItemPrice).HasColumnName("Item_price");

                entity.Property(e => e.ItemQuantity).HasColumnName("Item_quantity");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TotalBuyingPrice).HasColumnName("Total_buying_price");

                entity.Property(e => e.UserId).HasColumnName("User_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsConfirmed).HasColumnName("isConfirmed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
