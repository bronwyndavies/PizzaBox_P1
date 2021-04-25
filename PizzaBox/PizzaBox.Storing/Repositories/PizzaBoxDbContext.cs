using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace PizzaBox.Domain.Entities
{
    public partial class PizzaBoxDbContext : DbContext
    {
       
        public PizzaBoxDbContext()
        {
        }

        public PizzaBoxDbContext(DbContextOptions<PizzaBoxDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Crust> Crusts { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Pizza> Pizzas { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Topping> Toppings { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PizzaTopping> PizzaToppings { get; set; }
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                var builder = new ConfigurationBuilder()
//       .AddJsonFile($"appsettings.json", true, true)
//       ;

//                var config = builder.Build();
//                var connectionString = config["ConnectionString"];
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer(connectionString);
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "Chicago Store" },
                new Store { Id = 2, Name = "New York Store" }
                );
            modelBuilder.Entity<Crust>().HasData(
                new Crust { Id = 1, Name = "Thin Crust", Price = 3m },
                new Crust { Id = 2, Name = "Stuffed Crust", Price = 4m },
                new Crust { Id = 3, Name = "Cauiliflower Crust", Price = 5m },
                new Crust { Id = 4, Name = "Vegan Crust", Price = 6m }
                );
            modelBuilder.Entity<Size>().HasData(
               new Size { Id = 1, Name = "Small", Price = 0m },
               new Size { Id = 2, Name = "Medium", Price = 1m },
               new Size { Id = 3, Name = "Large", Price = 2m }
               );
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, CustomerName = "George Lopez" },
                new Customer { Id = 2, CustomerName = "Jim Carrey" },
                new Customer { Id = 3, CustomerName = "Whoopie Goldburg" },
                new Customer { Id = 4, CustomerName = "Da Baby" }
                );
            
            
            modelBuilder.Entity<Topping>().HasData(
               new Topping { Id = 1, Name = "Pepperoni", Price = 0.5m },
               new Topping { Id = 2, Name = "Peppers", Price = 0.5m },
                new Topping { Id = 3, Name = "Pineapple", Price = 0.5m },
               new Topping { Id = 4, Name = "Peppers", Price = 0.5m },
               new Topping { Id = 5, Name = "Sausage", Price = 1m },
               new Topping { Id = 6, Name = "Bacon", Price = 1m },
               new Topping { Id = 7, Name = "Spinach", Price = 0.5m },
               new Topping { Id = 8, Name = "Olives", Price = 0.5m },
               new Topping { Id = 9, Name = "Anchovies", Price = 1m },
               new Topping { Id = 10, Name = "Extra Cheese", Price = 1m },
               new Topping { Id = 11, Name = "Mozerella Cheese", Price = 0.5m },
               new Topping { Id = 12, Name = "Basil", Price = 0.25m },
               new Topping { Id = 13, Name = "Chicken", Price = 1m },
               new Topping { Id = 14, Name = "BBQ Sauce", Price = 0.75m }


               ) ;
            modelBuilder.Entity<Crust>(entity =>
            {
                entity.ToTable("Crust");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza");

  
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
