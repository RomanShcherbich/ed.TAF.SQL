using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SQLCore;

namespace Tests.Asserts
{
    public class DataAsserts : SqlQuery

    {

        public void AssertDataTable(string expectedQuery, string actualQuery)
        {
            using (_connection)
            {
                DataTable expected = getDataTable(expectedQuery);
                DataTable actual = getDataTable(actualQuery);

                List<string> expectedColumns = columnsToList(expected);
                List<string> actualColumns = columnsToList(actual);

                Assert.AreEqual(expected.Columns.Count, actual.Columns.Count, "Tables contain different columns count");
                Console.WriteLine("Tables have the same columns count");

                CollectionAssert.AreEqual(expectedColumns, actualColumns);
                Console.WriteLine("Tables have the same columns names");


                int rowComparingCount = expected.Rows.Count > actual.Rows.Count ? expected.Rows.Count : actual.Rows.Count;

                string rowErrorsLog = string.Empty;

                List<List<string>> expectedRows = rowsToList(expected);
                List<List<string>> actualRows = rowsToList(actual);

                if (expectedRows.Count <= rowComparingCount || actualRows.Count <= rowComparingCount)
                {
                    rowErrorsLog += "Row count: Actual - " + actualRows.Count + " ; Expected - " + expectedRows.Count + "\n";
                }

                for (int i = 0; i < rowComparingCount; i++ )
                {
                    string rowNumber = "Row #" + (i + 1);

                    if (expectedRows.Count <= i)
                    {
                        rowErrorsLog += rowNumber + " expected is empty" + "\n";
                    }
                    else if (actualRows.Count <= i)
                    {
                        rowErrorsLog += rowNumber + " actual is empty" + "\n";
                    }
                    else
                    {
                        List<string> expectedRow = expectedRows[i];
                        List<string> actualRow = actualRows[i];
                        for (int r = 0; r < expectedColumns.Count; r++)
                        {
                            if (expectedRow[r] != actualRow[r])
                            {
                                rowErrorsLog += rowNumber + " [" + expectedColumns[r] + "]:"
                                    + " actual = " + actualRow[r]
                                    + " ; expected = " + expectedRow[r] + "\n";
                            }
                        }
                    }
                }
                if(rowErrorsLog != string.Empty)
                {
                    Assert.Fail("ERROR there is some difference:\n" + rowErrorsLog.Trim());
                }
                Console.WriteLine("Tables have the same rows count");
                Console.WriteLine("Tables have the same rows values");
                Assert.AreEqual(expected.Rows.Count, actual.Rows.Count, "Tables contain different rows count");

                //CollectionAssert.AreEqual(expected.Columns, actual.Columns);

            }
        }
        public List<string> columnsToList(DataTable dataTable)
        {
            List<string> expectedColumns = new List<string> { };
            foreach (DataColumn column in dataTable.Columns)
            {
                expectedColumns.Add(column.ColumnName);
            }

            return expectedColumns;
        }

        public List<List<string>> rowsToList(DataTable dataTable)
        {
            List<List<string>> listRows = new List<List<string>> { };

            foreach (DataRow rows in dataTable.Rows)
            {
                List<string> row = new List<string> { };
                foreach (object cell in rows.ItemArray)
                {
                    row.Add(string.Format("{0}", cell));
                }
                listRows.Add(row);
            }

            return listRows;
        }

    }
}
