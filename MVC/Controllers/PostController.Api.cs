using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using MVC.Filters;
using MVC.Models;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [CheckLoginFilter]
        [HttpGet("{id}/withBlog")]
        public ActionResult<GetWithBlogOutput> GetWithBlog(int id)
        {
            var post = _postService.GetById(id);
            if (post == null)
                return null;

            //懒加载
            return new GetWithBlogOutput
            {
                BlogId = post.Blog.Id,
                BlogName = post.Blog.Name,
                Content = post.Content,
                Id = post.Id,
                Title = post.Title
            };
        }
    }
}
