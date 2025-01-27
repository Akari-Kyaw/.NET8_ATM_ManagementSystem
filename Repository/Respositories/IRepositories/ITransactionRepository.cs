using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Model.Entities;

namespace Repository.Respositories.IRepositories
{
    public interface  ITransactionRepository:IGenericRepository<Transactions>
    {
    }
}
