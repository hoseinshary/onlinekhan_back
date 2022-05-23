using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace NasleGhalam.WebApi.ModelBinderAndFormatter
{
    public class IntegerModelBinder : IModelBinder
    {
        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(
                bindingContext.ModelName);
            if (valueResult?.AttemptedValue == null)
            {
                return false;
            }

            int.TryParse(valueResult
                .AttemptedValue.Trim()
                .Replace(",", ""), out var val);

            bindingContext.Model = val;
            return true;
        }
    }
}