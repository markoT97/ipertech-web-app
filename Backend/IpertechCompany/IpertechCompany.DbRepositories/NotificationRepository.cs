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

        public IEnumerable<Notification> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM notifications.Notification n" +
                                     " INNER JOIN notifications.NotificationType nt ON n.NotificationTypeID = nt.NotificationTypeID" +
                                     " ORDER BY n.Title";
                return connection.Query<Notification, NotificationType, Notification>(query, (notification, notificationType) =>
                {
                    notification.NotificationType = notificationType;
                    return notification;
                }
                , splitOn: "NotificationTypeID");
            }
        }

        public IEnumerable<Notification> GetAll(int numberOfNewestRows)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT TOP (@numberOfNewestRows) * FROM notifications.Notification n" +
                                     " INNER JOIN notifications.NotificationType nt ON n.NotificationTypeID = nt.NotificationTypeID" +
                                     " ORDER BY n.CreatedAt DESC";
                return connection.Query<Notification, NotificationType, Notification>(query, (notification, notificationType) =>
                {
                    notification.NotificationType = notificationType;
                    return notification;
                }
                , splitOn: "NotificationTypeID", param: new { numberOfNewestRows });
            }
        }

        public IEnumerable<Notification> Get(Guid notificationTypeId, int numberOfNewestRows)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT TOP (@numberOfNewestRows) * FROM notifications.Notification n" +
                                     " INNER JOIN notifications.NotificationType nt ON n.NotificationTypeID = nt.NotificationTypeID" +
                                     " WHERE n.NotificationTypeID = @NotificationTypeID" +
                                     " ORDER BY n.CreatedAt DESC";
                return connection.Query<Notification, NotificationType, Notification>(query, (notification, notificationType) =>
                {
                    notification.NotificationType = notificationType;
                    return notification;
                }, splitOn: "NotificationTypeID", param: new { NotificationTypeID = notificationTypeId, numberOfNewestRows });
            }
        }

        public Notification Insert(Notification notification)
        {
            var insertedNotification = notification;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO notifications.Notification (NotificationID, NotificationTypeID, Title, Content, CreatedAt, ImageLocation)" +
                                         " OUTPUT INSERTED.NotificationID" +
                                         " VALUES(@NotificationID, @NotificationTypeID, @Title, @Content, @CreatedAt, @ImageLocation)";
                    command.CommandText = query;
                    command.Parameters.Add("@NotificationID", SqlDbType.UniqueIdentifier).Value = notification.NotificationId;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notification.NotificationType.NotificationTypeId;
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
                                         " Content = @Content, CreatedAt = @CreatedAt, ImageLocation = @ImageLocation" +
                                         " WHERE NotificationID = @NotificationID";
                    command.CommandText = query;
                    command.Parameters.Add("@NotificationTypeID", SqlDbType.UniqueIdentifier).Value = notification.NotificationType.NotificationTypeId;
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
