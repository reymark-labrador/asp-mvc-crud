using CrudApplication.Data;
using CrudApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudLibrary;

namespace CrudApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly CrudContext _crudContext;

        public UserController(CrudContext crudContext)
        {
            _crudContext = crudContext;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var users = await _crudContext.Users.ToListAsync();
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _crudContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Password")] UserModel user)
        {
            if (ModelState.IsValid)
            {
                user.Password = Cipher.Encrypt(user.Password);
                _crudContext.Add(user);
                await _crudContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _crudContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = Cipher.Decrypt(user.Password);
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, UserModel user)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (user != null)
            {
                user.Password = Cipher.Encrypt(user.Password);
                _crudContext.Update(user);
                await _crudContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _crudContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _crudContext.Users.FindAsync(id);
            _crudContext.Users.Remove(course);
            await _crudContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
