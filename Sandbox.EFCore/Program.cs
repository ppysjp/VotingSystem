using System;
using System.Collections.Generic;
using System.Linq;
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
            var ctx = new AppDbContext();
            var fruit = ctx.Fruits.FirstOrDefault();

            Console.ReadLine();
        }
    }
}
