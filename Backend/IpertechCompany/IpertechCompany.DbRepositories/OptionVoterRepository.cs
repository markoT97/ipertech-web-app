using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace IpertechCompany.DbRepositories
{
    public class OptionVoterRepository : IOptionVoterRepository
    {
        private readonly IDbContext _dbContext;

        public OptionVoterRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Get(Guid pollOptionId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.OptionVoter" +
                                     " WHERE PollOptionID = @PollOptionID";
                return connection.Query<OptionVoter>(query, new { PollOptionID = pollOptionId }).Count();
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
