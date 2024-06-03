using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace TestSolution.SourceGenerator
{
    public class PostgreDataProvider
    {
        public List<string> GetSimpleData()
        {
            using (var connection =
                   new NpgsqlConnection("Host=localhost;Username=postgres;Password=admin;Database=testdb"))
            {
                var compiler = new PostgresCompiler();
                var db = new QueryFactory(connection, compiler);

                var result = db.Query("classes").Select("name").Get<string>().ToList();
                result = result.Select(x => x.Trim()).ToList();
                return result;
            }
        }
    }
}