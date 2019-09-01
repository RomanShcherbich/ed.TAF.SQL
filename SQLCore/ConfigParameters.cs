using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SQLCore
{
    public static class ConfigManager
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().
            SetBasePath(GetProjectDirectory())
#if DEBUG
            .AddJsonFile("SqlConnectionConfig.json")
#endif
            .Build();

        /// <summary>
        /// Gets Data Source (SQLServer address).
        /// </summary>
        /// <value>
        /// The Data Source.
        /// </value>
        public static string LocalServer => Configuration[":Local Data Source"];

        /// <summary>
        /// Gets DataBase name.
        /// </summary>
        /// <value>
        /// The Data Source.
        /// </value>
        public static string DataBase => Configuration["Initial Catalog"];

        /// <summary>
        /// Gets Integrated Security.
        /// </summary>
        /// <value>
        /// The Integrated Security.
        /// </value>
        public static string IntegratedSecurity => Configuration["Integrated Security"];

        public static string GetProjectDirectory()
        {
            string current = Directory.GetCurrentDirectory();
            string debug = Directory.GetParent(current).FullName;
            string bin = Directory.GetParent(debug).FullName;
            string project = Directory.GetParent(bin).FullName;
            return project;
        }
    }
}
