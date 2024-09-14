using Microsoft.AspNetCore.Mvc;

namespace Lab12_PH00000.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string username, string password)
        {
            // trường hợp mới mở form, không có gì cả
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password)) return View();
            else
            {
                // Nếu username = admin và password không rỗng => Chuyển hướng về trang chủ
                if (username.ToLower() == "admin" && password.Trim().Length > 0)
                {
                    TempData["message"] = "Bạn đã đăng nhập với tài khoản admin";
                    return RedirectToAction("Index", "Hame"); // chuyển hướng về trang chủ
                }
                else
                {
                    TempData["message"] = "Bạn đã đăng nhập với tài khoản thường";
                    return RedirectToAction("Privacy", "Hame"); // còn lại về trang privacy
                }
            }
            /*
             * TempData được sử dụng để lưu trữ tạm dữ liệu cho đến khi dữ liệu đó được đọc hoặc có 1
             * request khác được đưa ra, vòng đời của dữ liệu nằm trong tempdata là giữa 2 request
             * Tempdata được sử dungjt heo cơ chế key - value
             * TempData đó thể được đọc trực tiếp trong action hoặc trong View với cú pháp @TempData[key]
             */
        }
        public IActionResult SignUp()
        {
            return View();
        }
        // Thực hiện điều hướng với RedirectToAction - điều hướng tới action
        public IActionResult GoHome()
        {
            // return RedirectToAction("Register"); // Khi chỉ điền tên action nó sẽ hiểu là action đó nằm trong cùng controller
            // return RedirectToAction("Index", "Hame"); //=> Chạy OK
            return Redirect("/Hame/Index"); // điều hướng trực tiếp //=> Chạy OK
        }
    }
}
