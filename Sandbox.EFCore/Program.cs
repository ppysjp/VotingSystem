using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.EfCore
{

    public class AppDbContext : DbContext
    {

        public DbSet<Fruit> Fruits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Database");
        }
    }
    
    public class Fruit 
    {
        public string Name { get; set; }
        public int Weight { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new AppDbContext();

            var orange = new Fruit { Name = "Orange" };
            ctx.Fruits.Add(orange);
            ctx.Fruits.Add(new Fruit { Name = "Banana" });

            ctx.SaveChanges();

            var fruit1 = ctx.Fruits.FirstOrDefault();
            var fruit2 = ctx.Fruits.Single(x => x.Name == "Banana");

            Console.ReadLine();
        }
    }
}
