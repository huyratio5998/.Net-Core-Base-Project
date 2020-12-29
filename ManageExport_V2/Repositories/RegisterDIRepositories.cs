using ManageExport_V2.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public static class RegisterDIRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));            
            services.AddTransient<IUserRepository, UserRepository>();            
            services.AddTransient<IExportListDetailRepository, ExportListDetailRepository>();
            services.AddTransient<IExportProductBillRepository, ExportProductBillRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }

    }
}
