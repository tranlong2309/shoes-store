using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopgiay.Controllers
{
    public class NguoidungController : Controller
    { MydataDataContext data = new MydataDataContext();
        // GET: Nguoidung
        [HttpGet]
        public ActionResult Dangky()
        {
            if (Session["TKKH"] == null || Session["TKKH"].ToString() == "")
            {
                return View();
            }
            return RedirectToAction("Index", "Homegiay");
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection,KhachHang kh)
        {
            var hoten = collection["NameND"];
            var email = collection["EmailND"];
            var pass = collection["PassND"];
            var passl = collection["PassLND"];
            var diachi = collection["Diachi"];
            var sdt = collection["Sdt"];
            var passmh = MaHoaMD5.MD5Hash(pass);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["loi1"] = "Name khách hàng không được trống";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["loi2"] = "Email khách hàng không được trống";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["loi3"] = "Phải nhập Password";
            }
            else if (String.IsNullOrEmpty(passl))
            {
                ViewData["loi4"] = "Phải nhập lại Password";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["loi5"] = "Phải nhập  Địa chỉ";
            }
            else if (String.IsNullOrEmpty(sdt))
            {
                ViewData["loi6"] = "Phải nhập Số điện thoại";
            }
            else if (pass != passl)
            {
                ViewData["loi4"] = "Mật khẩu nhập lại không khớp";
            }
            else
            {
                kh.TenKH = hoten;
                kh.Email = email;
                kh.Pass = passmh;
                kh.DiachiKH = diachi;
                kh.SDTKH = sdt;
                data.KhachHangs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Dangky();
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (Session["TKKH"] == null || Session["TKKH"].ToString() == "")
            {
                return View();
            }
            return RedirectToAction("Index", "Homegiay");

        }
        public ActionResult Login(FormCollection collection)
        {
            var email = collection["EmailND"];
            var pass = collection["PassND"];
            var passmh = MaHoaMD5.MD5Hash(pass);
            if (String.IsNullOrEmpty(email))
            {
                ViewData["loi1"] = "Phải nhập email";
            }
            else if(String.IsNullOrEmpty(pass))
            {
                ViewData["loi2"] = "Phải nhập Password";
            }
            else
            {
                KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.Email == email && n.Pass == passmh);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["TKKH"] = kh;
                    Session["TenKH"] = kh.TenKH;
                    return RedirectToAction("Index","Homegiay");
                }
                else
                    ViewBag.Thongbao = "Email hoặc Password không đúng";
            }
            return View();
        }
        public ActionResult logout()
        {
            Session["TKKH"] = "";
            return RedirectToAction("Index", "Homegiay");
        }

    }
}