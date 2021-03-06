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
    public class BillRepository : IBillRepository
    {
        private readonly IDbContext _dbContext;

        public BillRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Delete(Guid billId)
        {
            var rowsAffected = 0;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "DELETE FROM useractions.Bill" +
                        " WHERE BillID = @BillID";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@BillID", SqlDbType.UniqueIdentifier).Value = billId;

                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
            }
            return (rowsAffected > 0);
        }

        public IEnumerable<Bill> Get(Guid userContractId, int offset, int numberOfRows)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT * FROM useractions.Bill b" +
                                     " INNER JOIN useractions.UserContract uc ON b.UserContractID = uc.UserContractID" +
                                     " INNER JOIN packets.PacketCombination pc ON uc.PacketCombinationID = pc.PacketCombinationID" +
                                     " INNER JOIN useractions.[User] u ON uc.UserContractID = u.UserContractID" +
                                     " WHERE b.UserContractID = @UserContractID" +
                                     " ORDER BY b.startDate DESC" +
                                     " OFFSET @Offset ROWS" +
                                     " FETCH NEXT @NumberOfRows ROWS ONLY";
                return connection.Query<Bill, UserContract, PacketCombination, User, Bill>(query, (bill, userContract, packetCombination, user) =>
                {
                    userContract.PacketCombination = packetCombination;
                    bill.UserContract = userContract;
                    bill.FirstAndLastName = user.FirstName + " " + user.LastName;
                    return bill;
                }, splitOn: "UserContractID, PacketCombinationID, UserID", param: new { UserContractId = userContractId, Offset = offset, NumberOfRows = numberOfRows });
            }
        }

        public int Get(Guid userContractId)
        {
            using (var connection = _dbContext.Connect())
            {
                const string query = "SELECT COUNT(*) FROM useractions.Bill b" +
                                     " INNER JOIN useractions.UserContract uc ON b.UserContractID = uc.UserContractID" +
                                     " WHERE b.UserContractID = @UserContractID";
                return connection.ExecuteScalar<int>(query, new { UserContractId = userContractId });
            }
        }

        public Bill Insert(Bill bill)
        {
            var insertedBill = bill;
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "INSERT INTO useractions.Bill (BillID, UserContractID, StartDate, EndDate, CallNum, AccOfRecipient, IsPaid, Price, Currency)" +
                        " OUTPUT INSERTED.BillID" +
                        " VALUES(@BillID, @UserContractID, @StartDate, @EndDate, @CallNum, @AccOfRecipient, @IsPaid, @Price, @Currency)";
                    command.CommandText = query;
                    command.Parameters.Add("@BillID", SqlDbType.UniqueIdentifier).Value = bill.BillId;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = bill.UserContract.UserContractId;
                    command.Parameters.Add("@StartDate", SqlDbType.Date).Value = bill.StartDate;
                    command.Parameters.Add("@EndDate", SqlDbType.Date).Value = bill.EndDate;
                    command.Parameters.Add("@CallNum", SqlDbType.VarChar, 50).Value = bill.CallNum;
                    command.Parameters.Add("@AccOfRecipient", SqlDbType.VarChar, 50).Value = bill.AccOfRecipient;
                    command.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = bill.IsPaid;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = bill.Price;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar, 5).Value = bill.Currency;

                    connection.Open();
                    insertedBill.BillId = Guid.Parse(command.ExecuteScalar().ToString());
                }
            }
            return insertedBill;
        }

        public void Update(Bill bill)
        {
            using (var connection = _dbContext.Connect())
            {
                using (var command = (SqlCommand)connection.CreateCommand())
                {
                    const string query = "UPDATE useractions.Bill SET UserContractID = @UserContractID, StartDate = @StartDate, EndDate = @EndDate," +
                        " CallNum = @CallNum, AccOfRecipient = @AccOfRecipient, IsPaid = @IsPaid, Price = @Price, Currency = @Currency" +
                        " WHERE BillID = @BillID";

                    command.CommandText = query;
                    command.Parameters.Add("@UserContractID", SqlDbType.UniqueIdentifier).Value = bill.UserContract.UserContractId;
                    command.Parameters.Add("@StartDate", SqlDbType.Date).Value = bill.StartDate;
                    command.Parameters.Add("@EndDate", SqlDbType.Date).Value = bill.EndDate;
                    command.Parameters.Add("@CallNum", SqlDbType.VarChar, 50).Value = bill.CallNum;
                    command.Parameters.Add("@AccOfRecipient", SqlDbType.VarChar, 50).Value = bill.AccOfRecipient;
                    command.Parameters.Add("@IsPaid", SqlDbType.Bit).Value = bill.IsPaid;
                    command.Parameters.Add("@Price", SqlDbType.Decimal, 10).Value = bill.Price;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar, 5).Value = bill.Currency;
                    command.Parameters.Add("@BillID", SqlDbType.UniqueIdentifier).Value = bill.BillId;

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
