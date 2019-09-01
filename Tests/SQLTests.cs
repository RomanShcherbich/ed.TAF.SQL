using NUnit.Framework; 
using SQLCore; 
namespace tests 
{ 
    [TestFixture] 
    public class unitTestFixture 
    {
        private SQLClass ExecuteQuery;

        [SetUp]
        public void Setup()
        {
            ExecuteQuery = new SQLClass();
        }

        [Test] 
        public void Select_from_family() 
        { 
            var result = ExecuteQuery.Execute("SELECT * FROM [tafDB].[dbo].[tblFamily]"); 
            Assert.Pass(result); 
        }

        //[TestCase(-1)] 
        //[TestCase(0)] 
        //[TestCase(1)] 
        //public void IsPrime_ValuesLessThan2_ReturnFalse(int value) 
        //{ 
        //	SQLClass primeService = new SQLClass(); 
        //	var result = primeService.IsPrime(value); 
        //	Assert.IsFalse(result, $"{value} should not be prime"); 
        //} 
    }
} 
