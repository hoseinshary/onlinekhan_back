using System;
using System.Linq;
using System.Web;

namespace NasleGhalam.Common
{
    public class CheckFile // todo: hossein, use filter attribute
    {
        
        public static string UploadPictureFile(HttpPostedFileBase file , decimal fileSize)
        {
            try
            {
                var supportedTypes = new[] { "jpg", "jpeg", "png" };
              
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return "File Extension Is InValid - Only Upload jpg/jpeg/png File";
                    
                }
                else if (file.ContentLength > (fileSize * 1024))
                {
                    return "File size Should Be UpTo " + fileSize + "KB";
                    
                }
                else
                {
                    return "Check Picture Is Successfully";
                    
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
                
            }
        }


        public static string UploadWordFile(HttpPostedFileBase file, decimal fileSize)
        {
            try
            {
                var supportedTypes = new[] { "docx","doc" };
                
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return "File Extension Is InValid - Only Upload docx/doc File";

                }
                else if (file.ContentLength > (fileSize * 1024))
                {
                    return "File size Should Be UpTo " + fileSize + "KB";

                }
                else
                {
                    return "OK";

                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;

            }
        }


        public static string UploadExcelFile(HttpPostedFileBase file, decimal fileSize)
        {
            try
            {
                var supportedTypes = new[] { "xlsx", "xls" };
                
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if (!supportedTypes.Contains(fileExt))
                {
                    return "File Extension Is InValid - Only Upload jpg/jpeg/png File";

                }
                else if (file.ContentLength > (fileSize * 1024))
                {
                    return "File size Should Be UpTo " + fileSize + "KB";

                }
                else
                {
                    return "Check Picture Is Successfully";

                }
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;

            }
        }


    }
}

