using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopgiay.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Goihang
        MydataDataContext data = new MydataDataContext();
        public ActionResult Index()
        {
            return View();
        }
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
       
        public ActionResult ThemGiohang(FormCollection  f,int Idgiay,string strURL)
        {
            var size = double.Parse(f["txtsize"].ToString());
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(n => n.IdGiay == Idgiay );
            if (sanpham == null )
            { 
                sanpham = new Giohang(int.Parse(f["soluong"].ToString()), size, Idgiay);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            if (sanpham.Size == sanpham.Size)
            {
                sanpham = new Giohang(int.Parse(f["soluong"].ToString()), size, Idgiay);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.Soluong++;
                return Redirect(strURL);
            }
        }
        private double TongSoLuong()
        {
            double Tongsoluong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                Tongsoluong = lstGiohang.Sum(n => n.Soluong);
            }
            return Tongsoluong;
        }
        private double TongTien()
        {
            double Tongtien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                Tongtien = lstGiohang.Sum(n => n.Thanhtien);
            }
            return Tongtien;
        }
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "HomeGiay");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
        public ActionResult Xoagiohang(int Idgiay,double size)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Where(n=>n.Size==size).SingleOrDefault(n => n.IdGiay == Idgiay );
            
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.IdGiay == Idgiay && n.Size == size);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Homegiay");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Capnhatgiohang(int Idgiay,FormCollection f, double size)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Where(n => n.Size == size).SingleOrDefault(n => n.IdGiay == Idgiay);
            if (sanpham != null)
            {
                sanpham.Soluong = int.Parse(f["txtSoluong"].ToString());

            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Clearall()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Homegiay");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["TKKH"] == null || Session["TKKH"].ToString() == "")
            {
                return RedirectToAction("Login", "Nguoidung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Homegiay");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult Dathang(FormCollection collection)
        {
            Dondathang ddh = new Dondathang();
            KhachHang kh = (KhachHang) Session["TKKH"];
            List<Giohang> gh = Laygiohang();
            ddh.IDKH = kh.IDKH;
            ddh.Ngaydat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["ngaygiao"]);
            ddh.Ngaygiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohang = false;
            ddh.Dathanhtoan = false;
            data.Dondathangs.InsertOnSubmit(ddh);
            data.SubmitChanges();
               foreach(var item in gh)
            {
                CTHD ctdh = new CTHD();
                ctdh.IDDondathang = ddh.IDDondathang;
                ctdh.IDGiay = item.IdGiay;
                ctdh.Size = item.Size;
                ctdh.Soluong = item.Soluong;
                ctdh.Dongia = (decimal)item.Dongia;
                data.CTHDs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
        
    }
}