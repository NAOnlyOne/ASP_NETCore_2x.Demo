using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public interface IPostRepository
    {
        Post GetById(int postId);
    }
}
