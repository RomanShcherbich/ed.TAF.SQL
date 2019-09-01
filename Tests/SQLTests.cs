using NUnit.Framework; 
using SQLCore;
using System;
using System.IO;

namespace tests 
{ 
    [TestFixture] 
    public class unitTestFixture 
    {
        private SqlQuery ExecuteQuery;

        [SetUp]
        public void Setup()
        {
            ExecuteQuery = new SqlQuery();
        }

        [Test]
        public void Select_result_as_table()
        {
            string selectQuery = "SELECT * FROM [tafDB].[dbo].[tblFamily]";
            var result = ExecuteQuery.ResultStringAsTable(selectQuery);
            Assert.Pass(result);
        }

        [Test]
        public void Select_result_as_list()
        {
            string selectQuery = "SELECT * FROM [tafDB].[dbo].[tblFamily]";
            var result = ExecuteQuery.Execute(selectQuery);

            string queryResult = String.Empty;

            for (int i = 0; i < result.Count; i++)
            {
                queryResult += String.Join("\t", result[i]) + "\n";
            }

            Assert.Pass(queryResult);
        }

        [Test] 
        public void Select_from_family() 
        { 

            ExecuteQuery.Execute("UPDATE [tafDB].[dbo].[tblFamily] SET [Location] = 'Rubin' WHERE [Title] = 'husband'");

            string location = ExecuteQuery.Execute("SELECT [Location] FROM [tafDB].[dbo].[tblFamily] WHERE [Title] = 'husband'")[1][0];

            Assert.AreEqual("Rubin", location);
        }
    }
} 
