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
    public class InternetRouterRepository : IInternetRouterRepository
    {
        private readonly IDbContext _dbContext;

        public InternetRouterRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid internetRouterId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM packets.InternetRouter" +
                                         " WHERE InternetRouterID = @InternetRouterID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = internetRouterId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public InternetRouter Get(Guid internetRouterId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM packets.InternetRouter" +
                               " WHERE InternetRouterID = @InternetRouterID";
                return connection.QueryFirstOrDefault<InternetRouter>(query, new { InternetRouterID = internetRouterId });
            }
        }

        public InternetRouter Insert(InternetRouter internetRouter)
        {
            var insertedInternetRouter = new InternetRouter();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO packets.InternetRouter (Name, ImageLocation)" +
                                         " OUTPUT INSERTED.InternetRouterID" +
                                         " VALUES(@InternetRouterID, @Name, @ImageLocation)";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = internetRouter.Name;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = (object)internetRouter.ImageLocation ?? DBNull.Value;

                    connection.Open();
                    insertedInternetRouter.InternetRouterId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedInternetRouter;
        }

        public void Update(InternetRouter internetRouter)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE packets.InternetRouter SET Name = @Name, ImageLocation = @ImageLocation" +
                                         " WHERE InternetRouterID = @InternetRouterID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = internetRouter.Name;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = (object)internetRouter.ImageLocation ?? DBNull.Value;
                    command.Parameters.Add("@InternetRouterID", SqlDbType.UniqueIdentifier).Value = internetRouter.InternetRouterId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
