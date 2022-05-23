using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Log;
using Newtonsoft.Json;
using StructureMap.TypeRules;

namespace NasleGhalam.ServiceLayer.Services
{
    public class LogService
    {
        private const string Title = "لاگ";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Log> _logservices;

        public LogService(IUnitOfWork uow)
        {
            _uow = uow;
            _logservices = uow.Set<Log>();
        }

        /// <summary>
        /// گرفتن  لاگ با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LogViewModel GetById(int id)
        {
           var log= _logservices
                .Where(current => (int)current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LogViewModel>)
                .FirstOrDefault();
            byte[] inputBytes = Convert.FromBase64String(log.ObjectValue);

            var inputStream = new MemoryStream(inputBytes);
            var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            var streamReader = new StreamReader(gZipStream);
                var decompressed = streamReader.ReadToEnd();

                log.ObjectValue = decompressed;

            return log;
        }
        public string GetValueById(int id)
        {
            var log = _logservices
                .Where(current => (int)current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(x=>x.ObjectValue)
                .FirstOrDefault();

            byte[] inputBytes = Convert.FromBase64String(log);

            var inputStream = new MemoryStream(inputBytes);
            var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress);
            var streamReader = new StreamReader(gZipStream);
            var decompressed = streamReader.ReadToEnd();

            return decompressed;
        }
        /// <summary>
        /// گرفتن همه لاگ ها
        /// </summary>
        /// <returns></returns>
        public IList<LogGetAllViewModel> GetAll()
        {
            return _logservices
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<LogGetAllViewModel>)
                .ToList();
        }
       
        /// <summary>
        /// ثبت لاگ
        /// </summary>
        /// <param name="logserviceViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(CrudType Crud, string TableName, object Value,int UserId)
        {
            string jsonString = JsonConvert.SerializeObject(Value);
            byte[] inputBytes = Encoding.UTF8.GetBytes(jsonString);

            var outputStream = new MemoryStream();

                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);

                var outputBytes = outputStream.ToArray();


                var outputStr = Convert.ToBase64String(outputBytes);
                System.Reflection.PropertyInfo pi = Value.GetType().GetProperty("Id");
                int Id = 0;
                if(pi!=null)
                    Id = (int)(pi.GetValue(Value, null));
                
                    
                Log log = new Log() { CrudType = Crud, DateTime = DateTime.Now, ObjectValue = outputStr, TableName = TableName, UserId = UserId, ObjectId = Id };
                _logservices.Add(log);

                var serverResult = _uow.CommitChanges(CrudType.Create, Title);
                var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

                if (clientResult.MessageType == MessageType.Success)
                    clientResult.Obj = GetById(log.Id);

                return clientResult;

        }

        /// <summary>
        /// ویرایش لاگ
        /// </summary>
        /// <param name="logserviceViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(LogViewModel logserviceViewModel)
        {
            var logservice = Mapper.Map<Log>(logserviceViewModel);
            _uow.MarkAsChanged(logservice);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(logservice.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف لاگ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var logserviceViewModel = GetById(id);
            if (logserviceViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var logservice = Mapper.Map<Log>(logserviceViewModel);
            _uow.MarkAsDeleted(logservice);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
