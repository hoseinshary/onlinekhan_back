using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Teacher;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ErrorService
    {
        private const string Title = "ارور";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Error> _errors;

        public ErrorService(IUnitOfWork uow)
        {
            _uow = uow;
            _errors = uow.Set<Error>();
        }

        /// <summary>
        /// گرفتن  ارور با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Error GetById(int id)
        {
            return _errors
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه ارور ها
        /// </summary>
        /// <returns></returns>
        public IList<Error> GetAll()
        {
            return _errors
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }

        /// <summary>
        /// ثبت ارور
        /// </summary>
        /// <param name="teacherViewModel"></param>
        /// <param name="userRoleLevel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(ErrorCreateViewModel errorViewModel)
        {
      
            var error = Mapper.Map<Error>(errorViewModel);
            
            _errors.Add(error);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(error.Id);

            return clientResult;
        }
        
        
    }
}
