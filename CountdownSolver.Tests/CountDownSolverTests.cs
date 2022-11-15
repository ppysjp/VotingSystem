using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using Xunit;
using static Xunit.Assert;

namespace CountdownSolver.Tests
{
    public class CountDownSolverTests
    {

        [Fact]
        public void GenerateTwoTile_Add_TwoTilesReturnsCompound()
        {
            var tile1 = new Tile() { Type = "Small", Value = 1 };
            var tile2 = new Tile() { Type = "Small", Value = 1 };
            var factory = new CompoundTileFactory();
            var add = new ArithmeticTile() { Name = "Add", F = (a, b) => a + b };

            var compound = factory.GenerateTwoTile(tile1, tile2, add);

            Assert.Equal(2, compound.Value);
            Assert.Equal("TwoSmallTiles", compound.Type);
            Assert.Equal("Small", compound.Tile1.Type);
            Assert.Equal("Small", compound.Tile2.Type);
            Assert.Equal(1, compound.Tile1.Value);
            Assert.Equal(1, compound.Tile2.Value);
            Assert.Equal("Add", compound.Lambda.Name);
        }


        [Fact]
        public void GenerateTwoTile_Sub_TwoTilesReturnsCompound()
        {
            var tile1 = new Tile() { Type = "Small", Value = 3 };
            var tile2 = new Tile() { Type = "Small", Value = 2 };
            var factory = new CompoundTileFactory();
            var add = new ArithmeticTile() { Name = "Sub", F = (a, b) => a - b };

            var compound = factory.GenerateTwoTile(tile1, tile2, add);

            Assert.Equal(1, compound.Value);
            Assert.Equal("TwoSmallTiles", compound.Type);
            Assert.Equal("Small", compound.Tile1.Type);
            Assert.Equal("Small", compound.Tile2.Type);
            Assert.Equal(3, compound.Tile1.Value);
            Assert.Equal(2, compound.Tile2.Value);
            Assert.Equal("Sub", compound.Lambda.Name);
        }


        [Fact]
        public void GenerateThreeTile_Add__Add_ThreeTilesReturnsCompound()
        {
            var tile1 = new Tile() { Type = "Small", Value = 1 };
            var tile2 = new Tile() { Type = "Small", Value = 1 };
            var tile3 = new Tile() { Type = "Small", Value = 1 };
            var factory = new CompoundTileFactory();
            var add = new ArithmeticTile() { Name = "Add", F = (a, b) => a + b };

            var compound = factory.GenerateThreeTile(tile1, tile2, tile3, add, add);

            Assert.Equal(3, compound.Value);
            Assert.Equal("ThreeSmallTiles", compound.Type);
            Assert.Equal("Small", compound.Tile1.Type);
            Assert.Equal("Small", compound.Tile2.Type);
            Assert.Equal("Small", compound.Tile3.Type);
            Assert.Equal(1, compound.Tile1.Value);
            Assert.Equal(1, compound.Tile2.Value);
            Assert.Equal(1, compound.Tile3.Value);
            Assert.Equal("Add", compound.Lambda1.Name);
            Assert.Equal("Add", compound.Lambda2.Name);

        }
    }

    public class ArithmeticTile
    {
        public ArithmeticTile()
        {
        }

        public string Name { get; set; }

        public Func<int, int, int> F { get; set; }
    }


    public class CompoundTileFactory
    {
        public CompoundTileFactory()
        {
        }

        public TwoTile GenerateTwoTile(Tile tile1, Tile tile2, ArithmeticTile lambda)
        {
            return new TwoTile()
            {
                Tile1 = tile1,
                Tile2 = tile2,
                Type = "TwoSmallTiles",
                Value = lambda.F(tile1.Value, tile2.Value),
                Lambda = lambda
            };
        }

        public ThreeTile GenerateThreeTile(Tile tile1, Tile tile2, Tile tile3, ArithmeticTile add1, ArithmeticTile add2)
        {
            return new ThreeTile()
            {
                Tile1 = tile1,
                Tile2 = tile2,
                Tile3 = tile3,
                Type = "ThreeSmallTiles",
                Value = add2.F(add1.F(tile1.Value, tile2.Value), tile3.Value),
                Lambda1 = add1, 
                Lambda2 = add2 
            };
        }
    }

    public class Tile
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public Tile()
        {
        }
    }

    public class ThreeTile
    {
        public Tile Tile1 { get; set; }
        public Tile Tile2 { get; set; }
        public Tile Tile3 { get; set; }

        public string Type { get; set; }
        public int Value { get; set; }
        
        public ArithmeticTile Lambda1 { get; set; }
        public ArithmeticTile Lambda2{ get; set; }
    }

    public class TwoTile
    {
        public TwoTile()
        {
        }

        public Tile Tile1 { get; set; }
        public Tile Tile2 { get; set; }

        public string Type { get; set; }
        public int Value { get; set; }
        
        public ArithmeticTile Lambda { get; set; }
    }

}
