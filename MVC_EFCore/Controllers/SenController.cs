using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_EFCore.Models;

namespace MVC_EFCore.Controllers
{
    public class SenController : Controller
    {
        PET2Context _context; // Tạo context
        public SenController()
        {
            _context = new PET2Context(); // Khởi tạo context trong constructor
        }
        // GET: SenController
        // Trước khi Gen View nhớ Build lại Project
        public ActionResult Index() // Gen View GetAll - Dạng Template List model là Sen
        {
            var sen = _context.Sens.ToList(); // Lấy cả List
            return View(sen); // Truyền vào View
        }

        // GET: SenController/Details/5
        public ActionResult Details(int id) // Template Details
        {
            var item = _context.Sens.Find(id);
            return View(item);
        }

        // GET: SenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
