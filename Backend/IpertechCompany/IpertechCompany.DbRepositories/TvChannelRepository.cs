using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using IpertechCompany.DbContext;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;

namespace IpertechCompany.DbRepositories
{
    public class TvChannelRepository : ITvChannelRepository
    {
        private readonly IDbContext _dbContext;

        public TvChannelRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid tvChannelId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.TvChannel" +
                                         " WHERE TvChannelID = @TvChannelID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@TvChannelID", SqlDbType.UniqueIdentifier).Value = tvChannelId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<TvChannel> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.TvChannel";
                return connection.Query<TvChannel>(query);
            }
        }

        public TvChannel Insert(TvChannel tvChannel)
        {
            var insertedTvChannel = new TvChannel();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.TvChannel (Name, ImageLocation, PositionNumber, TvBackwards)" +
                                         " OUTPUT INSERTED.TvChannelID" +
                                         " VALUES(@Name, @ImageLocation, @PositionNumber, @TvBackwards)";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tvChannel.Name;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = tvChannel.ImageLocation;
                    command.Parameters.Add("@PositionNumber", SqlDbType.Int).Value = tvChannel.PositionNumber;
                    command.Parameters.Add("@TvBackwards", SqlDbType.Bit).Value = tvChannel.TvBackwards;

                    connection.Open();
                    insertedTvChannel.TvChannelId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedTvChannel;
        }

        public void Update(TvChannel tvChannel)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.TvChannel SET Name = @Name, ImageLocation = @ImageLocation, " +
                                         "PositionNumber = @PositionNumber, TvBackwards = @TvBackwards" +
                                         " WHERE TvChannelID = @TvChannelID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tvChannel.Name;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = tvChannel.ImageLocation;
                    command.Parameters.Add("@PositionNumber", SqlDbType.Int).Value = tvChannel.PositionNumber;
                    command.Parameters.Add("@TvBackwards", SqlDbType.Bit).Value = tvChannel.TvBackwards;
                    command.Parameters.Add("@TvChannelID", SqlDbType.UniqueIdentifier).Value = tvChannel.TvChannelId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
