using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webapp1.Data;
using webapp1.Models;

namespace webapp1.Controllers
{
    public class tesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public tesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: tes
        public async Task<IActionResult> Index()
        {
            return View(await _context.tes.ToListAsync());
        }
        // GET: tes/Search
        public async Task<IActionResult> Search()
        {
            return View();
        }

        // POST: tes/ShowSearch
        public async Task<IActionResult> ShowSearch(String SearchText)
        {
            return View("Index",await _context.tes.Where(t => t.question.Contains(SearchText)).ToListAsync());
        }

        // GET: tes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tes = await _context.tes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tes == null)
            {
                return NotFound();
            }

            return View(tes);
        }

        // GET: tes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: tes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,question,answer")] tes tes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tes);
        }

        // GET: tes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tes = await _context.tes.FindAsync(id);
            if (tes == null)
            {
                return NotFound();
            }
            return View(tes);
        }

        // POST: tes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,question,answer")] tes tes)
        {
            if (id != tes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!tesExists(tes.Id))
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
            return View(tes);
        }

        // GET: tes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tes = await _context.tes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tes == null)
            {
                return NotFound();
            }

            return View(tes);
        }

        // POST: tes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tes = await _context.tes.FindAsync(id);
            if (tes != null)
            {
                _context.tes.Remove(tes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool tesExists(int id)
        {
            return _context.tes.Any(e => e.Id == id);
        }
    }
}
