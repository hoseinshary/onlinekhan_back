using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onlinekhan.SSO.Common
{
   
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

    
}
