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
    public class OptionVoterRepository : IOptionVoterRepository
    {
        private readonly IDbContext _dbContext;

        public OptionVoterRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OptionVoter> Get(Guid pollId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT COUNT(*) AS NumberOfVoters, PollID, PollOptionID FROM useractions.OptionVoter" +
                                     " WHERE PollID = @PollID" +
                                     " GROUP BY PollID, PollOptionID";
                return connection.Query<OptionVoter>(query, new { PollID = pollId });
            }
        }

        public bool Get(Guid pollId, Guid userId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT COUNT(*) FROM useractions.OptionVoter" +
                                     " WHERE PollID = @PollID AND UserID = @UserID";
                return (connection.ExecuteScalar<int>(query, new { PollID = pollId, UserID = userId })) == 1;
            }
        }

        public OptionVoter Insert(OptionVoter optionVoter)
        {
            var insertedOptionVoter = optionVoter;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.OptionVoter (PollID, PollOptionID, UserID)" +
                                         " VALUES(@PollID, @PollOptionID, @UserID)";
                    command.CommandText = query;
                    command.Parameters.Add("@PollID", SqlDbType.UniqueIdentifier).Value = optionVoter.PollId;
                    command.Parameters.Add("@PollOptionID", SqlDbType.UniqueIdentifier).Value = optionVoter.PollOptionId;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = optionVoter.UserId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return insertedOptionVoter;
        }
    }
}
