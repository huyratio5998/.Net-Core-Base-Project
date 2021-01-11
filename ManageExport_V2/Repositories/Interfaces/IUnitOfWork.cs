using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IExportListDetailRepository ExportListDetailRepositorys { get; }
        IExportProductBillRepository ExportProductBillRepositorys{ get; }
        IStockRepository StockRepository { get; }
        IImageRepository ImageRepository { get; }
        IBrandRepository BrandRepository { get; }
        Task Commit();
    }
}
