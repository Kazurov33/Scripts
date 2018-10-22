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
    // Контроллер сценариев 

    public class ScenesController : Controller
    {
        private readonly ScriptContext _context;

        public ScenesController(ScriptContext context)
        {
            _context = context;
        }

        // GET: Scenes
        public async Task<IActionResult> IndexScene(int? IdSCR) // входящие данные - id развернутого скрипта
        {
            var scriptContext = _context.Scenes.Where(s => s.ScriptId == IdSCR);
            return View(await scriptContext.ToListAsync());
           
        }

        // GET: Scenes/CreateScene
        public IActionResult CreateScene(int IdSCR)
        {
            /* Попытка реализовать автоматическое заполнения поля ScriptId при создания сценария
             * + Проверка данного значения, если такого Id не найдено то переадрессация на главную страницу */
             //Script script = _context.Scripts.Find(IdSCR);
           //    if (script != null)
          //   {
                ViewData["ScriptId"] = new SelectList(_context.Scripts, "ScriptId", "ScriptId");
                return View(); 
          //  }

        //   return RedirectToAction("IndexScript", "Scripts");
        }

        // POST: Scenes/CreateScene
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateScene([Bind("SceneId,Name,ScriptId")] Scene scene)
        {       
            if (ModelState.IsValid)
            {
                _context.Add(scene);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexScene", "Scenes", new { IdSCE = scene.ScriptId });
            }
            ViewData["ScriptId"] = new SelectList(_context.Scripts, "ScriptId", "ScriptId", scene.ScriptId);
            return View(scene);
        }

        // GET: Scenes/EditScene
        public async Task<IActionResult> EditScene(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }
            ViewData["ScriptId"] = new SelectList(_context.Scripts, "ScriptId", "ScriptId", scene.ScriptId);

            return View(scene);
        }

        // POST: Scenes/EditScene
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditScene(int id, [Bind("SceneId,Name,ScriptId")] Scene scene)
        {
            if (id != scene.SceneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneExists(scene.SceneId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("IndexScene", "Scenes", new { IdSCR = scene.ScriptId });
            }
            ViewData["ScriptId"] = new SelectList(_context.Scripts, "ScriptId", "ScriptId", scene.ScriptId);
            return View(scene);
        }

        // GET: Scenes/DeleteScene/
        public async Task<IActionResult> DeleteScene(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes
                .Include(s => s.Script)
                .FirstOrDefaultAsync(m => m.SceneId == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // POST: Scenes/DeleteScene/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scene = await _context.Scenes.FindAsync(id);
            _context.Scenes.Remove(scene);
            await _context.SaveChangesAsync();
            return RedirectToAction("IndexScene", "Scenes", new { IdSCE = scene.SceneId });
        }

        private bool SceneExists(int id)
        {
            return _context.Scenes.Any(e => e.SceneId == id);
        }
    }
}
