using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public static class DbInitializer
    {
        public static void Seed(this CustomDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                dbContext.Users.Add(new User
                {
                    Name = "yaoweijie",
                    Blog = new Blog
                    {
                        Name = "姚伟杰的博客",
                        Posts = new[]
                        {
                            new Post
                            {
                                Title = "测试",
                                Content = "待完善"
                            }
                        }
                    }
                });
                dbContext.SaveChanges();
            }
        }
    }
}
