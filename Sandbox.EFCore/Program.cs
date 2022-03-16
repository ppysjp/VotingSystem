using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sandbox.EfCore
{

    public class AppDbContext : DbContext
    {
        public DbSet<Fruit> Fruits { get; set; }
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
            IEnumerable<Fruit> fruits = new[] { new Fruit { Name = "Banana", Weight = 1 } }; 
        }
    }
}
