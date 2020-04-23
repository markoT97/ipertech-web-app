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
    public class UserContractRepository : IUserContractRepository
    {
        private readonly IDbContext _dbContext;

        public UserContractRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid userContractId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.UserContract" +
                                         " WHERE UserContractID = @UserContractID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@UserContractID", SqlDbType.UniqueIdentifier).Value = userContractId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<UserContract> GetAll()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.UserContract uc" +
                                    " INNER JOIN packets.PacketCombination pc ON uc.PacketCombinationID = pc.PacketCombinationID";
                return connection.Query<UserContract, PacketCombination, UserContract>(query, (userContract, packetCombination) =>
                {
                    userContract.PacketCombination = packetCombination;
                    return userContract;
                }, splitOn: "PacketCombinationID");
            }
        }

        public UserContract GetById(Guid userContractId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.UserContract uc" +
                                    " INNER JOIN packets.PacketCombination pc ON uc.PacketCombinationID = pc.PacketCombinationID" +
                                    " WHERE UserContractID = @UserContractID";
                return connection.Query<UserContract, PacketCombination, UserContract>(query, (userContract, packetCombination) =>
                {
                    userContract.PacketCombination = packetCombination;
                    return userContract;
                }, splitOn: "PacketCombinationID", param: new { UserContractID = userContractId }).SingleOrDefault();
            }
        }

        public UserContract Insert(UserContract userContract)
        {
            var insertedUserContract = userContract;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.UserContract (UserContractID, PacketCombinationID, ContractDurationMonths)" +
                                         " OUTPUT INSERTED.UserContractID" +
                                         " VALUES(@UserContractID, @PacketCombinationID, @ContractDurationMonths)";
                    command.CommandText = query;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = userContract.UserContractId;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = userContract.PacketCombination.PacketCombinationId;
                    command.Parameters.Add("@ContractDurationMonths", SqlDbType.Int).Value = userContract.ContractDurationMonths;

                    connection.Open();
                    insertedUserContract.UserContractId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedUserContract;
        }

        public void Update(UserContract userContract)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.UserContract SET PacketCombinationID = @PacketCombinationID, " +
                                         "ContractDurationMonths = @ContractDurationMonths" +
                                         " WHERE UserContractID = @UserContractID";
                    command.CommandText = query;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = userContract.PacketCombination.PacketCombinationId;
                    command.Parameters.Add("@ContractDurationMonths", SqlDbType.Int).Value = userContract.ContractDurationMonths;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = userContract.UserContractId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
