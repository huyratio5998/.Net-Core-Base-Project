using ManageExport_V2.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Services
{
    public static class RegisterDIServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<ICommonServices, CommonServices>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IExportProductServices, ExportProductServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IStockServices, StockServices>();
            services.AddTransient<IImageServices, ImageServices>();
            services.AddTransient<IBrandServices, BrandServices>();
            

            return services;
        }
    }
}
