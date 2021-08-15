using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopgiay.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        MydataDataContext data = new MydataDataContext();
        Account1 a = new Account1();
        AdminController admin = new AdminController();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //public ActionResult Dangky()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Dangky(FormCollection collection, Account1 acc)
        //{
        //    var name = collection["Name"];
        //    var email = collection["Email"];
        //    var pass1 = collection["Pass"];
        //    var pass = MaHoaMD5.MD5Hash(pass1);
        //    var passl = collection["PassL"];
        //    if (String.IsNullOrEmpty(name))
        //    {
        //        ViewData["loi1"] = "Name không để trống";
        //    }
        //    else if (String.IsNullOrEmpty(email))
        //    {
        //        ViewData["loi2"] = "nhập email";
        //    }
        //    else if (String.IsNullOrEmpty(pass))
        //    {
        //        ViewData["loi3"] = "Nhập mật khẩu";
        //    }
        //    else if (String.IsNullOrEmpty(passl))
        //    {
        //        ViewData["loi3"] = "Nhập lại mật khẩu";
        //    }
        //    else
        //    {
        //        acc.HoTen = name;
        //        acc.Email = email;
        //        acc.Pass = pass;
        //        data.Account1s.InsertOnSubmit(acc);
        //        data.SubmitChanges();
        //        return RedirectToAction("Dangnhap");
        //    }
        //    return this.Dangky();
        //}
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection)
        {
            //gán các giá trị người dùng nhập liệu cho các biến
            var email = collection["Email"];
            var pass = collection["Pass"];
            var pass1 = MaHoaMD5.MD5Hash(pass);
            if (String.IsNullOrEmpty(email))
            {
                ViewData["loi1"] = "Phải nhập email";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["loi2"] = "Phải nhập pass";
            }
            else
            {
                Account1 acc = data.Account1s.SingleOrDefault(n => n.Email == email && n.Pass == pass1);
                if (acc != null)
                {

                    //data.Accounts.SingleOrDefault(m =>  m.Type ==1);
                    if (acc.UserID == 1)
                    {

                        ViewBag.Thongbao = "Xin Chào admin";
                        Session["TKAD"] = acc;
                        Session["HoTen"] = acc.HoTen;
                        Session["Email"] = acc.Email;
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.Thongbao = "Xin chào Thành viên";
                        Session["TKTV"] = acc;
                        Session["ID"] = acc.UserID;
                        Session["HoTen"] = acc.HoTen;
                        Session["Email"] = acc.Email;
                        Session["Pass"] = acc.Pass;
                        if (acc.Follow == 1)
                        {
                            admin.LoginHistory(acc.UserID);
                        }
                        return RedirectToAction("Giay", "UserQL");
                    }

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu sai";
            }
            return View();
        }
    }
}