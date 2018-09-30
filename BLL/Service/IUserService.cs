using Model;

namespace BLL.Service
{
    public interface IUserService
    {
        int Add(User user);

        User GetByName(string name);
    }
}
