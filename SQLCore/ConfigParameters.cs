using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SQLCore
{
    public static class ConfigManager
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().
            SetBasePath(
            //Path.Combine(
            //Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(
            //    // ROOT\Tests\bin\Debug\netcoreapp2.2
            //    Directory.GetCurrentDirectory()
            //    ))))
            //    ,"SQLCore")
             //Directory.GetParent(Directory.GetCurrentDirectory())
             //Directory.GetCurrentDirectory()

             GetSQLCoreDirectory()
             )
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
        public static string LocalServer
        {
            get
            {
                var serverName = Configuration["Data Source Local"];
                return serverName;
            }
        }
        //public static string LocalServer => Configuration["Data Source Local"];
        //public static Servers LocalServer
        //{
        //    get
        //    {
        //        var serverName = Configuration["Data Source Local"];
        //        return (Servers)Enum.Parse(typeof(Servers), serverName);
        //    }
        //}

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
            string current = Directory.GetCurrentDirectory();
            string debug = Directory.GetParent(current).FullName;
            string bin = Path.GetDirectoryName(debug);
            string project = Path.GetDirectoryName(bin);
            string solution = Path.GetDirectoryName(project);
            string SQLCore = Path.Combine(solution, "SQLCore");

            return SQLCore;
        }
    }
}
