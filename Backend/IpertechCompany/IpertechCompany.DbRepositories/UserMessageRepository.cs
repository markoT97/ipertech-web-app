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
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly IDbContext _dbContext;

        public UserMessageRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(UserMessage userMessage)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.UserMessage" +
                                         " WHERE UserID = @UserID AND MessageID = @MessageID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@UserID", SqlDbType.UniqueIdentifier).Value = userMessage.UserId;
                    command.Parameters.AddWithValue("@MessageID", SqlDbType.UniqueIdentifier).Value = userMessage.MessageId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public Message Get(Guid userId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.UserMessage" +
                                     " WHERE UserID = @UserID";
                return connection.QuerySingleOrDefault<Message>(query, new { UserID = userId });
            }
        }

        public UserMessage Insert(UserMessage userMessage)
        {
            var insertedUserMessage = userMessage;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.UserMessage (UserID, MessageID)" +
                                         " VALUES(@UserID, @MessageID)";
                    command.CommandText = query;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userMessage.UserId;
                    command.Parameters.Add("@MessageID", SqlDbType.UniqueIdentifier).Value = userMessage.MessageId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return insertedUserMessage;
        }
    }
}
