using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Province;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ProvinceService
    {
        private const string Title = "استان";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Province> _provinces;

        public ProvinceService(IUnitOfWork uow)
        {
            _uow = uow;
            _provinces = uow.Set<Province>();
        }

        /// <summary>
        /// گرفتن  استان با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProvinceViewModel GetById(int id)
        {
            return _provinces
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProvinceViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه استان ها
        /// </summary>
        /// <returns></returns>
        public IList<ProvinceViewModel> GetAll()
        {
            return _provinces
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProvinceViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت استان
        /// </summary>
        /// <param name="provinceViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(ProvinceViewModel provinceViewModel)
        {
            var province = Mapper.Map<Province>(provinceViewModel);
            _provinces.Add(province);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = province;

            return clientResult;
        }

        /// <summary>
        /// ویرایش استان
        /// </summary>
        /// <param name="provinceViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(ProvinceViewModel provinceViewModel)
        {
            var province = Mapper.Map<Province>(provinceViewModel);
            _uow.MarkAsChanged(province);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = province;

            return clientResult;
        }

        /// <summary>
        /// حذف استان
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var provinceViewModel = GetById(id);
            if (provinceViewModel == null)
                return ClientMessageResult.NotFound();

            var province = Mapper.Map<Province>(provinceViewModel);
            _uow.MarkAsDeleted(province);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
