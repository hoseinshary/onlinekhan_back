using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.ViewModels.Sale
{
    public class ChangeStatusViewModel
    {
        public Guid saleId { get; set; }
        public string VerifiedCode { get; set; }
    }
}
