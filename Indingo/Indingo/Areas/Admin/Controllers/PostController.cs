using Indingo.Base.Extension;
using Indingo.DAL;
using Indingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indingo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public PostController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Post> posts = _context.Posts.ToList();
            return View(posts);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid) { return View(); }
            if (post == null)
            {
                ModelState.AddModelError("Photo", "Zehmet olmazsa dogru doldurun");
                return View();
            }
            if (!post.Photo.CheckSizeFile(300))
            {
                ModelState.AddModelError("Photo", "File olcusunu max 300 kb olaraq secin");
                return View();
            }
            if (!post.Photo.CheckTypeFile("image/"))
            {
                ModelState.AddModelError("Photo", "File tipiniduzgun olaraq secin");
                return View();
            }
            post.Image = await post.Photo.CreateFileAsync(_env.WebRootPath, "assets/images");
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!ModelState.IsValid) { return View(); }
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid) { return View(); }
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Post post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return NotFound();
            }



            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Post post)
        {
            if (!ModelState.IsValid) { return View(); }
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Post existing = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null)
            {
                return NotFound();
            }
            if(existing.Photo!= null)
            {

                if (!post.Photo.CheckSizeFile(300))
                {
                    ModelState.AddModelError("Photo", "File olcusunu max 300 kb olaraq secin");
                    return View();
                }
                if (!post.Photo.CheckTypeFile("image/"))
                {
                    ModelState.AddModelError("Photo", "File tipiniduzgun olaraq secin");
                    return View();
                }

            }
            existing.Image.Delete(_env.WebRootPath, "assets/images");
            existing.Image = await post.Photo.CreateFileAsync(_env.WebRootPath, "assets/images");
            existing.Tittle= post.Tittle;
            existing.Description= post.Description;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
