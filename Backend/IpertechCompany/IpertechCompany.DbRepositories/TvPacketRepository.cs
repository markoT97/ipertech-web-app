using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IpertechCompany.DbRepositories
{
    public class TvPacketRepository : ITvPacketRepository
    {
        private readonly IDbContext _dbContext;

        public TvPacketRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid tvPacketId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.TvPacket" +
                                         " WHERE TvPacketID = @TvPacketID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@TvPacketID", SqlDbType.UniqueIdentifier).Value = tvPacketId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<TvPacket> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.TvPacket tp" +
                    " INNER JOIN packets.TvPacketTvChannel tptc ON tp.TvPacketID = tptc.TvPacketID" +
                    " INNER JOIN packets.TvChannel tc ON tc.TvChannelID = tptc.TvChannelID";
                return connection.Query<TvPacket, TvChannel, TvPacket>(query, (tvPacket, tvChannel) =>
                {
                    tvPacket.TvChannels.Add(tvChannel);
                    return tvPacket;
                }, splitOn: "TvChannelID");
            }
        }

        public TvPacket Insert(TvPacket tvPacket)
        {
            var insertedTvPacket = tvPacket;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.TvPacket (TvPacketID, Name, Price)" +
                                         " OUTPUT INSERTED.TvPacketID" +
                                         " VALUES(@TvPacketID, @Name, @Price)";
                    command.CommandText = query;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = tvPacket.TvPacketId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tvPacket.Name;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = tvPacket.Price;

                    connection.Open();
                    insertedTvPacket.TvPacketId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedTvPacket;
        }

        public void Update(TvPacket tvPacket)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.TvPacket SET Name = @Name, Price = @Price" +
                                         " WHERE TvPacketID = @TvPacketID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = tvPacket.Name;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = tvPacket.Price;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = tvPacket.TvPacketId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
