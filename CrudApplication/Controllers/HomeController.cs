using CrudApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CrudApplication.Data;
using Microsoft.EntityFrameworkCore;

namespace CrudApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CrudContext _crudContext;

        public HomeController(
            ILogger<HomeController> logger,
            CrudContext crudContext)
        {
            _logger = logger;
            _crudContext = crudContext;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _crudContext.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
