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

            int orangeId = 0;

            using (var ctx = new AppDbContext())
            {
                var orange = new Fruit { Name = "Orange" };
                var banana = new Fruit { Name = "Banana" };

                ctx.Fruits.Add(orange);

                orangeId = ctx.Entry(orange).Property<int>("Id").CurrentValue;

                ctx.Fruits.Add(banana);

                //orange.Address = Address;

                ctx.SaveChanges();
            }

            using (var ctx = new AppDbContext())
            {
                var address = new Address { PostCode = "Moon" };

                ctx.Addresses.Add(address);
                
                ctx.SaveChanges();
            }

            using (var ctx = new AppDbContext())
            {
                var fruits = ctx.Fruits
                    .Include(x => x.Address)
                    .ToList();

                var addresses = ctx.Addresses.ToList();
            }


            Console.ReadLine();
        }
    }
}
