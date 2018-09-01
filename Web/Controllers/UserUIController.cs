using System.Web.Mvc;
using Web.Autentications;

namespace Web.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class UserUIController : Controller
    {
        public ActionResult GetPanel()
        {
            var contextUser = HttpContext.User;
            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
            {
                return new EmptyResult();
            }

            var user = ((UserIndentity)contextUser.Identity).User;
            return PartialView(user.Role + "Panel");
        }

        public ActionResult GetLogInOutLink()
        {
            var contextUser = HttpContext.User;
            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
            {
                return new EmptyResult();
            }

            var user = ((UserIndentity)contextUser.Identity).User;
            return PartialView("LogOutLink", user.Name);
        }
    }
}