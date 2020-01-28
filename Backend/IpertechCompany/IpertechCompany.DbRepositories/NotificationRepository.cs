using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using IpertechCompany.DbContext;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;

namespace IpertechCompany.DbRepositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbContext _dbContext;

        public NotificationRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid notificationId, Guid notificationTypeId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM notifications.Notification" +
                                         " WHERE NotificationID = @NotificationID AND NotificationTypeID = @NotificationTypeID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@NotificationID", SqlDbType.UniqueIdentifier).Value = notificationId;
                    command.Parameters.AddWithValue("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notificationTypeId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<Notification> Get(Guid notificationTypeId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM notifications.Notification" +
                                     " WHERE NotificationTypeID = @NotificationTypeID";
                return connection.Query<Notification>(query, new { NotificationTypeID = notificationTypeId });
            }
        }

        public Notification Insert(Notification notification)
        {
            var insertedNotification = new Notification();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO notifications.Notification (NotificationTypeID, Title, Content, CreatedAt, ImageLocation)" +
                                         " OUTPUT INSERTED.NotificationID" +
                                         " VALUES(@NotificationTypeID, @Title, @Content, @CreatedAt, @ImageLocation)";
                    command.CommandText = query;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notification.NotificationTypeId;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = notification.Title;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar, 500).Value = (object)notification.Content ?? DBNull.Value;
                    command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = notification.CreatedAt;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = notification.ImageLocation;

                    connection.Open();
                    insertedNotification.NotificationId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedNotification;
        }

        public void Update(Notification notification)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE notifications.Notification SET NotificationTypeID = @NotificationTypeID, Title = @Title," +
                                         " Content = @Content, CreatedAt = @CreatedAt, ImageLocation = @ImageLocation)" +
                                         " WHERE NotificationID = @NotificationID";
                    command.CommandText = query;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notification.NotificationTypeId;
                    command.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = notification.Title;
                    command.Parameters.Add("@Content", SqlDbType.NVarChar, 500).Value = (object)notification.Content ?? DBNull.Value;
                    command.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = notification.CreatedAt;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = notification.ImageLocation;
                    command.Parameters.Add("@NotificationID", SqlDbType.UniqueIdentifier).Value = notification.NotificationId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
