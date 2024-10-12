using Microsoft.AspNetCore.Mvc;
using MVC_EFCore.Models;

namespace MVC_EFCore.Controllers
{
    public class PETController : Controller
    {
        PET2Context _context = new PET2Context();
        public PETController()
        {
            _context = new PET2Context();

        }
        public IActionResult Index() // Sử dụng ViewModels để lấy kết quả là Join từ 2 bảng
        {
            var listPetVM = from pet in _context.Pets
                            join sen in _context.Sens
                            on pet.SenId equals sen.Id
                            select new PetViewModel
                            {
                                Id = pet.Id,
                                Ten = pet.Ten,
                                Loai = pet.Loai,
                                SoChan = pet.SoChan,
                                SenName = sen.Ten,
                                SDTSen =sen.Sdt
                            }; // Cú pháp LinQ Join

            return View(listPetVM);
        }
        public IActionResult Create()
        {
            // Cần lấy ID của Sen để tạo ở Pet
            var listSen = _context.Sens.ToList();
            ViewData["listSen"] = listSen;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Pet pet)
        {
            // Cần lấy ID của Sen để tạo ở Pet
            _context.Pets.Add(pet);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
