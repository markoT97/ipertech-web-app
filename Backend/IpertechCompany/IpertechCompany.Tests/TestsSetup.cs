using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using IpertechCompany.DbConnection;
using NUnit.Framework;

namespace IpertechCompany.Tests
{
    [SetUpFixture]
    public class TestsSetup
    {
        private readonly IDbContext _dbContext;

        public TestsSetup()
        {
            _dbContext = new DbContext("Data Source=DESKTOP-883AG4N\\SQLEXPRESS;Initial Catalog=IpertechCompany;Integrated Security=True");
        }

        [OneTimeSetUp]
        public void InitializeDatabase()
        {
            // Inicijalizacija baze
            try
            {
                // For using 1252 encoding
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var ddl = File.ReadAllText(@"D:\Marko\Projekti\onion-architecture-web-app-with-r-db\database\db-scripts\ddl-script.sql", Encoding.GetEncoding(1252));
                var dml = File.ReadAllText(@"D:\Marko\Projekti\onion-architecture-web-app-with-r-db\database\db-scripts\dml-script.sql", Encoding.GetEncoding(1252));

                using (var connection = _dbContext.Connect())
                {
                    IEnumerable<string> commandStrings = Regex.Split(ddl, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    connection.Open();

                    foreach (var commandString in commandStrings)
                    {
                        if (commandString.Trim() != "")
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = commandString;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    commandStrings = Regex.Split(dml, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                    foreach (var commandString in commandStrings)
                    {
                        if (commandString.Trim() != "")
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = commandString;
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Cannot find SQL files.");
            }
        }
    }
}
