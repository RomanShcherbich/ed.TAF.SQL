using System;
using System.Collections.Generic;
using System.Text;

namespace SQLCore
{
    public class ConnectionBuilder
    {
        private readonly string ConnectionString;
        private readonly string _SQLServerName;

        private string _serverName;
        private string _database;
        private string _security;
        public ConnectionBuilder(String serverName)
        {
            _SQLServerName = serverName;
            _database = ConfigManager.DataBase;
            _security = ConfigManager.IntegratedSecurity;
            ConnectionString = ChangeDataBase(serverName);
        }

        private String BuildConnectionString(string serverName)
        {
            
            return $"Data Source={ConfigManager.LocalServer};" +
                $"Initial Catalog={ConfigManager.DataBase};" +
                $"Integrated Security={ConfigManager.IntegratedSecurity};";
        }
        private String ChangeDataBase(String serverName)
        {
            //switch (serverName)
            //{
            //    case Servers.Local:
            //        {
            //            _serverName = ConfigManager.LocalServer.ToString();
            //            break;
            //        }
            //    default:
            //        {
            //            throw new NotImplementedException($"Connection string for '{_SQLServerName}' is not implemented yet");
            //        }
            //}

            return $"Data Source={serverName};" +
                $"Initial Catalog={_database};" +
                $"Integrated Security={_security};";
        }

        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
