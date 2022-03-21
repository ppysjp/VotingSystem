using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.EfCore
{

    public class AppDbContext : DbContext
    {

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fruit>().Property<int>("Id"); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Database");
        }
    }
    
    public class Fruit 
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public Address Address { get; set; }
    }

    public class Address 
    { 
        public int Id { get; set; }
        public string PostCode { get; set; }
    }


    public class FruitVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new AppDbContext();

            var orange = new Fruit { Name = "Orange" };
            var banana = new Fruit { Name = "Banana" };

            ctx.Fruits.Add(orange);

            var address = new Address { PostCode = "Moon" };

            ctx.Addresses.Add(address);

            var orangeId = ctx.Entry(orange).Property<int>("Id").CurrentValue;

            ctx.Fruits.Add(banana);

            ctx.SaveChanges();

            var fruits = ctx.Fruits
                .Select(x => new FruitVm {
                    Id = EF.Property<int>(x, "Id"),
                    Name = x.Name
                }).ToList();


            Console.ReadLine();
        }
    }
}
