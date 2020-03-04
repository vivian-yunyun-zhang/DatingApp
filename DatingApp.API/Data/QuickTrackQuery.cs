using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using DatingApp.API.Models;
using MySql.Data.MySqlClient;

namespace DatingApp.API.Data
{
    public class QuickTrackQuery
    {
        public AppDb Db { get; }

        public QuickTrackQuery(AppDb db)
        {
            Db = db;
        }

        public async Task<List<QuickTrack>> FindOneAsync(int id, string job_date)
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT j.id, CAST(j.job_date as char) as job_date, s.stop_number, s.suburb_id, CAST(s.arrived as char) as arrived, CAST(s.departed as char) as departed 
            FROM job j 
            INNER JOIN stop s on j.id = s.job_id and j.job_date = s.job_date 
            WHERE j.id = @id AND 
            j.job_date = @job_date";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@job_date",
                DbType = DbType.String,
                Value = job_date,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        public async Task<List<QuickTrack>> LatestPostsAsync()
        {
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT j.id, CAST(j.job_date as char) as job_date, s.stop_number, s.suburb_id, CAST(s.arrived as char) as arrived, CAST(s.departed as char) as departed 
            FROM job j 
            INNER JOIN stop s on j.id = s.job_id and j.job_date = s.job_date 
            LIMIT 10";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<QuickTrack>> ReadAllAsync(DbDataReader reader)
        {
            var posts = new List<QuickTrack>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var post = new QuickTrack(Db)
                    {
                        id = reader.GetInt32(0),
                        job_date = reader.GetString(1),
                        stop_number = reader.GetInt32(2),
                        suburb_id = reader.GetInt32(3),
                        arrived = reader.GetString(4),
                        departed = reader.GetString(5),
                    };
                    posts.Add(post);
                }
            }
            return posts;
        }
    }
}