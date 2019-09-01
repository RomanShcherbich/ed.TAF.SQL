using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
namespace SQLCore 
{ 
    public class SQLClass 
    { 
        public String Execute(String query) 
        {
            SqlConnection local = new SqlConnection();

            local.ConnectionString = GetConnectionString();

            using (local)
            {
                local.Open();
                SqlCommand selectQuery = new SqlCommand(query, local);

                using (SqlDataReader reader = selectQuery.ExecuteReader())
                {
                    Console.WriteLine("Columns count = {0}", reader.VisibleFieldCount);
                    Console.WriteLine("Rows count = {0}", reader.FieldCount);

                    List<String> Table = new List<string>();
                    List<String> columns = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i));
                    }
                    Console.WriteLine(String.Join("\t", columns));

                    List<object> rowValues = new List<object>();

                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.VisibleFieldCount; i++)
                        {
                            var x = reader.GetValue(i);
                            if(i == 0)
                            {
                                x = "\n" + x;
                            }
                            rowValues.Add(x);
                        }
                    }
                    Console.WriteLine(String.Join("\t", rowValues));
                    return String.Join("\t", columns) + "\n" + String.Join("\t", rowValues);
                }
            }
        }

        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return "Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=tafDB;" +
                "Integrated Security=SSPI;";
        }
    } 
} 
