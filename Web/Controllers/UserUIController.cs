using System.Web.Mvc;
using Domain.Interfaces.Users;

namespace Web.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class UserUIController : Controller
    {
        public ICurrentOperatorService CurrentOperatorService { get; set; }

        public ActionResult GetPanel()
        {
            var contextUser = HttpContext.User;
            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
            {
                return new EmptyResult();
            }

            var user = CurrentOperatorService.GetCurrentUser();
            return PartialView(user.Role + "Panel");
        }

        public ActionResult GetLogInOutLink()
        {
            var contextUser = HttpContext.User;
            if (contextUser == null || !contextUser.Identity.IsAuthenticated)
            {
                return new EmptyResult();
            }

            var user = CurrentOperatorService.GetCurrentUser();
            return PartialView("LogOutLink", user.Name);
        }
    }
}