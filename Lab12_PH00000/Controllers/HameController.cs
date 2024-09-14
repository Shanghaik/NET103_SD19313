using Lab12_PH00000.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab12_PH00000.Controllers
{
    public class HameController : Controller
    {
        private readonly ILogger<HameController> _logger;

        public HameController(ILogger<HameController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // ViewBag/Viewdata được sử dụng để truyền dữ liệu từ controller sang View nhưng phạm vi sử dụng
            // chỉ nằm trong View ứng với Action
            // Vòng đời của Viewbag/Viewdata cũng chỉ nằm trong 1 Request duy nhất, còn temp data là giữa 2 request
            // ViewBag là dạng dữ liệu dynamic Object
            // ViewData se giống tempData theo cơ chế key-value
            // tương tự như tempdata, ta cũng có thể gọi dữ liệu trực tiếp từ ViewBag Viewdata với @
            ViewBag.message = TempData["message"];
            ViewData["data"] = TempData["message"];
            return View(); // Trả về / hiển thị ra 1 View có cùng tên với Action
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