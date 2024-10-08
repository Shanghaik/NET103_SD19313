﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_EFCore.Models;
using Newtonsoft.Json;

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
        public ActionResult Create() // Form này chỉ để mở ra giao diện cho phép nhập
        {
            Sen dataMau = new Sen()
            {
                Ten = "Quà tặng cột sống",
                DiaChi = "Đốt sống lưng thứ 13",
                Sdt = "1234"
            };
            return View(dataMau);
        }
        // POST: SenController/Create
        [HttpPost] 
        public ActionResult Create(Sen sen) // 
        {
            try
            {
                _context.Sens.Add(sen); _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }
        // Action Result cho phép 1 action trả về Result (JsonResult, ContentResult, Status, ViewResult...)
        // GET: SenController/Edit/5
        public ActionResult Edit(int id)
        {
            var editItem = _context.Sens.Find(id);
            return View(editItem);
        }

        //// POST: SenController/Edit/5
        //[HttpPost]
        //public ActionResult Edit(Sen sen)
        //{
        //    try
        //    {
        //        _context.Update(sen);
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception e)
        //    {
        //        return Content(e.Message);
        //    }
        //}
        [HttpPost]
        public ActionResult Edit(Sen sen, int id)
        {
            var editItem = _context.Sens.FirstOrDefault(x => x.Id == id);
            // var editItem2 = _context.Sens.SingleOrDefault(x => x.Id == id);
            // Cả first và single đều trả về default nếu không tìm thấy dữ liệu
            // Nếu có dữ liệu thì First trả về đối tượng đầu tiên map với điều kiện trong th nhiều
            // Single quang ra exception nếu có nhiều đối tượng map với điều kiện
            try
            {
                editItem.Ten = sen.Ten;
                editItem.Sdt = sen.Sdt;
                editItem.DiaChi = sen.DiaChi;   
                _context.Update(editItem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        // Lý do số 1: Chúng tư dử sụng @model cho View cho nên View
        // nhận diện trực tiếp được model
        // Lý do thứ 2: (Bổ sung cho 1) chúng ta sử dụng EntityFramework core có tính Tracking (theo dõi)
        // nên khi ta thao tác trên 1 model của View thực chất chính là model trong database 
        // GET: SenController/Delete/5
        public ActionResult Delete(int id)
        {
            var deleteItem = _context.Sens.Find(id);
            // Biến dữ liệu cần xóa thành string để đưa vào trong Session
            string jsonData = JsonConvert.SerializeObject(deleteItem);
            // add data đó vào trong Session
            HttpContext.Session.SetString("deleted", jsonData);
            _context.Sens.Remove(deleteItem);   
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RollBack()
        {
            var sessionData = HttpContext.Session.GetString("deleted");
            if (String.IsNullOrEmpty(sessionData))
            {
                return Content("Không có đối tượng nào vừa bị xóa");
            }else
            {
                Sen sen = JsonConvert.DeserializeObject<Sen>(sessionData);  // Tạo ra đối tượng từ dữ liệu
                Sen newSen = new Sen()
                {
                    Ten = sen.Ten,
                    Sdt = sen.Sdt,
                    DiaChi = sen.DiaChi
                };
                // Vì ID của Sen trong db là identity nên đối tượng đã bị xóa vẫn làm tăng id khi thêm nên
                // ta phải tạo ra đối tượng mới, nếu thuộc tính id không phải identity thì ko cần tạo mới
                _context.Sens.Add(newSen);
                _context.SaveChanges();
                HttpContext.Session.Remove("deleted"); // xóa dữ liệu đó đi sau khi đa rollback
                return RedirectToAction("Index");
            }
        }

        
    }
}
