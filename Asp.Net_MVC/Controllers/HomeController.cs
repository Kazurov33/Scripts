using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.Net_MVC.Models;

namespace Asp.Net_MVC.Controllers
{
    public class HomeController : Controller
    {
        ScriptContext db;
        public HomeController(ScriptContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            return View(db.Scripts.ToList());
        }
    }
}
