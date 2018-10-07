using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.Net_MVC.Models;

namespace Asp.Net_MVC.Controllers
{
    public class PointsController : Controller
    {
        private readonly ScriptContext _context;

        public PointsController(ScriptContext context)
        {
            _context = context;
        }

        // GET: Points
        // Вывод групп и текстов конкретного сценария и конкретного скрипта
        public async Task<IActionResult> Index(int? IdSCE, int? IdSCR) // IdSCE - Id сценария, IdSCR - Id скрипта
        {
            // Попытка реализации объединения 
            var scriptContext = _context.Groups.Include(p => p.Points);
            return View(await scriptContext.ToListAsync());
        }

        // GET: Points/Create
        public IActionResult Create(int? idGroup)
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId");
            return View();
        }

        // POST: Points/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Context,GroupId")] Point point)
        {
            if (ModelState.IsValid)
            {
                _context.Add(point);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Points", new { id = point.Group.SceneId});
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", point.GroupId);
            return View(point);
        }

        // GET: Points/Edit/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var point = await _context.Points.FindAsync(id);
            if (point == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", point.GroupId);
            return View(point);
        }

        // POST: Points/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Context,GroupId")] Point point)
        {
            if (id != point.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(point);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointExists(point.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Points", new { id = point.Group.SceneId });
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "GroupId", "GroupId", point.GroupId);
            return View(point);
        }

        // GET: Points/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var point = await _context.Points
                .Include(p => p.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (point == null)
            {
                return NotFound();
            }

            return View(point);
        }

        // POST: Points/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var point = await _context.Points.FindAsync(id);
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Points", new { id = point.Group.SceneId });
        }

        private bool PointExists(int id)
        {
            return _context.Points.Any(e => e.Id == id);
        }
    }
}
