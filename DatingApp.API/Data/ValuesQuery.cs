using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DatingApp.API.Models;
using MySql.Data.MySqlClient;

namespace DatingApp.API.Data
{
    public class ValuesQuery
    {
        public AppDb Db { get; }

        public ValuesQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<Value> FindOneAsync(int id)
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name` FROM `values` WHERE `Id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Value>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT `Id`, `Name` FROM `values` ORDER BY `Id` DESC LIMIT 10;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task DeleteAllAsync()
        {
            var txn = await Db.Connection.BeginTransactionAsync();
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `values`";
            await cmd.ExecuteNonQueryAsync();
            await txn.CommitAsync();
        }

        private async Task<List<Value>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<Value>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new Value(Db)
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1)
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}