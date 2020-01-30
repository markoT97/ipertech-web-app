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
                const string query = "SELECT * FROM useractions.UserContract";
                return connection.Query<UserContract>(query);
            }
        }

        public UserContract Insert(UserContract userContract)
        {
            var insertedUserContract = new UserContract();
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.UserContract (PacketCombinationID, ContractDurationMonths)" +
                                         " OUTPUT INSERTED.UserContractID" +
                                         " VALUES(@PacketCombinationID, @ContractDurationMonths)";
                    command.CommandText = query;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = userContract.PacketCombinationId;
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
                                         "ContractDurationMonths = @ContractDurationMonths)" +
                                         " WHERE UserContractID = @UserContractID";
                    command.CommandText = query;
                    command.Parameters.Add("@PacketCombinationID", SqlDbType.UniqueIdentifier).Value = userContract.PacketCombinationId;
                    command.Parameters.Add("@ContractDurationMonths", SqlDbType.Int).Value = userContract.ContractDurationMonths;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = userContract.UserContractId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
