using System;
using System.Collections.Generic;
using System.Text;

namespace SQLCore
{
    public class ConnectionBuilder
    {
        /*
        private readonly String ConnectionString = GetConnectionString();
        private readonly Servers _SQLServerName;
        private String _database;
        public ConnectionBuilder(Servers serverName)
        {
            _SQLServerName = serverName;
        }

        private String BuildConnectionString(Servers serverName)
        {
            
            return $"Data Source={ConfigManager.LocalServer};" +
                $"Initial Catalog={ConfigManager.DataBase};" +
                $"Integrated Security={ConfigManager.IntegratedSecurity};";
        }
        private String ChangeDataBase(String dataBaseName)
        {
            String DataSource = String.Empty;
            switch (dataBaseName)
            {
                case Servers.Local:
                    {
                        DataSource = new ChromeDriver(pathToDriverBinary);
                        break;
                    }
                    Ser
                default:
                    {
                        throw new NotImplementedException($"Launch for '{browserType}' is not implemented yet");
                    }
            }

            return $"Data Source={ConfigManager.LocalServer};" +
                $"Initial Catalog={ConfigManager.DataBase};" +
                $"Integrated Security={ConfigManager.IntegratedSecurity};";
        }

        private static string GetConnectionString()
        {
            // To avoid storing the connection string in your code,
            // you can retrieve it from a configuration file.
            return $"Data Source={ConfigManager.LocalServer};" +
                $"Initial Catalog={ConfigManager.DataBase};" +
                $"Integrated Security={ConfigManager.IntegratedSecurity};";
        }
        */
    }
}
