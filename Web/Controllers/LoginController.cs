using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Domain.Entities;
using NHibernateConfigs;
using Web.Autentications;
using Web.Services;

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
        
        public ActionResult Register()
        {
            return View(new RegisterDto());
        }

        [HttpPost]
        public ActionResult Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }

            if (registerDto.RepeatedPassword != registerDto.Password)
            {
                ModelState.AddModelError(nameof(registerDto.Password), "Пароли не совпадают");
                ModelState.AddModelError(nameof(registerDto.RepeatedPassword), "Пароли не совпадают");
                return View(registerDto);
            }

            var userDs = new DataStore<User>();
            var existedUser = userDs.GetAll().SingleOrDefault(user => user.Login == registerDto.Login && user.PasswordHash == string.Empty);
            if (existedUser == null)
            {
                ModelState.AddModelError(nameof(registerDto.Login), "Данный логин недоступен, обратитесь к администратору");
                return View(registerDto);
            }

            existedUser.PasswordHash = new PasswordHashService().GetHash(registerDto.Password);
            userDs.Update(existedUser);

            return RedirectToAction("Index");
        }
    }

    public class RegisterDto : LoginPasswordDto
    {
        [Required]
        public string RepeatedPassword { get; set; }
    }
}