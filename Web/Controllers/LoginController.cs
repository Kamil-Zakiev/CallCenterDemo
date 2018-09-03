using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Core;
using Domain;
using Domain.Entities;
using Domain.Interfaces.Users;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        public IDataStore<User> UserDataStore { get; set; }

        public IPasswordHashService PasswordHashService { get; set; }

        public IAuthenticationService CustomAutentication { get; set; }

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

            var authedUser = CustomAutentication.Login(new LoginPassDto
            {
                Login = loginPasswordDto.Login,
                Password = loginPasswordDto.Password,
                RememberMe = loginPasswordDto.RememberMe
            });
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
            CustomAutentication.LogOut();
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

            var existedUser = UserDataStore.GetAll()
                .SingleOrDefault(user => user.Login == registerDto.Login && user.PasswordHash == string.Empty);
            if (existedUser == null)
            {
                ModelState.AddModelError(nameof(registerDto.Login),
                    "Данный логин недоступен, обратитесь к администратору");
                return View(registerDto);
            }

            existedUser.PasswordHash = PasswordHashService.GetHash(registerDto.Password);
            UserDataStore.Update(existedUser);

            return RedirectToAction("Index");
        }
    }

    public class RegisterDto : LoginPasswordDto
    {
        [Required]
        public string RepeatedPassword { get; set; }
    }
}