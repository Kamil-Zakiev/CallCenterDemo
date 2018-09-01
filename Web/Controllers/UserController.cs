using System.Web.Mvc;
using Domain.Enums;
using Domain.Interfaces.Users;
using Domain.Interfaces.Users.Dto;
using Web.Autentications.Attributes;

namespace Web.Controllers
{
    [AdminOnly]
    public class UserController : Controller
    {
        public IUserService UserService { get; set; }

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
            
            var result = UserService.CreateNewUser(newUserDto);
            if (!result.Success)
            {
                ModelState.AddModelError(nameof(newUserDto.Login), result.Message);
                return View(newUserDto);
            }

            TempData["Message"] = "Пользователь успешно добавлен.";
            return RedirectToAction("ViewAll");
        }

        public ActionResult ViewAll()
        {
            return View(UserService.GetUsers());
        }
    }
}