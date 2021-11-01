using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace CsvImport.EntityFramework
{
    public class MigrationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            var projectDir = GetApplicationRoot();
            var builder = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            return builder.Build();
        }

        private static string GetApplicationRoot()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var regex = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = regex.Match(path).Value;
            return appRoot;
        }
    }
}
