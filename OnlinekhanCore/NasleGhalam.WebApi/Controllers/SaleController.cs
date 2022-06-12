using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Sale;
using NasleGhalam.WebApi.Extensions;
using NasleGhalam.WebApi.FilterAttribute;

namespace NasleGhalam.WebApi.Controllers
{
    public class SaleController : ApiController
    {
        private readonly SaleService _saleService;
        public SaleController(SaleService saleService)
        {
            _saleService = saleService;
        }

      

        [HttpPost]
        [CheckUserAccess()]
        [CheckModelValidation]
        public IHttpActionResult Create(SaleCreateViewModel saleCreateViewModel)
        {
            return Ok(_saleService.Create(saleCreateViewModel,Request.GetUserId()));
        }
        [HttpPost]
        [CheckUserAccess()]
        [CheckModelValidation]
        public IHttpActionResult ChangeStatus(ChangeStatusViewModel changeStatusViewModel)
        {
            return Ok(_saleService.ChangeStatus(changeStatusViewModel));
        }

        
    }
}