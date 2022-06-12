using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ServiceLayer.Configs;
using NasleGhalam.ServiceLayer.Jwt;
using NasleGhalam.ViewModels.Sale;
using NasleGhalam.ViewModels.User;

namespace NasleGhalam.ServiceLayer.Services
{
    public class SaleService
    {
        private const string Title = "فاکتور";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Sale> _sales;
        private readonly Lazy<PackageService> _packageService;

        public SaleService(IUnitOfWork uow,
            Lazy<PackageService> packageService)
        {

            _uow = uow;
            _sales = _uow.Set<Sale>();
            _packageService = packageService;
          
        }

        /// <summary>
        /// گرفتن  فاکتور با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Sale GetById(Guid id)
        {
            return _sales
                .Where(current => current.Id == id)
                .Include(current => current.Sale_Packages.Select(x=>x.Package))
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
            
        }

        /// <summary>
        /// ساخت فاکتور
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="packagesId"></param>
        /// <returns></returns>
        public ClientMessageResult Create(SaleCreateViewModel saleCreateViewModel,int userId)
        {
            Sale currentSale = new Sale(){Id = Guid.NewGuid(),Date=DateTime.Now,UserId = userId,Status = SaleStatus.Pending,Sale_Packages = new List<Sale_Package>()};
            foreach (int item in saleCreateViewModel.packagesId)
            {
                Sale_Package salePackage = new Sale_Package(){CountItem = 1,PackageId = item,PriceRow = _packageService.Value.GetById(item).Price};
                currentSale.Sale_Packages.Add(salePackage);
            }

            currentSale.TotalPrice = currentSale.Sale_Packages.Sum(x => x.PriceRow);
            _sales.Add(currentSale);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(currentSale.Id);

            return clientResult;
        }


        /// <summary>
        /// تغییر وضعیت فاکتور
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="VerifiedCode"></param>
        /// <returns></returns>
        public ClientMessageResult ChangeStatus(ChangeStatusViewModel changeStatusViewModel)
        {
            var sale = _sales.FirstOrDefault(x => x.Id == changeStatusViewModel.saleId);
            sale.Status = SaleStatus.Completed;
            sale.PaidCode = changeStatusViewModel.VerifiedCode;
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(changeStatusViewModel.saleId);

            return clientResult;
        }

    }
}
