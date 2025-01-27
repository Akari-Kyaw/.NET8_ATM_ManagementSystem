using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Respositories.IRepositories;

namespace Repository.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
        {
        IUserRepository Users { get; }
        IFileRepository Files { get; }
        ITransactionRepository Transactions { get; }


        Task<int> SaveChangesAsync();

    }
}
