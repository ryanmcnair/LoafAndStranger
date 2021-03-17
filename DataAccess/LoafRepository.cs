using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoafAndStranger.Models;
using Microsoft.Data.SqlClient;
using Dapper;

namespace LoafAndStranger.DataAccess
{
    public class LoafRepository
    {
        const string ConnectionString = "Server=localhost;Database=LoafAndStranger;Trusted_Connection=True";

       
        public List<Loaf> GetAll()
        {
            var loaves = new List<Loaf>();

            //create a connection
            using var db = new SqlConnection(ConnectionString);

            //telling the command what you want to do
            var sql = @"Select *
                        From Loaves";

            var results = db.Query<Loaf>(sql).ToList();

            return results;
        }

        public void Add(Loaf loaf)
        {

            var sql = @"INSERT INTO [Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                        OUTPUT inserted.Id
                        VALUES(@Size, @type, @weightInOunces, @Price, @Sliced)";
            using var db = new SqlConnection(ConnectionString);

            var id = db.ExecuteScalar<int>(sql, loaf);

            loaf.Id = id;

            /*var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO [Loaves] ([Size],[Type],[WeightInOunces],[Price],[Sliced])
                                OUTPUT inserted.Id
                                VALUES(@Size, @type, @weightInOunces, @Price, @Sliced)";

            cmd.Parameters.AddWithValue("Size", loaf.Size);
            cmd.Parameters.AddWithValue("type", loaf.Type);
            cmd.Parameters.AddWithValue("weightInOunces", loaf.WeightInOunces);
            cmd.Parameters.AddWithValue("Price", loaf.Price);
            cmd.Parameters.AddWithValue("Sliced", loaf.Sliced);

            var id = (int)cmd.ExecuteScalar();

            loaf.Id = id;
            */
            //var biggestExistingId = _loaves.Max(l => l.Id); -- This functionality is handled by SQL now
            //loaf.Id = biggestExistingId + 1;
            //_loaves.Add(loaf);
        }

        public Loaf Get(int id)
        {

            var sql = $@"Select *
                        From Loaves
                        where Id = @Id";

            //create a connection
            using var db = new SqlConnection(ConnectionString);

            var loaf = db.QueryFirstOrDefault<Loaf>(sql, new { id = id });

            return loaf;

            //create a command
            /*
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.AddWithValue("Id", id);

            //execute the command
            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                var Loaf = MapLoaf(reader);
                return Loaf;
            }

            return null;
            */
            //var loaf = _loaves.FirstOrDefault(bread => bread.Id == id);
            //return loaf;
        }

        public void Remove(int id)
        {
            using var db = new SqlConnection(ConnectionString);
            
            var sql = @"Delete 
                        From Loaves 
                        Where id = @id";

            db.Execute(sql, new { id });

            //var loafToRemove = Get(id);
            //_loaves.Remove(loafToRemove);
        }

        
    }
}
