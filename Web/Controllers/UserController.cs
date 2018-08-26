using System.Web.Mvc;
using Domain.Enums;
using Web.Autentications.Attributes;
using Web.Models.Users;
using Web.Services;

namespace Web.Controllers
{
    [AdminOnly]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult AddNew()
        {
            return View(new NewUserDto()
            {
                Role = ERole.Operator
            });
        }

        [HttpPost]
        public ActionResult AddNew(NewUserDto newUserDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Введены некорректные данные");
                return View(newUserDto);
            }

            var userService = new UserService();
            userService.CreateNewUser(newUserDto);
            TempData["Message"] = "Пользователь успешно добавлен.";
            
            return View(newUserDto);
        }

        public ActionResult ViewAll()
        {
            var userService = new UserService();
            var users = userService.GetUsers();
            return View(users);
        }
    }
}