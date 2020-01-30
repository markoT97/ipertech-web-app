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
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContext _dbContext;

        public MessageRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid messageId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.Message" +
                                         " WHERE MessageID = @MessageID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@MessageID", SqlDbType.UniqueIdentifier).Value = messageId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public Message Get(Guid messageId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.Message" +
                                     " WHERE MessageID = @MessageID";
                return connection.QueryFirstOrDefault<Message>(query, new { MessageID = messageId });
            }
        }

        public Message Insert(Message message)
        {
            var insertedMessage = new Message();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.Message (Title, Content, CreatedAt, Category)" +
                                         " OUTPUT INSERTED.MessageID" +
                                         " VALUES(@MessageID, @Title, @Content, @CreatedAt, @Category)";
                    command.CommandText = query;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 30).Value = message.Title;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar, 200).Value = message.Content;
                    command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = message.CreatedAt;
                    command.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = message.Category;

                    connection.Open();
                    insertedMessage.MessageId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedMessage;
        }

        public void Update(Message message)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.Message" +
                                         " SET Title = @Title, Content = @Content, CreatedAt = @CreatedAt, Category = @Category)" +
                                         " WHERE MessageID = @MessageID";
                    command.CommandText = query;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 30).Value = message.Title;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar, 200).Value = message.Content;
                    command.Parameters.Add("@CreatedAt", SqlDbType.VarChar, 50).Value = message.CreatedAt;
                    command.Parameters.Add("@Category", SqlDbType.VarChar, 50).Value = message.Category;
                    command.Parameters.Add("@MessageID", SqlDbType.UniqueIdentifier).Value = message.MessageId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
