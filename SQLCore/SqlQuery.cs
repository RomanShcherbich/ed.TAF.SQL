using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SQLCore 
{ 
    public class SqlQuery 
    {
        private ConnectionBuilder _connectionBuilder;
        private SqlConnection _connection;
        private SqlDataReader _sqlDataReader;

        public SqlQuery()
        {
            _connectionBuilder = new ConnectionBuilder(ConfigManager.LocalServer);
            _connection = new SqlConnection();
            _connection.ConnectionString = _connectionBuilder.GetConnectionString();
            _connection.Open();
        }
        public void ChangeDatabase(string database)
        {
            _connectionBuilder.ChangeDataBase(database);
            _connection.ConnectionString = _connectionBuilder.GetConnectionString();
        }

        public String ResultStringAsTable(String query)
        {
            List<List<string>> table = Execute(query);
            string queryResult = String.Empty;

            for (int i = 0; i < table.Count; i++)
            {
                queryResult += String.Join("\t", table[i]) + "\n";
            }
            return queryResult.TrimEnd(char.Parse("\n"));
        }


        public List<List<string>> Execute(String query) 
        {
            using (_connection)
            {
                SqlCommand selectQuery = new SqlCommand(query, _connection);

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
        public int InsertGetId(String query)
        {
            string sqlExpression = query + ";SET @id=SCOPE_IDENTITY()";
            SqlCommand selectQuery = new SqlCommand(sqlExpression, _connection);

            SqlParameter idParam = new SqlParameter
            {
                ParameterName = "@id",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };
            selectQuery.Parameters.Add(idParam);
            selectQuery.ExecuteNonQuery();
            Console.WriteLine($"New id = {idParam}");
            return (int)idParam.Value;
        }
        public List<List<string>> SelectById(String query, String where)
        {
            string sqlExpression = query + where;

            return Execute(sqlExpression);
        }
    } 
} 
