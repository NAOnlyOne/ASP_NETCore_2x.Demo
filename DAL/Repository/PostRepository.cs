using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace DAL.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly CustomDbContext _dbContext;

        public PostRepository(CustomDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Post GetById(int postId)
        {
            if (postId <= 0)
                return null;

            return _dbContext.Posts.Find(postId);
        }
    }
}
