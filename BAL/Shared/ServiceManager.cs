using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.Common;
using BAL.IServices;
using BAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.ApplicationConfig;
using Repository.UnitOfWork;

namespace BAL.Shared
{
    public class ServiceManager
    {
       public static void SetServiceInfo(IServiceCollection services, AppSetting appSetting)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(appSetting.ConnectionStrings);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService ,UserServices>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITransactionService, TransactionSecvice>();
            services.AddScoped<TokenProvider, TokenProvider>();

        }
    }
}
