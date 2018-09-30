using DAL.Repository;
using Model;

namespace BLL.Service
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepo;

        public PostService(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }

        public Post GetById(int postId)
        {
            if (postId <= 0)
                return null;

            return _postRepo.GetById(postId);
        }
    }
}
