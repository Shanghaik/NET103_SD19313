using Microsoft.AspNetCore.Mvc;

namespace MVC_EFCore.Controllers
{
    public class SessionController : Controller
    {
        /*
         * Session - phiên làm việc là một cách thức để lưu trữ tạm thời dữ liệu trên
         * web server theo cơ chế timeout. Bộ đếm giờ sẽ đếm ngược thời gian kể từ khi 
         * có request cuối cùng được thực thi. Nếu hết quá trình timeout mà không có thêm
         * request nào thì dữ liệu được lưu trong session sẽ bị xóa. Ngược lại nếu trước
         * thời điểm timeout mà có request thì bộ đếm sẽ được reset
         * Ví dụ: Thầy giáo sẽ cho lớp mỗi bạn 1 điểm nếu không bạn nào phát ra tiếng động
         * trong 10 phút. Nếu hết 10p đó thì + điểm, tuy nhiên sau 420 giây có 1 bạn X lỡ 
         * tay click chuột nên 10 phút này lại reset lại.
         */
        public IActionResult LoginWithSession(string username, string password)
        {
            if (String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password)) return View();
            else if (username.ToLower() == "admin" && password == "123456")
            {
                HttpContext.Session.SetString("message", "Welcome " + username);
                return RedirectToAction("Index", "Home");
            }
            else return Content("Dunk nkap Tkat paj");
        }
    }
}
