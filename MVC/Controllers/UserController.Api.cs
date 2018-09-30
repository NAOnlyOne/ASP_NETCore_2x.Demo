using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Model;
using MVC.Filters;
using MVC.Models;
using System.Text;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMsgService _msgService;

        public UserController(IUserService userService, IMsgService msgService)
        {
            _userService = userService;
            _msgService = msgService;
        }

        [HttpPost]
        public ActionResult<int> Add([FromBody]AddUserInput dto)
        {
            if (!ModelState.IsValid)
                return 0;

            var user = new User { Name = dto.Name };
            return _userService.Add(user);
        }

        [HttpPost("signIn")]
        [CheckLoginFilter]
        public ActionResult<bool> SignIn()
        {
            bool succeed = HttpContext.Session.TryGetValue("UserId", out var bytes);
            string userIdStr = succeed ? Encoding.UTF8.GetString(bytes) : null;
            int.TryParse(userIdStr, out int userId);
            return _msgService.SignIn(userId);
        }
    }
}
