using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcDemo.Data
{
    public class MvcDemoDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        public DbSet<Product> Products { get; set; }

        // Pour être configurable avec un DbContextOptionsBuilder,
        // le DbContext DOIT avoir un constructeur qui accepte un DbContextOptions<MvcDemoDbContext>
        // et appelle base(options)
        public MvcDemoDbContext(DbContextOptions<MvcDemoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Si quantité non précisée dans une ligne de commande, quantité = 1
            modelBuilder.Entity<OrderLine>()
                        .Property(b => b.Quantity)
                        .HasDefaultValue(1);

            // Les noms de pays doivent être uniques
            modelBuilder.Entity<Country>()
                        .HasAlternateKey(c => c.Name)
                        .HasName("Unique_CountryName");

            // Index sur les villes
            modelBuilder.Entity<Customer>()
                        .HasIndex(b => b.City)
                        .HasName("Idx_CustomerCity");

            AddSeedData(modelBuilder);
        }

        private void AddSeedData(ModelBuilder modelBuilder)
        {
            var countryFR = new { Id = 1, Name = "France" };
            var countryPO = new { Id = 2, Name = "Portugal" };
            var countryIT = new { Id = 3, Name = "Italy" };

            modelBuilder.Entity<Country>().HasData(new[] { countryFR, countryPO, countryIT });

            var customerFR = new { Id = 1, Name = "Jean", Address = "1, place Bellecour", ZipCode = "69002", City = "Lyon", CountryId = countryFR.Id };
            var customerPO = new { Id = 2, Name = "João", Address = "31, rua Humberto Delgado", ZipCode = "8700", City = "Olhão", CountryId = countryPO.Id };
            var customerIT = new { Id = 3, Name = "Giovanni", Address = "7, vico delle mele", ZipCode = "16123", City = "Genova", CountryId = countryPO.Id };

            var customers = new[] { customerFR, customerPO, customerIT };
            modelBuilder.Entity<Customer>().HasData(customers);

            var product1 = new Product { Id = 1, Name = "Spaghetti", UnitPrice = 1.25m };
            var product2 = new Product { Id = 2, Name = "Pomodori (kg)", UnitPrice = 1.80m };
            var product3 = new Product { Id = 3, Name = "Folar", UnitPrice = 4.30m };
            var product4 = new Product { Id = 4, Name = "Pastel de nata", UnitPrice = 0.80m };
            var product5 = new Product { Id = 5, Name = "Macaron", UnitPrice = 2.50m };
            var product6 = new Product { Id = 6, Name = "Rosette", UnitPrice = 9.40m };

            var products = new Product[] { product1, product2, product3, product4, product5, product6 };
            modelBuilder.Entity<Product>().HasData(products);


            ArrayList headers = new ArrayList();
            ArrayList lines = new ArrayList();

            var rnd = new Random();

            int currentHeaderId = 1;
            int currentLineId = 1;

            for (int i = 0; i < 10; i++)
            {
                foreach (var cust in customers)
                {
                    var total = 0.0m;

                    foreach (var prod in products)
                    {
                        var q = rnd.Next(0, 4);
                        if (q == 0) continue;

                        var line = new { Id = currentLineId, ProductId = prod.Id, Quantity = q, UnitPrice = prod.UnitPrice * (rnd.Next(90, 100) / 100.00m), OrderHeaderId = currentHeaderId };
                        total += line.UnitPrice * q;

                        lines.Add(line);

                        currentLineId++;
                    }

                    var header = new { Id = currentHeaderId, CustomerId = cust.Id, Date = DateTime.Today, TotalAmount = total };

                    headers.Add(header);

                    currentHeaderId++;
                }
            }

            modelBuilder.Entity<OrderHeader>().HasData(headers.ToArray());
            modelBuilder.Entity<OrderLine>().HasData(lines.ToArray());
        }
    }
}
