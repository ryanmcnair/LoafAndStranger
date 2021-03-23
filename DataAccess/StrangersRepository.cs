using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using LoafAndStranger.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.DataAccess
{
    public class StrangersRepository
    {
        readonly string ConnectionString;

        public StrangersRepository(IConfiguration config)
        {
            //These both do the same thing
            //ConnectionString = config.GetValue<string>("ConnectionStrings:LoafAndStranger");
            ConnectionString = config.GetConnectionString("LoafAndStranger");
        }

        public IEnumerable<Stranger> GetAll()
        {
            var sql = @"select *
                        From Strangers s
	                        left join Tops t
	                         on s.TopId = t.id
	                        left join Loaves l
	                         on s.LoafId = l.Id";

            using var db = new SqlConnection(ConnectionString);
            var strangers = db.Query<Stranger,Top, Loaf, Stranger>(sql, 
                (stranger, top, loaf) => 
                {
                    stranger.Loaf = loaf;
                    stranger.Top = top;

                    return stranger;
                }, splitOn: "Id");

            return strangers;
        }
    }
}
