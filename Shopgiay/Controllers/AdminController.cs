using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shopgiay.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        MydataDataContext data = new MydataDataContext();


        public ActionResult Index()
        {
            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            return View();
        }
        public ActionResult Thongtinuser()
        {
            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            var thongtin = from BMTT in data.Account1s.Where(m => m.UserID != 1) select BMTT;
            return View(thongtin);
        }
        public ActionResult Dangxuat()
        {
            Session["TKAD"] = "";
            Session["TKTV"] = "";
            return RedirectToAction("Dangnhap", "User");

        }

        public ActionResult Details(int id)
        {

            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            var Details_tin = data.Account1s.Where(m => m.UserID == id).First();
            return View(Details_tin);

        }
        [HttpGet]
        public ActionResult Create()
        {

            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Account1 acc)
        {

            var name = collection["NameNew"];
            var email = collection["EmailNew"];
            var pass1 = collection["PassNew"];
            var pass = MaHoaMD5.MD5Hash(pass1);

            if (String.IsNullOrEmpty(name))
            {
                ViewData["loi1"] = "Name không để trống";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["loi2"] = "nhập email";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["loi3"] = "Nhập mật khẩu";
            }

            else
            {
                acc.HoTen = name;
                acc.Email = email;
                acc.Pass = pass;
                data.Account1s.InsertOnSubmit(acc);
                data.SubmitChanges();
                return RedirectToAction("Thongtinuser");
            }
            return this.Create();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }

            var D_tin = data.Account1s.First(m => m.UserID == id);
            return View(D_tin);
            var tt = data.TTLS.First(m => m.UserID == id);
            return View(tt);

        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Tạo biến D_Tin gán với dối dượng có ID bằng với ID tham số 

            var D_tin = data.Account1s.Where(m => m.UserID == id).First();

            //xóa 


            data.Account1s.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("Thongtinuser");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            var EB_tin = data.Account1s.First(m => m.UserID == id);
            return View(EB_tin);
        }
        [HttpPost]
        public ActionResult Edit(int Id, FormCollection collection)
        {
            var acc = data.Account1s.First(m => m.UserID == Id);
            var name = collection["NameEditTV"];
            var email = collection["Email"];
            var pass1 = collection["PassEditTV"];
            var check1 = collection["Check"];
            //int check;
            //Int32.TryParse(check1,out check);

            var pass = MaHoaMD5.MD5Hash(pass1);
            acc.UserID = Id;

            if (String.IsNullOrEmpty(name))
            {
                ViewData["loi1"] = "Name không để trống";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["loi2"] = "nhập email";
            }
            else if (String.IsNullOrEmpty(pass))
            {
                ViewData["loi3"] = "Nhập mật khẩu";
            }
            else
            {

                acc.HoTen = name;
                acc.Email = email;
                acc.Pass = pass;
                //acc.Ckeckacc = check;
                UpdateModel(acc);
                data.SubmitChanges();
                return RedirectToAction("Thongtinuser");
            }
            return this.Edit(Id);
        }
        //lich su nguoi dung
        public void LoginHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 1;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void LogOutHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 2;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void DMKHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 3;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void InsertHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 4;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void DeleteHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 5;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void DetailHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 6;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void EditHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 7;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void CreateHistory(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            userAction.ActionID = 8;
            userAction.Time = DateTime.Now;
            data.TTLS.InsertOnSubmit(userAction);
            data.SubmitChanges();
        }
        public void DeleteTTLS(int id)
        {
            TTLS userAction = new TTLS();
            userAction.UserID = id;
            data.TTLS.DeleteOnSubmit(userAction);
            data.SubmitChanges();
        }
        public ActionResult HistoryUser()
        {
            if (Session["TKAD"] == null || Session["TKAD"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }

            var query = (from BMTT in data.Account1s
                         join TTLS in data.TTLS on BMTT.UserID equals TTLS.UserID
                         join HD in data.HDs on TTLS.ActionID equals HD.ActionID
                         orderby TTLS.Time
                         select new history
                         {
                             ID = BMTT.UserID,
                             HoTen = BMTT.HoTen,
                             HD = HD.Name,
                             Time = TTLS.Time

                         }).ToList();

            return View(query);
        }
        public void checkFollow(int iduser)
        {
            var acc = data.Account1s.First(m => m.UserID == iduser);

            if (acc.Follow == 1)
            {
                acc.Follow = 2;
            }
            else
            {
                acc.Follow = 1;
            }
            data.SubmitChanges();
        }
        public ActionResult link(int id)
        {
            checkFollow(id);
            return RedirectToAction("Thongtinuser");

        }
    }
}