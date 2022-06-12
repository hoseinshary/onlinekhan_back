using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.DomainClasses.Entities;

namespace NasleGhalam.ViewModels.Sale
{
    public class SaleResultViewModel
    {
        public IList<Sale_Package> salePackages { get; set; }
        public DomainClasses.Entities.Sale sale { get; set; }
    }
}
