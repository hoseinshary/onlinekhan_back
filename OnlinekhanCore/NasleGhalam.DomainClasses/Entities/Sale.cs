using System;
using System.Collections.Generic;
using NasleGhalam.Common;

namespace NasleGhalam.DomainClasses.Entities
{
    public class Sale
    {

        public Sale()
        {
            
        }


        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int TotalPrice { get; set; }

        public string Code { get; set; }

        public string PaidCode { get; set; }
        public SaleStatus Status { get; set; }
        public ICollection<Sale_Package> Sale_Packages { get; set; }


    }
}
