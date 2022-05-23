using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using NasleGhalam.Common;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class DateTimeModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (string.IsNullOrWhiteSpace(valueResult?.AttemptedValue))
                return false;

            bindingContext.Model = valueResult.AttemptedValue.ToMiladiDateTime();
            return true;
        }
    }
}