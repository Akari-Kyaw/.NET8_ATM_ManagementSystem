using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Entities;
using Repository.Respositories.IRepositories;

namespace Repository.Respositories.Repositories
{
    internal class TransactionRepository : GenericRepository<Transactions>, ITransactionRepository
    {
        public TransactionRepository(DataContext context) : base(context) { }

    }
}
