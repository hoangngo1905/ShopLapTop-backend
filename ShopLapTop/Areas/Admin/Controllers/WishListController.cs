using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopLapTop.Model.Entities;

namespace ShopLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WishListController : Controller
    {
        private readonly ShopLapTopContext _context;

        public WishListController(ShopLapTopContext context)
        {
            _context = context;
        }

        // GET: Admin/WishList
        public async Task<IActionResult> Index()
        {
            var shopLapTopContext = _context.WishLists.Include(w => w.Product).Include(w => w.User);
            return View(await shopLapTopContext.ToListAsync());
        }

        // GET: Admin/WishList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .Include(w => w.Product)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }

        // GET: Admin/WishList/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username");
            return View();
        }

        // POST: Admin/WishList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,UserId")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wishList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", wishList.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", wishList.UserId);
            return View(wishList);
        }

        // GET: Admin/WishList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", wishList.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", wishList.UserId);
            return View(wishList);
        }

        // POST: Admin/WishList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,UserId")] WishList wishList)
        {
            if (id != wishList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wishList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WishListExists(wishList.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", wishList.ProductId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Username", wishList.UserId);
            return View(wishList);
        }

        // GET: Admin/WishList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wishList = await _context.WishLists
                .Include(w => w.Product)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wishList == null)
            {
                return NotFound();
            }

            return View(wishList);
        }

        // POST: Admin/WishList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wishList = await _context.WishLists.FindAsync(id);
            if (wishList != null)
            {
                _context.WishLists.Remove(wishList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishListExists(int id)
        {
            return _context.WishLists.Any(e => e.Id == id);
        }
    }
}
