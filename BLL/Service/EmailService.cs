using DAL.Repository;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Service
{
    public class EmailService : IMsgService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly IUserRepository _userRepo;

        public EmailService(ILogger<EmailService> logger,IUserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool SignIn(int userId)
        {
            if (userId <= 0)
                return false;

            var user = _userRepo.GetById(userId);
            if (user == null)
                return false;

            _logger.LogInformation($"{user.Name}签到成功");
            return true;
        }
    }
}
