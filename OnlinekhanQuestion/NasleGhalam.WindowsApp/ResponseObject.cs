using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NasleGhalam.Common;


namespace NasleGhalam.WindowsApp
{
    public class ResponseObject<T>
    {
        public ResponseObject()
        {
            Errors = new List<string>();
        }

        public MessageType MessageType { get; set; }

        public string Message { get; set; }

        public List<string> Errors { get; set; }

        public string JoinedErrors => string.Join(",", Errors);

        public int Id { get; set; }

        public T obj { get; set; }

        public static ResponseObject<T> NotFound()
        {
            var result = new ResponseObject<T>()
            {
                MessageType = Common.MessageType.NotFound
            };
            result.Errors.Add(ConstantSettings.NotFound);

            return result;
        }

        public static ResponseObject<T> Success()
        {
            return new ResponseObject<T>()
            {
                MessageType = Common.MessageType.Success
            };
        }

        public static ResponseObject<T> Error()
        {
            var result = new ResponseObject<T>()
            {
                MessageType = Common.MessageType.Error
            };
            result.Errors.Add(ConstantSettings.Error);

            return result;
        }

        public static ResponseObject<T> Unauthorized()
        {
            var result = new ResponseObject<T>()
            {
                MessageType = Common.MessageType.Unauthorized
            };
            result.Errors.Add(ConstantSettings.Unauthorized);

            return result;
        }
    }
}