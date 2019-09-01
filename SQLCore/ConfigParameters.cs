using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SQLCore
{
    public static class ConfigManager
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(GetSQLCoreDirectory())
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
        public static string LocalServer => Configuration["Data Source Local"];

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

        public static string GetSQLCoreDirectory()
        {
            string current = Directory.GetCurrentDirectory();       // ROOT\Tests\bin\Debug\netcoreapp2.2
            string debug = Directory.GetParent(current).FullName;   // ROOT\Tests\bin\Debug
            string bin = Path.GetDirectoryName(debug);              // ROOT\Tests\bin
            string project = Path.GetDirectoryName(bin);            // ROOT\Tests
            string solution = Path.GetDirectoryName(project);       // ROOT
            string SQLCore = Path.Combine(solution, "SQLCore");     // ROOT\SQLCore

            return SQLCore;
        }
    }
}
