﻿using Dapper;
using IpertechCompany.DbConnection;
using IpertechCompany.IRepositories;
using IpertechCompany.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace IpertechCompany.DbRepositories
{
    public class PollOptionRepository : IPollOptionRepository
    {
        private readonly IDbContext _dbContext;

        public PollOptionRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid pollOptionId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.PollOption" +
                                         " WHERE PollOptionID = @PollOptionID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@PollOptionID", SqlDbType.UniqueIdentifier).Value = pollOptionId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<PollOption> Get(Guid pollId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT po.PollOptionID, po.AnswerText, p.* FROM useractions.PollOption po" +
                                     " INNER JOIN useractions.Poll p ON po.PollID = p.PollID" +
                                     " WHERE po.PollID = @PollID";
                return connection.Query<PollOption, Poll, PollOption>(query, (pollOption, poll) =>
                    {
                        pollOption.Poll = poll;
                        return pollOption;
                    }, splitOn: "PollID", param: new { PollID = pollId });
            }
        }

        public PollOption Insert(PollOption pollOption)
        {
            var insertedPollOption = pollOption;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.PollOption (PollOptionID, PollID, AnswerText)" +
                                         " OUTPUT INSERTED.PollOptionID" +
                                         " VALUES(@PollOptionID, @PollID, @AnswerText)";
                    command.CommandText = query;
                    command.Parameters.Add("@PollOptionID", SqlDbType.UniqueIdentifier).Value = pollOption.PollOptionId;
                    command.Parameters.Add("@PollID", SqlDbType.UniqueIdentifier).Value = pollOption.Poll.PollId;
                    command.Parameters.Add("@AnswerText", SqlDbType.NVarChar, 50).Value = pollOption.AnswerText;

                    connection.Open();
                    insertedPollOption.PollOptionId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedPollOption;
        }

        public void Update(PollOption pollOption)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.PollOption SET PollID = @PollID, AnswerText = @AnswerText" +
                                        " WHERE PollOptionID = @PollOptionID";
                    command.CommandText = query;
                    command.Parameters.Add("@PollID", SqlDbType.UniqueIdentifier).Value = pollOption.Poll.PollId;
                    command.Parameters.Add("@AnswerText", SqlDbType.NVarChar, 50).Value = pollOption.AnswerText;
                    command.Parameters.Add("@PollOptionID", SqlDbType.UniqueIdentifier).Value = pollOption.PollOptionId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
