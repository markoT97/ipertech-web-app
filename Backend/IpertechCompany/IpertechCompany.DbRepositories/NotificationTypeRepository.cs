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
    public class NotificationTypeRepository : INotificationTypeRepository
    {
        private readonly IDbContext _dbContext;

        public NotificationTypeRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid notificationTypeId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM notifications.NotificationType" +
                                         " WHERE NotificationTypeID = @NotificationTypeID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notificationTypeId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<NotificationType> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM notifications.NotificationType";
                return connection.Query<NotificationType>(query);
            }
        }

        public NotificationType Insert(NotificationType notificationType)
        {
            var insertedNotificationType = notificationType;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO notifications.NotificationType (NotificationTypeID, Name, ImageWidth, ImageHeight)" +
                                         " OUTPUT INSERTED.NotificationTypeID" +
                                         " VALUES(@NotificationTypeID, @Name, @ImageWidth, @ImageHeight)";
                    command.CommandText = query;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notificationType.NotificationTypeId;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = notificationType.Name;
                    command.Parameters.Add("@ImageWidth", SqlDbType.Int).Value = notificationType.ImageWidth;
                    command.Parameters.Add("@ImageHeight", SqlDbType.Int).Value = notificationType.ImageHeight;

                    connection.Open();
                    insertedNotificationType.NotificationTypeId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedNotificationType;
        }

        public void Update(NotificationType notificationType)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE notifications.NotificationType SET Name = @Name, ImageWidth = @ImageWidth, ImageHeight = @ImageHeight" +
                                         " WHERE NotificationTypeID = @NotificationTypeID";
                    command.CommandText = query;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = notificationType.Name;
                    command.Parameters.Add("@ImageWidth", SqlDbType.Int).Value = notificationType.ImageWidth;
                    command.Parameters.Add("@ImageHeight", SqlDbType.Int).Value = notificationType.ImageHeight;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notificationType.NotificationTypeId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
