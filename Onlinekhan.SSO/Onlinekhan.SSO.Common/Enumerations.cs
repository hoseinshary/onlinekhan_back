using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.Common
{
    public enum JwtHashAlgorithm
    {
        RS256,
        HS384,
        HS512
    }
        public enum MessageType
        {
            Error = 0,
            Success = 1,
            Warning = 2,
            NotFound = 3,
            Unauthorized = 4
        }

        public enum CrudType
        {
            None = 0,
            Create = 1,
            Update = 2,
            Delete = 3
        }
        public enum UserType
        {
            [Display(Name = "سازمانی")]
            Organ = 0,

            [Display(Name = "دانش آموز")]
            Student = 1,

            [Display(Name = "معلم")]
            Teacher = 2
        }
    
}
