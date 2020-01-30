﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.Text;
using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;

namespace IpertechCompany.DbRepositories
{
    public class InternetPacketRepository : IInternetPacketRepository
    {
        private readonly IDbContext _dbContext;

        public InternetPacketRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid internetPacketId, Guid internetRouterId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.InternetPacket" +
                                   " WHERE InternetPacketID = @InternetPacketID AND InternetRouterID = @InternetRouterID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = internetPacketId;
                    command.Parameters.AddWithValue("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = internetRouterId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<InternetPacket> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.InternetPacket";
                return connection.Query<InternetPacket>(query);
            }
        }

        public InternetPacket Insert(InternetPacket internetPacket)
        {
            var insertedInternetPacket = new InternetPacket();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.InternetPacket (InternetRouterID, Name, Speed, Price)" +
                                   " OUTPUT INSERTED.InternetPacketID" +
                                   " VALUES(@InternetRouterID, @Name, @Speed, @Price)";
                    command.CommandText = query;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = internetPacket.InternetRouterId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = internetPacket.Name;
                    command.Parameters.Add("@Speed", SqlDbType.VarChar, 100).Value = internetPacket.Speed;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = internetPacket.Price;

                    connection.Open();
                    insertedInternetPacket.InternetPacketId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedInternetPacket;
        }

        public void Update(InternetPacket internetPacket)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.InternetPacket SET InternetRouterID = @InternetRouterID, Name = @Name," +
                                         " Speed = @Speed, Price = @Price" +
                                   " WHERE InternetPacketID = @InternetPacketID AND InternetRouterID = @InternetRouterID";

                    command.CommandText = query;
                    command.Parameters.Add("@InternetPacketID", SqlDbType.UniqueIdentifier).Value = internetPacket.InternetPacketId;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.Date).Value = internetPacket.InternetRouterId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = internetPacket.Name;
                    command.Parameters.Add("@Speed", SqlDbType.VarChar, 100).Value = internetPacket.Speed;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = internetPacket.Price;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
