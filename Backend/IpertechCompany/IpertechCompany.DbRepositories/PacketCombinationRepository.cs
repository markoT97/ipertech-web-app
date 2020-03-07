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
                const string query = "SELECT pc.PacketCombinationID, pc.Name, [ip].*, tp.*, ph.*" +
                " FROM packets.PacketCombination pc" +
                " INNER JOIN packets.InternetPacket [ip] ON pc.InternetPacketID = [ip].InternetPacketID" +
                " LEFT JOIN packets.TvPacket tp ON pc.TvPacketID = tp.TvPacketID " +
                " LEFT JOIN packets.PhonePacket ph ON pc.PhonePacketID = ph.PhonePacketID" +
                " ORDER BY [ip].price, tp.price, ph.price";
                return connection.Query<PacketCombination, InternetPacket, TvPacket, PhonePacket, PacketCombination>(query,
                    (packet, internet, tv, phone) =>
                    {
                        packet.InternetPacket = internet;
                        packet.TvPacket = tv;
                        packet.PhonePacket = phone;
                        return packet;
                    }, splitOn: "InternetPacketID, TvPacketID, PhonePacketID");
            }
        }

        public PacketCombination Insert(PacketCombination packetCombination)
        {
            var insertedPacketCombination = packetCombination;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.PacketCombination (PacketCombinationID, Name, InternetPacketID, InternetRouterID, TvPacketID, PhonePacketID)" +
                                         " OUTPUT INSERTED.PacketCombinationID" +
                                         " VALUES(@PacketCombinationID, @Name, @InternetPacketID, @InternetRouterID, @TvPacketID, @PhonePacketID)";
                    command.CommandText = query;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = packetCombination.PacketCombinationId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = packetCombination.Name;
                    command.Parameters.Add("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacket.InternetPacketId;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacket.InternetRouter.InternetRouterId;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.TvPacket?.TvPacketId ?? (object)DBNull.Value;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.PhonePacket?.PhonePacketId ?? (object)DBNull.Value;

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
                                         " InternetRouterID = @InternetRouterID, TvPacketID = @TvPacketID , PhonePacketID = @PhonePacketID" +
                                         " WHERE PacketCombinationID = @PacketCombinationID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = packetCombination.Name;
                    command.Parameters.Add("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacket.InternetPacketId;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = packetCombination.InternetPacket.InternetRouter.InternetRouterId;
                    command.Parameters.Add("@TvPacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.TvPacket?.TvPacketId ?? (object)DBNull.Value;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = packetCombination.PhonePacket?.PhonePacketId ?? (object)DBNull.Value;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = packetCombination.PacketCombinationId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
