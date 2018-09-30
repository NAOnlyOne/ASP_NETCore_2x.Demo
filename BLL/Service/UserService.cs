using DAL.Repository;
using Model;

namespace BLL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMsgService _msgService;

        public UserService(IUserRepository userRepo, IMsgService msgService)
        {
            _userRepo = userRepo;
            _msgService = msgService;
        }

        public int Add(User user)
        {
            if (user == null)
                return 0;

            return _userRepo.Add(user);
        }

        public User GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return _userRepo.GetByName(name);
        }
    }
}
