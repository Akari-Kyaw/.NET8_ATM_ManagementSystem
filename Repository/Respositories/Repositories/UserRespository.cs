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
    internal class UserRespository:GenericRepository<User>, IUserRespository

    {
        public UserRespository(DataContext context) : base(context) { }

}
}
