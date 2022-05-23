using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.City;

namespace NasleGhalam.ServiceLayer.Services
{
    public class CityService
    {
        private const string Title = "شهر";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<City> _cities;

        public CityService(IUnitOfWork uow)
        {
            _uow = uow;
            _cities = uow.Set<City>();
        }

        /// <summary>
        /// گرفتن  شهر با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CityViewModel GetById(int id)
        {
            return _cities
                .Include(current => current.Province)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<CityViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه شهر ها
        /// </summary>
        /// <returns></returns>
        public IList<CityViewModel> GetAll()
        {
            return _cities
                .Include(current => current.Province)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<CityViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت شهر
        /// </summary>
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(CityCreateViewModel cityViewModel)
        {
            var city = Mapper.Map<City>(cityViewModel);
            _cities.Add(city);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(city.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش شهر
        /// </summary>
        /// <param name="cityViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(CityUpdateViewModel cityViewModel)
        {
            var city = Mapper.Map<City>(cityViewModel);
            _uow.MarkAsChanged(city);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(city.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف شهر
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var cityViewModel = GetById(id);
            if (cityViewModel == null)
                return ClientMessageResult.NotFound();

            var city = Mapper.Map<City>(cityViewModel);
            _uow.MarkAsDeleted(city);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);

            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
