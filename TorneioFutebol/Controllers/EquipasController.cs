#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TorneioFutebol.Data;
using TorneioFutebol.Models;

namespace TorneioFutebol.Controllers
{
    public class EquipasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipas.ToListAsync());
        }

        // GET: Equipas
        public async Task<IActionResult> Search(String SearchPhrase)
        {
            List<Equipa> equipas;

            if (SearchPhrase == null)
            {
                equipas = await _context.Equipas.ToListAsync();
            }
            else
            {
                equipas = await _context.Equipas.Where(e => e.TeamName.Contains(SearchPhrase)).ToListAsync();
            }

            return View("Index", equipas);
        }


        // GET: Equipas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipa == null)
            {
                return NotFound();
            }

            return View(equipa);
        }

        // GET: Equipas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamName,Captain,NumberOfMembers")] Equipa equipa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipa);
        }

        // GET: Equipas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas.FindAsync(id);
            if (equipa == null)
            {
                return NotFound();
            }
            return View(equipa);
        }

        // POST: Equipas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamName,Captain,NumberOfMembers")] Equipa equipa)
        {
            if (id != equipa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipaExists(equipa.Id))
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
            return View(equipa);
        }

        // GET: Equipas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipa = await _context.Equipas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipa == null)
            {
                return NotFound();
            }

            return View(equipa);
        }

        // POST: Equipas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipa = await _context.Equipas.FindAsync(id);
            _context.Equipas.Remove(equipa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipaExists(int id)
        {
            return _context.Equipas.Any(e => e.Id == id);
        }
    }
}
