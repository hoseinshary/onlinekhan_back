using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;


namespace NasleGhalam.Common
{
    public static class Utility
    {
        #region ### DateTime ###

        public static string ToPersianDate(this DateTime mDateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime mDate = DateTime.Parse(mDateTime.ToString());
            int day = pc.GetDayOfMonth(mDate);
            int month = pc.GetMonth(mDate);
            int year = pc.GetYear(mDate);

            return string.Format("{0:0000}/{1:00}/{2:00}", year, month, day);
        }

        public static string ToPersianDateTime(this DateTime mDateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            DateTime mDate = DateTime.Parse(mDateTime.ToString());
            int day = pc.GetDayOfMonth(mDate);
            int month = pc.GetMonth(mDate);
            int year = pc.GetYear(mDate);

            return string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", year, month, day, mDate.Hour,
                mDate.Minute, mDate.Second);
        }

        public static DateTime? ToMiladiDateTime(this string pDateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime thisDate = DateTime.Now;

            string[] arr_dateTime = pDateTime.Split(new char[] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            if (arr_dateTime.Length == 2) // date and time
            {
                try
                {
                    string[] arr_date = arr_dateTime[0].Split('/', '\\', '-');
                    string[] arr_time = arr_dateTime[1].Split(':');

                    int pDay;
                    int pYear;
                    if (arr_date[2].Length == 4)
                    {
                        pDay = Convert.ToInt16(arr_date[0]);
                        pYear = Convert.ToInt16(arr_date[2]);
                    }
                    else
                    {
                        pDay = Convert.ToInt16(arr_date[2]);
                        pYear = Convert.ToInt16(arr_date[0]);
                    }

                    int pMonth = Convert.ToInt16(arr_date[1]);

                    thisDate = pc.ToDateTime(pYear, pMonth, pDay,
                        Convert.ToInt32(arr_time[0]), Convert.ToInt32(arr_time[1]), Convert.ToInt32(arr_time[2]), 0);

                }
                catch
                {
                    return null;
                }
            }
            else if (arr_dateTime.Length == 1) // only date
            {
                try
                {
                    string[] arr_date = arr_dateTime[0].Split('/', '\\', '-');

                    int pDay;
                    int pYear;
                    if (arr_date[2].Length == 4)
                    {
                        pDay = Convert.ToInt16(arr_date[0]);
                        pYear = Convert.ToInt16(arr_date[2]);
                    }
                    else
                    {
                        pDay = Convert.ToInt16(arr_date[2]);
                        pYear = Convert.ToInt16(arr_date[0]);
                    }

                    int pMonth = Convert.ToInt16(arr_date[1]);

                    thisDate = pc.ToDateTime(pYear, pMonth, pDay, 0, 0, 0, 0);

                }
                catch
                {
                    return null;
                }
            }

            return thisDate;
        }

        #endregion


        #region ### Access ###

        public static string SumBinary(string a, string b)
        {
            int a_len = a.Length;
            int b_len = b.Length;

            int max = Math.Max(a_len, b_len);

            StringBuilder sum = new StringBuilder("");
            int carry = 0;

            for (int i = 0; i < max; i++)
            {
                int m = GetBit(a, a_len - i - 1);
                int n = GetBit(b, b_len - i - 1);
                int add = m + n + carry;
                sum.Insert(0, add % 2);
                carry = add / 2;
            }

            if (carry == 1)
                sum.Insert(0, "1");

            return sum.ToString();
        }


        /// <summary>
        /// Find index of action bit from sumOfAction and replace it to 1
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static string AddAccess(string sumOfAction, int actionBit)
        {
            StringBuilder strSumAction = new StringBuilder(sumOfAction);
            while (strSumAction.Length <= actionBit)
            {
                strSumAction.Insert(0, "0");
            }

            // پیدا کردن اندکس بیت و جاگذاری آن با یک
            strSumAction.Replace('0', '1', strSumAction.Length - actionBit - 1, 1);

            return strSumAction.ToString();
        }

        /// <summary>
        /// Find index of action bit from sumOfAction and replace it to 0
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static string RemoveAccess(string sumOfAction, int actionBit)
        {
            StringBuilder strSumAction = new StringBuilder(sumOfAction);
            while (strSumAction.Length <= actionBit)
            {
                strSumAction.Insert(0, "0");
            }

            strSumAction.Replace('1', '0', strSumAction.Length - actionBit - 1, 1);

            return strSumAction.ToString();
        }

        /// <summary>
        /// Add two binary string
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AndBinary(string a, string b)
        {
            int a_len = a.Length;
            int b_len = b.Length;

            int max = Math.Max(a_len, b_len);

            StringBuilder and = new StringBuilder("");
            for (int i = 0; i < max; i++)
            {
                int m = GetBit(a, a_len - i - 1);
                int n = GetBit(b, b_len - i - 1);
                and.Insert(0, m & n);
            }

            return and.ToString();
        }

        private static int GetBit(string s, int index)
        {
            if (index < 0 || index >= s.Length)
                return 0;

            if (s[index] == '0')
                return 0;
            else
                return 1;
        }


        /// <summary>
        /// Check if index of action bit from sum of action be 1
        /// </summary>
        /// <param name="sumOfAction"></param>
        /// <param name="actionBit"></param>
        /// <returns></returns>
        public static bool HasAccess(string sumOfAction, short actionBit)
        {
            string strActionBit = ConvertIntToBit(actionBit, sumOfAction.Length);
            string result = AndBinary(sumOfAction, strActionBit);

            return result == strActionBit;
        }

        public static bool HasAccess(string sumOfAction, short[] actionBits)
        {
            foreach (short actionBit in actionBits)
            {
                string strActionBit = ConvertIntToBit(actionBit, sumOfAction.Length);
                string result = AndBinary(sumOfAction, strActionBit);

                if (result == strActionBit)
                    return true;
            }

            return false;
        }

        private static string ConvertIntToBit(int actionBit, int len)
        {
            StringBuilder strBulderAction = new StringBuilder("1");
            for (int i = 0; i < actionBit; i++)
            {
                strBulderAction.Append("0");
            }

            while (len > strBulderAction.Length)
            {
                strBulderAction.Insert(0, "0");
            }

            return strBulderAction.ToString();
        }

        #endregion


        #region ### Extension ###

        public static bool CheckImageExtension(string extension)
        {
            extension = extension.ToLower();
            return (extension == ".jpg" || extension == ".gif" || extension == ".jpeg"
                    || extension == ".png" || extension == ".bmp" || extension == ".icn");
        }

        public static bool CheckImageProfileExtension(string extension)
        {
            extension = extension.ToLower();
            return (extension == ".jpg");
        }

        public static bool CheckImageQuestionExtension(string extension)
        {
            extension = extension.ToLower();
            return (extension == ".png");
        }


        public static bool CheckWordFileExtension(string extension)
        {
            extension = extension.ToLower();
            return (extension == ".doc" || extension == ".docx");
        }

        public static bool CheckExcelFileExtension(string extension)
        {
            extension = extension.ToLower();
            return (extension == ".xls" || extension == ".xlsx");
        }

        #endregion

        #region ### Enum ###

        public static string GetDisplayName<TEnum>(this TEnum e)
        {
            var displayName = "";
            try
            {
                var enumType = e.GetType().GetEnumName(e);
                if (enumType == null)
                    return displayName;

                var memInfo = e.GetType().GetMember(enumType);
                var displayNameAttributes = memInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                displayName = displayNameAttributes.Length > 0
                    ? ((DisplayAttribute) displayNameAttributes[0]).Name
                    : e.ToString();
            }
            catch
            {
                displayName = "";
            }

            return displayName;
        }

        public static Dictionary<int, string> EnumToDictionary<T>()
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("Type must be an enum");

            return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(t => (int) (object) t, t => t.GetDisplayName());
        }

        #endregion

        public static DataTable ConvertToDataTable<T>(List<T> models)
        {
            // creating a data table instance and typed it as our incoming model   
            // as I make it generic, if you want, you can make it the model typed you want.  
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties of that model  
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties              
            // Adding Column name to our datatable  
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names    
                dataTable.Columns.Add(prop.CustomAttributes.First().NamedArguments[0].TypedValue.Value.ToString());

            }
            // Adding Row and its value to our dataTable  
            foreach (T item in models)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows    
                    values[i] = Props[i].GetValue(item, null);
                }
                // Finally add value to datatable    
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        public static List<int> ToInts(this IList<bool> list)
        {
            var returnList = new List<int>();
            foreach (var b in list)
            {
                if (b)
                {
                    returnList.Add(1);
                }
                else
                {
                    returnList.Add(0);
                }
            }

            return returnList;
        }
    }
}
