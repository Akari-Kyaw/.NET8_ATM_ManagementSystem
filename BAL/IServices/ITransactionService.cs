using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;
using Model.Entities;

namespace BAL.IServices
{
    public interface ITransactionService
    {

        Task<IEnumerable<Transactions>> GetAllTransactions();
        Task<IEnumerable<Transactions>> GetTransactionByUserID(Guid UserId);

        Task<Transactions> GetTransactionByID(Guid id);
        Task Deposit(DepositDTO inputModel);
        Task Withdraw(WithdrawDTO inputModel);


    }
}
