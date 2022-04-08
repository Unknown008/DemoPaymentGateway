using BankSimulator.DBContext;
using BankSimulator.Models;

namespace BankSimulator.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(BankContext bankContext) : base(bankContext) { }
    }
}
