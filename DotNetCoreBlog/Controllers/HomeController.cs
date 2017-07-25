using DotNetCoreBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace DotNetCoreBlog.Controllers
{
    public class HomeController : Controller
    {
        private BlogService _service;

        public HomeController(BlogService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index([FromServices] IOptions<AppSettings> settings)
        {
            return View(await _service.GetRecentPostsAsync(settings.Value.MaxPostsOnHomepage));
        }

        public IActionResult About() => View();

        [HttpGet]
        public async Task<IActionResult> Blog(string id)
        {
            var post = await _service.GetPostByUrlAsync(id);
            if (post != null)
            {
                return View(post);
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult Write()
        {
            return View(new WritePostModel());
        }

        [HttpPost]
        public async Task<IActionResult> Write(WritePostModel model)
        {
            if (model.Title == "Break me")
            {
                throw new InvalidOperationException("OK then!");
            }

            if (ModelState.IsValid)
            {
                var post = await _service.WritePostAsync(model.UrlSlug, model.Title, model.Summary, model.Body, model.Tags);
                return RedirectToAction(nameof(Blog), new { Id = post.UrlSlug });
            }

            return View(model);
        }

        public IActionResult Error(string id = null)
        {
            ViewBag.PageClass = "error";
            return View(id ?? "500");
        }
    }
}
