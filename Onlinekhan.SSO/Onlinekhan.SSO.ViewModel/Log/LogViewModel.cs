﻿using System;
using System.ComponentModel.DataAnnotations;
using Onlinekhan.SSO.Common;

namespace Onlinekhan.SSO.ViewModels.Log
{
    public class LogViewModel
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
        public string ObjectValue { get; set; }

        [Display(Name = "")]
        public int UserId { get; set; }

    }
}
