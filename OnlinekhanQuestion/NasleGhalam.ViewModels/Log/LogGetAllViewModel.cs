using System;
using System.ComponentModel.DataAnnotations;
using NasleGhalam.Common;

namespace NasleGhalam.ViewModels.Log
{
    public class LogGetAllViewModel
    {
        [Display(Name = "")]
        public int Id { get; set; }


        [Display(Name = "")]
        public string TableName { get; set; }


        [Display(Name = "")]
        public CrudType CrudType { get; set; }


        [Display(Name = "")]
        public DateTime DateTime { get; set; }


        [Display(Name = "")]
        public int ObjectId { get; set; }


      

        [Display(Name = "")]
        public int UserId { get; set; }

    }
}
