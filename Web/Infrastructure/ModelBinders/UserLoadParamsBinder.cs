using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Controllers;

namespace Web.Infrastructure.ModelBinders
{
    public class UserLoadParamsBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var userLoadParams = new UserLoadParams();

            var form = controllerContext.RequestContext.HttpContext.Request.Form;
            var page = Convert.ToInt32(form["page"]);
            if (page > 0)
            {
                userLoadParams.Page = page;
            }

            var keys = form.AllKeys.Where(key => key.StartsWith("filter."));
            userLoadParams.FilterParams = keys.Select(key => new
                {
                    Key = key,
                    Value = form[key]
                })
                .ToDictionary(d => d.Key, d => d.Value);

            return userLoadParams;
        }
    }
}