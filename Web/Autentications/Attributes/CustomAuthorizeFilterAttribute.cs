using System.Web.Mvc;
using Domain.Enums;

namespace Web.Autentications.Attributes
{
    public class CustomAuthorizeFilterAttribute : ActionFilterAttribute
    {
        public ERole Role { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // todo: container resolve
            var userPrincipal = filterContext.Controller.ControllerContext.HttpContext.User;
            var isInRole = userPrincipal.IsInRole(Role.ToString());
            if (!isInRole)
            {
                filterContext.Result = new RedirectResult("~/Home/AccessDenied");
            }
        }
    }
}