using Indingo.DAL;
using Indingo.Migrations;
using Indingo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indingo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;


        public SettingsController(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IActionResult> Index()
        {

            List<Setting> settings = await _context.Settings.ToListAsync();

            return View(settings);
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (!ModelState.IsValid) { return View(); }
            if (id == null || id < 1)
            {
                return BadRequest();
            }
            Setting setting = await _context.Settings.FirstOrDefaultAsync(s => s.Id == id);
            if (setting == null)
            {
                return NotFound();
            }
            return View(setting);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Setting setting)
        {
            Setting exist = await _context.Settings.FirstOrDefaultAsync(s => s.Id == setting.Id);
            if (exist == null) { return NotFound(); }
            if (!ModelState.IsValid) { return View(); }
            exist.Value = setting.Value;
            await _context.SaveChangesAsync();



            return RedirectToAction("Index");
        }

    }
}
