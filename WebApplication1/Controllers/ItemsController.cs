using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MyAppContext _context;
        public ItemsController(MyAppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items.ToListAsync();
            return View(items);
        }

        public IActionResult Add()
        {
            return View(new Item());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Id", "Name", "Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(item);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x=> x.Id== id);
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Items/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}
