using Indingo.DAL;
using Indingo.Models;
using Indingo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Indingo.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
       

        public HomeController(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IActionResult> Index()
        {
            var post = await _context.Posts.ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Posts = post
            };

            return View(homeVM);
        }

     
    }
}