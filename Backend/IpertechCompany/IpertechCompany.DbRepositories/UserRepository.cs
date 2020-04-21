using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace IpertechCompany.DbRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid userId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.[User]" +
                                         " WHERE UserID = @UserID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@UserID", SqlDbType.UniqueIdentifier).Value = userId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public User Get(Guid userContractId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT TOP 3 * FROM useractions.[User] u" +
                                    " INNER JOIN useractions.UserContract uc ON u.UserContractID = uc.UserContractID" +
                                    " INNER JOIN packets.PacketCombination pc ON uc.PacketCombinationID = pc.PacketCombinationID" +
                                    " INNER JOIN packets.InternetPacket [ip] ON pc.InternetPacketID = [ip].InternetPacketID" +
                                    " LEFT JOIN packets.TvPacket tp ON pc.TvPacketID = tp.TvPacketID" +
                                    " LEFT JOIN packets.PhonePacket pp ON pc.PhonePacketID = pp.PhonePacketID" +
                                    " LEFT JOIN useractions.Bill b ON uc.UserContractID = b.UserContractID" +
                                    " WHERE uc.UserContractID = @UserContractID AND (b.IsPaid = 0 OR b.isPaid IS NULL)" +
                                    " ORDER BY b.StartDate DESC";
                return connection.Query<User, UserContract, PacketCombination, InternetPacket, TvPacket, PhonePacket, Bill, User>(query, (user, userContract, packetCombination, internetPacket, tvPacket, phonePacket, bill) =>
                {
                    user.UserContract = userContract;
                    user.UserContract.PacketCombination = packetCombination;
                    user.UserContract.PacketCombination.InternetPacket = internetPacket;
                    user.UserContract.PacketCombination.TvPacket = tvPacket;
                    user.UserContract.PacketCombination.PhonePacket = phonePacket;
                    user.Bills.Add(bill);

                    return user;
                }, splitOn: "UserContractID, PacketCombinationID, InternetPacketID, TvPacketID, PhonePacketID, BillID", param: new { UserContractID = userContractId }).GroupBy(user => user.UserId).Select(group =>
                {
                    var userWithBills = group.First();
                    userWithBills.Bills = group.Select(user => user.Bills.Single()).ToList();
                    return userWithBills;
                }).SingleOrDefault();
            }
        }

        public User Get(UserLogin userLogin)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT *" +
                                    " FROM useractions.[User] u" +
                                    " LEFT JOIN useractions.UserContract uc ON u.UserContractID = uc.UserContractID" +
                                    " WHERE Email = @Email AND Password = @Password";
                return connection.Query<User, UserContract, User>(query, (user, userContract) =>
                {
                    user.UserContract = userContract;
                    return user;
                }, splitOn: "UserContractID", param: new { userLogin.Email, userLogin.Password }).SingleOrDefault();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT u.UserID, u.[Role], u.FirstName, u.LastName, u.Gender, u.Email, u.PhoneNumber, u.[Password],                       u.ImageLocation, uc.*" +
                                    " FROM useractions.[User] u" +
                                    " INNER JOIN useractions.UserContract uc ON u.UserContractID = uc.UserContractID";
                return connection.Query<User, UserContract, User>(query, (user, userContract) =>
                {
                    user.UserContract = userContract;
                    return user;
                }, splitOn: "UserContractID");
            }
        }

        public User Insert(User user)
        {
            var insertedUser = user;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.[User] (UserID, UserContractID, Role, FirstName, LastName, Gender, Email," +
                                         " PhoneNumber, Password)" +
                                         " OUTPUT INSERTED.UserID" +
                                         " VALUES(@UserID, @UserContractID, @Role, @FirstName, @LastName," +
                                         " @Gender, @Email, @PhoneNumber, @Password)";
                    command.CommandText = query;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = user.UserId;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = user.UserContract.UserContractId;
                    command.Parameters.Add("@Role", SqlDbType.VarChar, 30).Value = user.Role;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                    command.Parameters.Add("@Gender", SqlDbType.NVarChar, 20).Value = (object)user.Gender ?? DBNull.Value;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = user.Email;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = user.PhoneNumber;
                    command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;

                    connection.Open();
                    insertedUser.UserId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedUser;
        }

        public void Update(User user)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.[User] SET UserContractID = @UserContractID, Role = @Role, " +
                                         "FirstName = @FirstName, LastName = @LastName, Gender = @Gender, Email = @Email," +
                                         " PhoneNumber  = @PhoneNumber, Password = @Password, ImageLocation = @ImageLocation" +
                                         " WHERE UserID = @UserID";
                    command.CommandText = query;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = user.UserContract.UserContractId;
                    command.Parameters.Add("@Role", SqlDbType.VarChar, 30).Value = user.Role;
                    command.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                    command.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                    command.Parameters.Add("@Gender", SqlDbType.NVarChar, 20).Value = user.Gender;
                    command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = (object)user.Email ?? DBNull.Value;
                    command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 50).Value = user.PhoneNumber;
                    command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = (object)user.ImageLocation ?? DBNull.Value;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = user.UserId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserImage userImage)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.[User] SET ImageLocation = @ImageLocation" +
                                         " WHERE UserID = @UserID";
                    command.CommandText = query;
                    command.Parameters.Add("@ImageLocation", SqlDbType.NVarChar, 200).Value = (object)userImage.ImageLocation ?? DBNull.Value;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userImage.UserId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(UserPassword userPassword)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.[User] SET Password = @Password" +
                                         " WHERE UserID = @UserID";
                    command.CommandText = query;
                    command.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = userPassword.Password;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = userPassword.UserId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
