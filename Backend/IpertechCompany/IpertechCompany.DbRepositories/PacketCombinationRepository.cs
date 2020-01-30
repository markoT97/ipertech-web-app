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
    public class PacketCombinationRepository : IPacketCombinationRepository
    {
        private readonly IDbContext _dbContext;

        public PacketCombinationRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid packetCombinationId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.PacketCombination" +
                                         " WHERE PacketCombinationID = @PacketCombinationID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = packetCombinationId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<PacketCombination> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.PacketCombination";
                return connection.Query<PacketCombination>(query);
            }
        }

        public PacketCombination Insert(PacketCombination packetCombination)
        {
            var insertedPacketCombination = new PacketCombination();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.PacketCombination (Name, InternetPacketID, InternetRouterID, TvPacketID, PhonePacketID)" +
                                         " OUTPUT INSERTED.PacketCombinationID" +
                                         " VALUES(@Name, @InternetPacketID, @InternetRouterID, @TvPacketID, @PhonePacketID)";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = packetCombination.Name;
                    command.Parameters.Add("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacketId;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetRouterId;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = (object)packetCombination.TvPacketId ?? DBNull.Value;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = (object)packetCombination.PhonePacketId ?? DBNull.Value;

                    connection.Open();
                    insertedPacketCombination.PacketCombinationId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedPacketCombination;
        }

        public void Update(PacketCombination packetCombination)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.PacketCombination SET Name = @Name, InternetPacketID = @InternetPacketID," +
                                         " InternetRouterID = @InternetRouterID, TvPacketID = @TvPacketID , PhonePacketID = @PhonePacketID)" +
                                         " WHERE PacketCombinationID = @PacketCombinationID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = packetCombination.Name;
                    command.Parameters.Add("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacketId;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetRouterId;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = (object)packetCombination.TvPacketId ?? DBNull.Value;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = (object)packetCombination.PhonePacketId ?? DBNull.Value;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = packetCombination.PacketCombinationId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
