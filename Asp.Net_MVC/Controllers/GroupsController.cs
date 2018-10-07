﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.Net_MVC.Models;

namespace Asp.Net_MVC.Controllers
{
    // Контроллер для создания, удаления и редактирования групп

    public class GroupsController : Controller
    {
        private readonly ScriptContext _context;

        public GroupsController(ScriptContext context)
        {
            _context = context;
        }

        // GET: Groups/CreateGroup
        public IActionResult CreateGroup()
        {
            ViewData["SceneId"] = new SelectList(_context.Scenes, "SceneId", "SceneId");
            return View();
        }

        // POST: Groups/CreateGroup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroup([Bind("GroupId,Name,SceneId")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                _context.AddRange( //автоматическое добавление в группу первого текста при создании
                    new Point
                    {
                        Name = "Выражение",
                        Context = "Добавьте сюда ваше выражение",
                        GroupId = group.GroupId,
                    });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Points", new { id = group.SceneId });
            }
            ViewData["SceneId"] = new SelectList(_context.Scenes, "SceneId", "SceneId", @group.SceneId);
            return View(@group);
        }

        // GET: Groups/EditGroup/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            ViewData["SceneId"] = new SelectList(_context.Scenes, "SceneId", "SceneId", @group.SceneId);
            return View(@group);
        }

        // POST: Groups/EditGroup/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroup(int id, [Bind("GroupId,Name,SceneId")] Group @group)
        {
            if (id != @group.GroupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.GroupId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Points", new { id = group.SceneId });
            }
            ViewData["SceneId"] = new SelectList(_context.Scenes, "SceneId", "SceneId", @group.SceneId);
            return View(@group);
        }

        // GET: Groups/DeleteGroup/
        public async Task<IActionResult> DeleteGroup(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var group = await _context.Groups
                .Include(s => s.Scene)
                .FirstOrDefaultAsync(m => m.GroupId == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }

        // POST: Groups/DeleteGroup/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            int oldId = group.SceneId;
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Points", new { id = oldId });
        }

        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.GroupId == id);
        }
    }
}
