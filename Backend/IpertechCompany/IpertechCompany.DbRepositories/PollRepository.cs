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
    public class PollRepository : IPollRepository
    {
        private readonly IDbContext _dbContext;

        public PollRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid pollId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.Poll" +
                                         " WHERE PollID = @PollID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@PollID", SqlDbType.UniqueIdentifier).Value = pollId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public Poll Get()
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.Poll p" +
                                " INNER JOIN useractions.PollOption po ON p.PollID = po.PollID";
                return connection.Query<Poll, PollOption, Poll>(query, (poll, pollOption) =>
                {
                    poll.Options.Add(pollOption);
                    return poll;
                }, splitOn: "PollOptionID").GroupBy(poll => poll.PollId).Select(group =>
                {
                    var pollWithOptions = group.First();
                    pollWithOptions.Options = group.Select(poll => poll.Options.Single()).ToList();
                    return pollWithOptions;
                }).FirstOrDefault();
            }
        }

        public Poll Get(Guid pollId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.Poll p" +
                                " INNER JOIN useractions.PollOption po ON p.PollID = po.PollID" +
                                " WHERE p.PollID = @PollID";
                return connection.Query<Poll, PollOption, Poll>(query, (poll, pollOption) =>
                {
                    poll.Options.Add(pollOption);
                    return poll;
                }, splitOn: "PollOptionID", param: new { PollID = pollId }).GroupBy(poll => poll.PollId).Select(group =>
                {
                    var pollWithOptions = group.First();
                    pollWithOptions.Options = group.Select(poll => poll.Options.Single()).ToList();
                    return pollWithOptions;
                }).SingleOrDefault();
            }
        }

        public Poll Insert(Poll poll)
        {
            var insertedPoll = poll;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.Poll (PollID, Question)" +
                                         " OUTPUT INSERTED.PollID" +
                                         " VALUES(@PollID, @Question)";
                    command.CommandText = query;
                    command.Parameters.Add("@PollID", SqlDbType.UniqueIdentifier).Value = poll.PollId;
                    command.Parameters.Add("@Question", SqlDbType.NVarChar, 400).Value = poll.Question;

                    connection.Open();
                    insertedPoll.PollId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedPoll;
        }

        public void Update(Poll poll)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.Poll SET Question = @Question" +
                                         " WHERE PollID = @PollID";
                    command.CommandText = query;
                    command.Parameters.Add("@Question", SqlDbType.NVarChar, 400).Value = poll.Question;
                    command.Parameters.Add("@PollID", SqlDbType.UniqueIdentifier).Value = poll.PollId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
