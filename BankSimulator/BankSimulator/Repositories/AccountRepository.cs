using BankSimulator.DBContext;
using BankSimulator.Models;

namespace BankSimulator.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(BankContext bankContext) : base(bankContext) { }
    }
}
