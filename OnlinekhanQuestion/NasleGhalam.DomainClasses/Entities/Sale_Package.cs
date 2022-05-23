using System;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Sale_Package
    {

        public Sale_Package()
        {
            
        }


        public int Id { get; set; }


        public int CountItem { get; set; }

        public int PriceRow { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

        public Guid SaleId { get; set; }
        public Sale Sale { get; set; }

    }
}
