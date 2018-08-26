using System.Web.Mvc;
using Web.Autentications;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new LoginPasswordDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginPasswordDto loginPasswordDto, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginPasswordDto);
            }

            var authedUser = AuthHttpModule.BadDesicionAutentication.Login(loginPasswordDto);
            if (authedUser == null)
            {
                ModelState.AddModelError("",
                    "Не найден пользователь с такой комбинацией логина и пароля, обратитесь к одну из администратору");
                return View(loginPasswordDto);
            }

            returnUrl = returnUrl ?? Url.Action("Index", "Home");

            return Redirect(returnUrl);
        }

        public ActionResult LogOut()
        {
            AuthHttpModule.BadDesicionAutentication.LogOut();
            return Redirect(Url.RouteUrl(new {controller = "Home", action = "Index"}));
        }
    }
}