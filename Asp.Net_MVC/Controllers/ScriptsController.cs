using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.Net_MVC.Models;

namespace Asp.Net_MVC.Views
{
    // Контроллер для класса скрипт

    public class ScriptsController : Controller
    {
        private readonly ScriptContext _context;

        public ScriptsController(ScriptContext context)
        {
            _context = context;
        }

        // GET: ScriptsScript
        public async Task<IActionResult> IndexScript()
        {
            return View(await _context.Scripts.ToListAsync());
        }

        // GET: Scripts/CreateScript
        public IActionResult CreateScript()
        {
            return PartialView();
        }

        // POST: Scripts/CreateScript
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScript([Bind("ScriptId,Name,Date,Count")] Script script)
        {
            if (ModelState.IsValid)
            {
                _context.Add(script);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexScript));
            }
            return View(script);
        }

        // GET: Scripts/EditScript
        public async Task<IActionResult> EditScript(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var script = await _context.Scripts.FindAsync(id);
            if (script == null)
            {
                return NotFound();
            }
            return PartialView(script);
        }

        // POST: Scripts/EditScript
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditScript(int id, [Bind("ScriptId,Name,Date,Count")] Script script)
        {
            if (id != script.ScriptId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(script);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScriptExists(script.ScriptId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(IndexScript));
            }
            return View(script);
        }

        // GET: Scripts/DeleteScript
        public async Task<IActionResult> DeleteScript(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var script = await _context.Scripts
                .FirstOrDefaultAsync(m => m.ScriptId == id);
            if (script == null)
            {
                return NotFound();
            }

            return PartialView(script);
        }

        // POST: Scripts/DeleteScript
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var script = await _context.Scripts.FindAsync(id);
            _context.Scripts.Remove(script);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexScript));
        }

        private bool ScriptExists(int id)
        {
            return _context.Scripts.Any(e => e.ScriptId == id);
        }
    }
}
