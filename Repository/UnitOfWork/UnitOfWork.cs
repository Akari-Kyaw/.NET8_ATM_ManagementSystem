using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using Model.ApplicationConfig;
using Model.Entities;
using Repository.Respositories.IRepositories;
using Repository.Respositories.Repositories;

namespace Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dataContext;
        public UnitOfWork(DataContext dataContext, IOptions<AppSetting> appsetting)
        {
            _dataContext = dataContext;
            AppSetting = appsetting.Value;
            Users = new UserRespository(dataContext);
        }
        public IUserRespository Users { get; set; }
        public AppSetting AppSetting { get; set; }
        public void Dispose()
        {
            _dataContext.Dispose();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
