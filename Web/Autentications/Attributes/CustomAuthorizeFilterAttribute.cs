using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core;
using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class CustomAuthorizeFilterAttribute : ActionFilterAttribute
    {
        public CustomAuthorizeFilterAttribute(params ERole[] roles)
        {
            Roles = roles;
        }

        public IReadOnlyList<ERole> Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var authorizationService = MvcApplication.Container.Resolve<IAuthorizationService>();
            var isInRole = Roles.Any(authorizationService.IsInRole);
            if (!isInRole)
            {
                filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            }
        }
    }
}