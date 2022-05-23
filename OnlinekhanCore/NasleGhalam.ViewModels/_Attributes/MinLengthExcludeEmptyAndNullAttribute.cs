using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NasleGhalam.ViewModels._Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class MinLengthExcludeEmptyAndNullAttribute : ValidationAttribute
    {
        public MinLengthExcludeEmptyAndNullAttribute(int minLenght)
        {
            MinLeght = minLenght;
        }

        public int MinLeght { private set; get; }


        #region #### server validator ####

        public override bool IsValid(object value)
        {
            if (value == null || value.ToString() == "" || value.ToString().Length >= MinLeght)
            {
                return true;
            }

            return false;
        }


        public override string FormatErrorMessage(string name)
        {
            return string.Format((IFormatProvider)CultureInfo.CurrentCulture, ErrorMessageString, new object[2]
            {
                (object) name,
                (object) MinLeght
            });
        }


        #endregion

    }
}
