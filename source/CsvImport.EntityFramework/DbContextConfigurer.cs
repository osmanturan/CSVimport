using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CsvImport.EntityFramework
{
    public static class DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder builder, string connectionString)
        {
            if (connectionString.ToLowerInvariant().Contains("inMemory".ToLowerInvariant()))
            {
                builder.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

                var start = connectionString.ToLowerInvariant().IndexOf("Database=".ToLowerInvariant()) + "Database=".Length;
                var end = connectionString.ToLowerInvariant().IndexOf(";".ToLowerInvariant(), start);
                var dbName = connectionString.Substring(start, end - start);

                builder.UseInMemoryDatabase(dbName);
            }
            else
            {
                builder.UseSqlServer(connectionString);
            }
        }
    }
}
