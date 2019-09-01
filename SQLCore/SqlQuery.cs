using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace SQLCore 
{ 
    public class SqlQuery 
    {
        private ConnectionBuilder connectionBuilder;
        private SqlConnection _connection;

        public SqlQuery()
        {
            connectionBuilder = new ConnectionBuilder(ConfigManager.LocalServer);
            _connection = new SqlConnection();
            _connection.ConnectionString = connectionBuilder.GetConnectionString();
        }

        public String ResultStringAsTable(string query)
        {
            using (_connection)
            {
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("Columns count = {0}", reader.VisibleFieldCount);
                    Console.WriteLine("Rows count = {0}", reader.FieldCount);

                    List<String> Table = new List<string>();
                    List<String> columns = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i));
                    }

                    List<object> rowValues = new List<object>();

                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.VisibleFieldCount; i++)
                        {
                            var x = reader.GetValue(i);
                            if (i == 0)
                            {
                                x = "\n" + x;
                            }
                            rowValues.Add(x);
                        }
                    }
                    return String.Join("\t", columns) + "\n" + String.Join("\t", rowValues);
                } else
                {
                    Console.WriteLine("Resul is empty");
                }
            }
            return "Resul is empty";
        }


        public List<List<string>> Execute(String query) 
        {
            SqlConnection local = new SqlConnection();
            connectionBuilder = new ConnectionBuilder(ConfigManager.LocalServer);
            local.ConnectionString = connectionBuilder.GetConnectionString();

            using (local)
            {
                local.Open();
                SqlCommand selectQuery = new SqlCommand(query, local);

                using (SqlDataReader reader = selectQuery.ExecuteReader())
                {
                    List<List<string>> Table = new List<List<string>>();
                    List<string> columns = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i));
                    }
                    Table.Add(columns);


                    while (reader.Read())
                    {

                        List<object> rowValues = new List<object>();
                        for (int i = 0; i < reader.VisibleFieldCount; i++)
                        {
                            var x = reader.GetValue(i);
                            string objToString = x.ToString();
                            string objToString2 = String.Format("{0}", x);

                            rowValues.Add(x);
                        }

                        Table.Add(rowValues.Select(x => String.Format("{0}",x)).ToList());
                    }
                    return Table;
                }
            }
        }
    } 
} 
