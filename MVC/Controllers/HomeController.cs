using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult<bool> Login(string userName)
        {
            var user = _userService.GetByName(userName);
            if (user == null)
                return false;

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return true;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
