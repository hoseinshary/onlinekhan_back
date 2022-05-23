using AutoMapper;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ServiceLayer.MapperProfile
{
    public class LogProfile : Profile
    {
        public LogProfile()
        {
            CreateMap<LogViewModel, Log>();
            CreateMap<LogGetAllViewModel, Log>();
        }
    }
}
