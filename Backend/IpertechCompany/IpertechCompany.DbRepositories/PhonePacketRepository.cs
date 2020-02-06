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
    public class PhonePacketRepository : IPhonePacketRepository
    {
        private readonly IDbContext _dbContext;

        public PhonePacketRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid phonePacketId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.PhonePacket" +
                                         " WHERE PhonePacketID = @PhonePacketID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = phonePacketId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<PhonePacket> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.PhonePacket";
                return connection.Query<PhonePacket>(query);
            }
        }

        public PhonePacket Insert(PhonePacket phonePacket)
        {
            var insertedPhonePacket = new PhonePacket();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.PhonePacket (PhonePacketID, Name, FreeMinutes, Price)" +
                                         " OUTPUT INSERTED.PhonePacketID" +
                                         " VALUES(@PhonePacketID, @Name, @FreeMinutes, @Price)";
                    command.CommandText = query;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = phonePacket.PhonePacketId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = phonePacket.Name;
                    command.Parameters.Add("@FreeMinutes", SqlDbType.Int).Value = phonePacket.FreeMinutes;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = phonePacket.Price;

                    connection.Open();
                    insertedPhonePacket.PhonePacketId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedPhonePacket;
        }

        public void Update(PhonePacket phonePacket)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.PhonePacket SET Name = @Name, FreeMinutes = @FreeMinutes, Price = @Price" +
                                         " WHERE PhonePacketID = @PhonePacketID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = phonePacket.Name;
                    command.Parameters.Add("@FreeMinutes", SqlDbType.Int).Value = phonePacket.FreeMinutes;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = phonePacket.Price;
                    command.Parameters.Add("@PhonePacketID", SqlDbType.UniqueIdentifier).Value = phonePacket.PhonePacketId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
