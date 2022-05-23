using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Microsoft.Office.Interop.Excel;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Lookup;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LookupService
    {
        private readonly IDbSet<Lookup> _lookups;

        public LookupService(IUnitOfWork uow)
        {
            _lookups = uow.Set<Lookup>();
        }

        /// <summary>
        /// گرفتن  لوک آپ با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LookupViewModel GetById(int id)
        {
            return _lookups
                .Where(current => current.Id == id)
                .Select(current => new LookupViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    Value = current.Value,
                    State = current.State
                }).FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه ی لوکاپ ها با نام
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IList<LookupViewModel> GetAllByName(string name , int state =-1)
        {
            if (state == -1)
            {
                return _lookups
                    .Where(x => x.Name == name)
                    .AsNoTracking()
                    .AsEnumerable()
                    .OrderBy(x => x.State)
                    .Select(Mapper.Map<LookupViewModel>)
                    .ToList();
            }
            else
            {
                return _lookups
                    .Where(x => x.Name == name)
                    .Where(x => x.State == state)
                    .AsNoTracking()
                    .AsEnumerable()
                    .OrderBy(x => x.State)
                    .Select(Mapper.Map<LookupViewModel>)
                    .ToList();
            }
        }
    }
}
