using System;
using System.Collections.Generic;
using Xunit;

namespace VotingSystem.Tests
{
    public class MathOne 
    {
        private readonly ITestOne _testOne;

        public MathOne(ITestOne testOne)
        {
            _testOne = testOne; 
        }
        
        public int Add(int a, int b) => _testOne.Add(a, b);
    }


    public class MathOneTests 
    { 
       [Fact] 
       public void MathOneAddsTwoNumbers() 
       {
            var testOne = new TestOne();
            var mathOne = new MathOne(testOne);
            Assert.Equal(2, mathOne.Add(1, 1));

       }
    }

    public class TestOne : ITestOne 
    {
        public int Add(int a, int b) => a + b;
    }

    public interface ITestOne 
    {
        public int Add(int a, int b);
    }

    public class TestOneTests
    {
        [Fact]
        public void Add_AddsTwoNumbersTogether()
        {
            var result = new TestOne().Add(1, 1);
            Assert.Equal(2, result);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 0, 1)]
        [InlineData(0, 1, 1)]
        public void Add_AddsTwoNumbersTogether_Theory(int a, int b, int expected)
        {
            var result = new TestOne().Add(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]    
        public void TestListContainsValue() 
        {
            var list = new List<int> { 1, 2, 3, 4, 5 };
            Assert.Contains(1, list); 
        }

    }
}
