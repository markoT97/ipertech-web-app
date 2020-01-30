using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;

namespace IpertechCompany.DbRepositories
{
    public class TvPacketTvChannelRepository : ITvPacketTvChannelRepository
    {
        private readonly IDbContext _dbContext;

        public TvPacketTvChannelRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(TvPacketTvChannel tvPacketTvChannel)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.TvPacketTvChannel" +
                                         " WHERE TvPacketID = @TvPacketID AND TvChannelID = @TvChannelID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@TvChannelID", SqlDbType.UniqueIdentifier).Value = tvPacketTvChannel.TvChannelId;
                    command.Parameters.AddWithValue("@TvPacketID", SqlDbType.UniqueIdentifier).Value = tvPacketTvChannel.TvPacketId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<TvChannel> Get(Guid tvPacketId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.TvPacketTvChannel" +
                                     " WHERE TvPacketID = @TvPacketID";
                return connection.Query<TvChannel>(query, new { TvPacketID = tvPacketId });
            }
        }

        public TvPacketTvChannel Insert(TvPacketTvChannel tvPacketTvChannel)
        {
            var insertedTvPacketTvChannel = tvPacketTvChannel;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.TvPacketTvChannel (TvPacketID, TvChannelID)" +
                                         " OUTPUT INSERTED.TvChannelID" +
                                         " VALUES(@TvPacketID, @TvChannelID)";
                    command.CommandText = query;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = tvPacketTvChannel.TvPacketId;
                    command.Parameters.Add("@TvChannelID", SqlDbType.UniqueIdentifier).Value = tvPacketTvChannel.TvChannelId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return insertedTvPacketTvChannel;
        }
    }
}
