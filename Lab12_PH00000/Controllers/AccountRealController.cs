using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Lab12_PH00000.Controllers
{
    public class AccountRealController : Controller
    {
        string connectionString = @"Data Source=SHANGHAIK;Initial Catalog=SD19313;Integrated Security=True;TrustServerCertificate=True";
        // Để có thể kết nối với công nghệ ADO.Net chúng ta cần
        // 1. Cài đặt package cần thiết Microsoft.Data.SqlClient
        // 2. Lấy được connectionString
        // 2 tư duy khi thực hiện login
        // a. Viết truy vấn kiểm tra xem username và password có trong db hay không
        // select * from account where username = ... and password = ...
        // b. Lấy ra tất cả các danh sách account sau đó kiểm tra xem với username và password
        // mình nhập có tồn tại trong danh sách đó hay không?
        // Tạo ra để cố gắng thực hiện truy vấn trên kết nối vừa được tạo
        //cmd.ExecuteNonQuery();// Trả về số row bị tác động (Insert, delete, update)
        //cmd.ExecuteScalar();// Trả về row đầu tiên thu được từ kết quả truy vấn
        //cmd.ExecuteReader();// Trả về danh sách kết quả truy vấn
        public IActionResult Login(string username, string password)
        {
            ViewData["message"] = "Hãy nhập thông tin để đăng nhập";
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(password))
            {
                return View();
            }
            else
            {
                // Tạo 1 kết nối thông qua connectionString vừa lấy được
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                // Tạo ra câu truy vấn
                string query = $"select * from account" +
                    $" where username = '{username}' and password = '{password}'";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                try
                {
                    sqlConnection.Open(); // Mở kết nối
                    var row = cmd.ExecuteScalar();
                    if (row != null) // có dữ liệu, login thành công
                    {
                        TempData["message"]="Bạn đã đăng nhập thành công với username " + row.ToString();
                        return RedirectToAction("Index", "Hame"); 
                    }
                    else
                    {
                        ViewData["message"] = "Đăng nhập không thành công, hãy thử lại xem";
                    }
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
                finally // Trọng mọi trường hợp kể cả lỗi thì câu lệnh trong finally luôn luôn được chạy
                {
                    sqlConnection.Close(); // đóng kết nối
                }
            }
            return View();
        }
    }
}
