using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using webapi.data;
using webapi.Models;

namespace CRUD_WEBSITE_PERSONS.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsersContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public UsersController(UsersContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var persons = await _context.Users.ToListAsync();
            return View(persons);
        }

        private string UpLoadFile(Users user)
        {
            string fileName = null;
            if (user.ImageFile != null)
            {
                string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.ImageFile.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create(Users user)
        {
            if (ModelState.IsValid)
            {
                user.imageUrl = ""; 
                if (user.ImageFile != null)
                {
                    string fileName = UpLoadFile(user);
                    user.imageUrl = fileName;
                }

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null) return BadRequest();
            var person = await _context.Users.FirstOrDefaultAsync(m => m.Id== id);
            if (person == null) return BadRequest();

            _context.Users.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var person = await _context.Users.FirstOrDefaultAsync(c => c.Id== id);
            if (person == null) return NotFound();
            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Users user)
        {
            // ตรวจสอบว่า id ที่ส่งมาตรงกับ user.Id ในฟอร์มหรือไม่
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                // 1. ตรวจสอบว่ามีการอัปโหลดไฟล์ใหม่หรือไม่
                if (user.ImageFile != null)
                {
                    // 2. ถ้ามีไฟล์ใหม่: ให้ลบไฟล์เก่า (ถ้ามี)
                    if (!string.IsNullOrEmpty(user.imageUrl))
                    {
                        string oldFilePath = Path.Combine(_hostEnvironment.WebRootPath, "images", user.imageUrl);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // 3. อัปโหลดไฟล์ใหม่ และอัปเดต imageUrl ใน object 'user'
                    user.imageUrl = UpLoadFile(user);
                }
                // 4. ถ้าไม่มีไฟล์ใหม่ (user.ImageFile == null):
                //    'user.imageUrl' ที่มาจาก hidden input จะถูกใช้ต่อไปอัตโนมัติ

                // 5. อัปเดตข้อมูลในฐานข้อมูล
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // จัดการกรณีข้อมูลถูกแก้ชนกัน (ถ้าจำเป็น)
                if (!_context.Users.Any(e => e.Id == user.Id))
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null || id <= 0) return BadRequest();
            var person = await _context.Users.FirstOrDefaultAsync(c => c.Id == id);
            if (person == null) return NotFound();
            return View(person);
        }
    }
}