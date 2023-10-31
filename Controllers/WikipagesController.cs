using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampaignWiki.Data;
using CampaignWiki.Models;

namespace CampaignWiki.Controllers
{
    public class WikipagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WikipagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wikipages
        public async Task<IActionResult> Index()
        {
              return _context.Wikipage != null ? 
                          View(await _context.Wikipage.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Wikipage'  is null.");
        }

        // GET: Wikipages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Wikipage == null)
            {
                return NotFound();
            }

            var wikipage = await _context.Wikipage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wikipage == null)
            {
                return NotFound();
            }

            return View(wikipage);
        }

        // GET: Wikipages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wikipages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Wikipage wikipage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wikipage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(wikipage);
        }

        // GET: Wikipages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Wikipage == null)
            {
                return NotFound();
            }

            var wikipage = await _context.Wikipage.FindAsync(id);
            if (wikipage == null)
            {
                return NotFound();
            }
            return View(wikipage);
        }

        // POST: Wikipages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Wikipage wikipage)
        {
            if (id != wikipage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wikipage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WikipageExists(wikipage.Id))
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
            return View(wikipage);
        }

        // GET: Wikipages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Wikipage == null)
            {
                return NotFound();
            }

            var wikipage = await _context.Wikipage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wikipage == null)
            {
                return NotFound();
            }

            return View(wikipage);
        }

        // POST: Wikipages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wikipage == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Wikipage'  is null.");
            }
            var wikipage = await _context.Wikipage.FindAsync(id);
            if (wikipage != null)
            {
                _context.Wikipage.Remove(wikipage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WikipageExists(int id)
        {
          return (_context.Wikipage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
