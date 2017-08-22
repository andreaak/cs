using _01_ASPMVCTest.Areas._003_Model.Models;
using System.Web.Mvc;

namespace _01_ASPMVCTest.ModelBinder
{
    public class MyModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // создаем новую или работаем с готовой моделью.
            M_02_TestModelSuccessor model = (M_02_TestModelSuccessor)bindingContext.Model ?? new M_02_TestModelSuccessor();

            bool hasPrefix = bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName);

            string searchPrefix = hasPrefix ? bindingContext.ModelName + "." : "";

            model.Prop1 = int.Parse(GetValue(bindingContext, searchPrefix, "Prop1"));
            model.Prop2 = GetValue(bindingContext, searchPrefix, "Prop2");

            return model;
        }

        private string GetValue(ModelBindingContext bindingContext, string prefix, string key)
        {
            ValueProviderResult vpr = bindingContext.ValueProvider.GetValue(prefix + key);
            return vpr == null ? null : vpr.AttemptedValue;
        }
    }
}