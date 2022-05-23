using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class StringModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (string.IsNullOrWhiteSpace(valueResult?.AttemptedValue))
            {
                return false;
            }
            string val = valueResult
                .AttemptedValue.Trim()
                .Replace("ي", "ی").Replace("ك", "ک");
            bindingContext.Model = val;
            return true;
        }
    }
}