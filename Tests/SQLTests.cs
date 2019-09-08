using NUnit.Framework;
using SQLCore;
using System;
using System.Collections.Generic;
using System.Data;
using Tests.Asserts;

namespace tests 
{ 
    [TestFixture] 
    public class unitTestFixture 
    {
        private DataAsserts _dataAsserts;

        [SetUp]
        public void Setup()
        {
            _dataAsserts = new DataAsserts();
        }

        [Test]
        public void AssertTables()
        {
            string selectExpectedTable = "SELECT * FROM [tafDB].[dbo].[tblExpectedFamily]";
            string selectActuaTable =
                "SELECT [personId],[name]" +
                ",[Surname] = CASE WHEN [personId] = 2 THEN 'INVALID' ELSE [Surname] END" +
                ",[Title] = CASE WHEN [personId] = 1 THEN 'FATHER' ELSE [Title] END" +
                ",[Location] " +
                "FROM [tafDB].[dbo].[tblFamily] " +
                "WHERE [personId] < 4";


            _dataAsserts.AssertDataTable(selectExpectedTable, selectActuaTable);
        }


        [Test]
        public void GetDataTable()
        {
            string selectQuery = "SELECT * FROM [tafDB].[dbo].[tblFamily]";

            Assert.Pass(_dataAsserts.ExecuteToDataSet(selectQuery));
        }


        [Test]
        public void GetNewRowById()
        {
            string insertQuery =
                "INSERT INTO[tafDB].[dbo].[tblFamily]" +
                "([name],[Surname],[Title],[Location]) VALUES" +
                "('Egor','Scherbich','brother','Kahovskaya')";

            var idNew = _dataAsserts.InsertGetId(insertQuery);

            string selectQuery = "SELECT * FROM [tafDB].[dbo].[tblFamily]";

            var result = _dataAsserts.SelectById(selectQuery, $" WHERE personId={idNew}");
            CollectionAssert.AreEqual(new List<string>() { idNew.ToString(),"Egor", "Scherbich", "brother", "Kahovskaya" }, result[1]);
        }

        [Test]
        public void Select_result_as_table()
        {
            string selectQuery = "SELECT * FROM [accountant].[dbo].[DAY_COUNT]";
            var result = _dataAsserts.ResultStringAsTable(selectQuery);
            Assert.Pass(result);
        }

        [Test]
        public void Select_result_as_list()
        {
            string selectQuery = "SELECT * FROM [tafDB].[dbo].[tblFamily]";
            var result = _dataAsserts.Execute(selectQuery);

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

            _dataAsserts.Execute("UPDATE [tafDB].[dbo].[tblFamily] SET [Location] = 'Rubin' WHERE [Title] = 'husband'");

            string location = _dataAsserts.Execute("SELECT [Location] FROM [tafDB].[dbo].[tblFamily] WHERE [Title] = 'husband'")[1][0];

            Assert.AreEqual("Rubin", location);
        }
    }
} 
