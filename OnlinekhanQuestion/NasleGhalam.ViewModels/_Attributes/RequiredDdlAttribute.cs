using System;
using System.ComponentModel.DataAnnotations;

namespace NasleGhalam.ViewModels._Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RequiredDdlValidator : ValidationAttribute
    {
        public RequiredDdlValidator(String invalidValue)
        {
            InvalidValue = invalidValue;
        }

        public String InvalidValue { private set; get; }


        #region #### server validator ####
        public override bool IsValid(object value)
        {
            if (value == null) return false;

            return value.ToString() != InvalidValue;
        }
        #endregion
    }
}