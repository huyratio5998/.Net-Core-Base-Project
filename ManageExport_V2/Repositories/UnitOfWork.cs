using ManageExport_V2.Models;
using ManageExport_V2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageExport_V2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ExportContext _context;        
        private IUserRepository _userRepository;
        private IProductRepository _productRepository;
        private IExportListDetailRepository _exportListDetailRepository;
        private IExportProductBillRepository _exportProductBillRepository;
        private IStockRepository _stockRepository;
        private IImageRepository _imageRepository;
        private IBrandRepository _brandRepository;
        public UnitOfWork(ExportContext context)
        {
            _context = context;
        }
        public ExportContext ExportContext
        {
            get { return _context; }
        }        

        public IUserRepository Users
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
        public IProductRepository Products
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_context)); }
        }
        public IExportListDetailRepository ExportListDetailRepositorys
        {
            get { return _exportListDetailRepository ?? (_exportListDetailRepository = new ExportListDetailRepository(_context)); }
        }
        public IExportProductBillRepository ExportProductBillRepositorys
        {
            get { return _exportProductBillRepository ?? (_exportProductBillRepository = new ExportProductBillRepository(_context)); }
        }
        public IStockRepository StockRepository
        {
            get { return _stockRepository ?? (_stockRepository = new StockRepository(_context)); }
        }
        public IImageRepository ImageRepository
        {
            get { return _imageRepository ?? (_imageRepository = new ImageRepository(_context)); }
        }
        public IBrandRepository BrandRepository
        {
            get { return _brandRepository ?? (_brandRepository = new BrandRepository(_context)); }
        }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
