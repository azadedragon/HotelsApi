using HotelsApi.Context;
using HotelsApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelsApi.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllTransaction();
        Task<Transaction?> GetTransactionById(int id);
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<Transaction?> UpdateTransaction(int id, Transaction transaction);
        Task<bool> DeleteTransaction(int id);
    }
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext databaseContext;
        public TransactionRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            databaseContext.Transactions.Add(transaction);
            await databaseContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transactionRecord = await databaseContext.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transactionRecord == null)
            {
                return false;
            }
            databaseContext.Transactions.Remove(transactionRecord);
            await databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Transaction>> GetAllTransaction()
        {
            var Transaction = await databaseContext.Transactions.ToListAsync();
            return Transaction;
        }

        public async Task<Transaction?> GetTransactionById(int id)
        {
            var Transaction = await databaseContext.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
            return Transaction;
        }

        public async Task<Transaction?> UpdateTransaction(int id, Transaction transaction)
        {
            var transactionRecord = await databaseContext.Transactions.FirstOrDefaultAsync(x => x.TransactionId == id);
            if (transactionRecord == null)
            {
                return null;
            }
            transactionRecord.HotelId = transaction.HotelId;
            transactionRecord.HotelCode = transaction.HotelCode;
            transactionRecord.HotelName = transaction.HotelName;
            transactionRecord.DateFrom = transaction.DateFrom;
            transactionRecord.DateTo = transaction.DateTo;
            transactionRecord.FullName = transaction.FullName;
            transactionRecord.PhoneNumber = transaction.PhoneNumber;
            transactionRecord.EmailAddress = transaction.EmailAddress;
            await databaseContext.SaveChangesAsync();
            return transactionRecord;
        }
    }
}
