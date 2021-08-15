using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace Shopgiay.Controllers
{
    public class UserQLController : Controller
    {
        MydataDataContext data = new MydataDataContext();
        AdminController admin = new AdminController();
        Account1 acc = new Account1();
        // GET: UserQL
        public ActionResult Index()
        {
            return View();
        }
        private List<dathang>Phieumuahang()
        {

            var query = from DDH in data.Dondathangs
                        join KH in data.KhachHangs on DDH.IDKH equals KH.IDKH
                        join CTHD in data.CTHDs on DDH.IDDondathang equals CTHD.IDDondathang
                        join g in data.Giays on CTHD.IDGiay equals g.IDGiay
                        orderby DDH.Ngaydat descending
                        select new dathang()
                        {
                            TenKH=KH.TenKH,
                            SDTKH=KH.SDTKH,
                            Diachi=KH.DiachiKH,
                            Soluongmua=CTHD.Soluong,
                            Dongia=CTHD.Dongia,
                           Dathanhtoan=DDH.Dathanhtoan,
                           Tinhtranggiaohang=DDH.Tinhtranggiaohang,
                           Ngaydat=DDH.Ngaydat,
                           Ngaygiao=DDH.Ngaygiao,
                           TenGiay=g.Tengiay,
                           Size=CTHD.Size
                        };
            return query.ToList();
        }
        public ActionResult Dondathang(int? page)
        {
            if (Session["TKTV"] == null || Session["TKTV"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            int pageNumber = (page ?? 1);
            int PageSize = 7;
            
            return View(Phieumuahang().ToPagedList(pageNumber, PageSize));
        }
        public ActionResult Giay(int? page)
        {
            if (Session["TKTV"] == null || Session["TKTV"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            int pageNumber = (page ?? 1);
            int PageSize = 7;
            return View(data.Giays.ToList().OrderBy(n => n.IDGiay).ToPagedList(pageNumber, PageSize));
        }
        public ActionResult Dangxuat()
        {
            int IDss;
            Int32.TryParse(Session["ID"].ToString(), out IDss);
            Session["TKAD"] = "";
            Session["TKTV"] = "";
            if (acc.Follow == 1)
            {
                admin.LogOutHistory(IDss);
            }
            return RedirectToAction("Dangnhap", "User");


        }
        [HttpGet]
        public ActionResult DoiMK(int id)
        {
            if (Session["TKTV"] == null || Session["TKTV"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            var EB_tin = data.Account1s.First(m => m.UserID == id);
            return View(EB_tin);
        }
        [HttpPost]
        public ActionResult DoiMK(int Id, FormCollection collection)
        {
            int IDss;
            Int32.TryParse(Session["ID"].ToString(), out IDss);
            var acc = data.Account1s.First(m => m.UserID == Id);
            var password = collection["Password"];
            var passnew = collection["PassNew"];
            var passnewl = collection["PassNewL"];
            var passnew1 = MaHoaMD5.MD5Hash(passnew);
            var password1 = MaHoaMD5.MD5Hash(password);
            var sesspass = Session["Pass"].ToString();
            acc.UserID = Id;
            if (String.IsNullOrEmpty(passnew))
            {
                ViewData["loi2"] = "nhập Pass";
            }
            else if (String.IsNullOrEmpty(passnewl))
            {
                ViewData["loi3"] = "Nhập lại mật khẩu";
            }
            else if (sesspass == password1)
            {
                if (passnew == passnewl)
                {
                    acc.Pass = passnew1;
                    UpdateModel(acc);
                    data.SubmitChanges();
                    if (acc.Follow == 1)
                    {
                        admin.DMKHistory(IDss);
                    }
                    ViewBag.Thongbao1 = "Đổi Password Thành Công";


                }
                else
                    ViewBag.Thongbao1 = "Nhập lại mật khẩu không khớp";

            }
            else
                ViewBag.Thongbao1 = "Nhập mật khẩu cũ không đúng ";

            return this.DoiMK(Id);
        }
        [HttpGet]
        public ActionResult Creategiay()
        {
            ViewBag.IDThuonghieu = new SelectList(data.Thuonghieus.ToList().OrderBy(n =>n.TenThuonghieu), "IDThuonghieu", "TenThuonghieu");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Creategiay(Giay giay,HttpPostedFileBase fileupload, HttpPostedFileBase fileupload2, HttpPostedFileBase fileupload3, HttpPostedFileBase fileupload4)
        {
            int IDss;
            Int32.TryParse(Session["ID"].ToString(), out IDss);
            ViewBag.IDThuonghieu = new SelectList(data.Thuonghieus.ToList().OrderBy(n => n.TenThuonghieu), "IDThuonghieu", "TenThuonghieu");
            if (fileupload == null || fileupload2 == null || fileupload3 == null || fileupload4 == null)
            {
                ViewBag.Thongbao = "vui lòng chọn ảnh ";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var filename2 = Path.GetFileName(fileupload2.FileName);
                    var filename3 = Path.GetFileName(fileupload3.FileName);
                    var filename4 = Path.GetFileName(fileupload4.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img"), filename);
                    var path2 = Path.Combine(Server.MapPath("~/Img"), filename2);
                    var path3 = Path.Combine(Server.MapPath("~/Img"), filename3);
                    var path4 = Path.Combine(Server.MapPath("~/Img"), filename4);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh này đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                        fileupload2.SaveAs(path2);
                        fileupload3.SaveAs(path3);
                        fileupload4.SaveAs(path4);
                    }
                    giay.Cover1 = filename;
                    giay.Cover2 = filename2;
                    giay.Cover3 = filename3;
                    giay.Cover4 = filename4;
                    data.Giays.InsertOnSubmit(giay);
                    data.SubmitChanges();
                    if (acc.Follow == 1)
                    {
                        admin.CreateHistory(IDss);
                    }

                }

                return RedirectToAction("Giay");
            }
       
        }
        
        public ActionResult Delete(int id)
        {
            int IDss;
            Int32.TryParse(Session["ID"].ToString(), out IDss);
            Giay giay = data.Giays.SingleOrDefault(n => n.IDGiay == id);
            ViewBag.IDgiay = giay.IDGiay;
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.Giays.DeleteOnSubmit(giay);
            data.SubmitChanges();
            if (acc.Follow == 1)
            {
                admin.Delete(IDss);
            }
            return RedirectToAction("Giay");
                
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Giay giay = data.Giays.SingleOrDefault(n => n.IDGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;

            }

            ViewBag.IDThuonghieu = new SelectList(data.Thuonghieus.ToList().OrderBy(n => n.TenThuonghieu), "IDThuonghieu", "TenThuonghieu");
            
            return View(giay);
            
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Giay giay, HttpPostedFileBase fileupload, HttpPostedFileBase fileupload2, HttpPostedFileBase fileupload3, HttpPostedFileBase fileupload4)
        {
            ViewBag.IDThuonghieu = new SelectList(data.Thuonghieus.ToList().OrderBy(n => n.TenThuonghieu), "IDThuonghieu", "TenThuonghieu");
            if (fileupload == null || fileupload2 == null || fileupload3 == null || fileupload4 == null)
            {
                ViewBag.Thongbao = "vui lòng chọn ảnh ";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fileupload.FileName);
                    var filename2 = Path.GetFileName(fileupload2.FileName);
                    var filename3 = Path.GetFileName(fileupload3.FileName);
                    var filename4 = Path.GetFileName(fileupload4.FileName);
                    var path = Path.Combine(Server.MapPath("~/Img"), filename);
                    var path2 = Path.Combine(Server.MapPath("~/Img"), filename2);
                    var path3 = Path.Combine(Server.MapPath("~/Img"), filename3);
                    var path4 = Path.Combine(Server.MapPath("~/Img"), filename4);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh này đã tồn tại";
                    }
                    else
                    {
                        fileupload.SaveAs(path);
                        fileupload2.SaveAs(path2);
                        fileupload3.SaveAs(path3);
                        fileupload4.SaveAs(path4);
                    }
                    giay.Cover1 = filename;
                    giay.Cover2 = filename2;
                    giay.Cover3 = filename3;
                    giay.Cover4 = filename4;
                    UpdateModel(giay);
                    data.SubmitChanges();

                }

                return RedirectToAction("Giay");
            }

        }


    }
}