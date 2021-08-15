using Shopgiay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace Shopgiay.Controllers
{
    public class HomeGiayController : Controller
    {
        MydataDataContext data = new MydataDataContext();
        // GET: HomeGiay
        private List<sanphamHome> Giaymoi(int count)
        {

            var query =from Giay in data.Giays
                         join TH in data.Thuonghieus on Giay.IDThuonghieu equals TH.IDThuonghieu
                         orderby Giay.Ngaycapnhap descending
                       select new sanphamHome ()
                         { 
                             IDGiay=Giay.IDGiay,
                             Cover1 = Giay.Cover1,
                             Cover2 = Giay.Cover2,
                             Cover3 = Giay.Cover3,
                             Cover4 = Giay.Cover4,
                             Thuonghieu = TH.TenThuonghieu,
                             Tengiay = Giay.Tengiay,
                             Giaban = Giay.Giaban
                         };
            return query.Take(count).ToList();
        }
        public ActionResult Index()
        {
            var giaymoi = Giaymoi(8);
            return View(giaymoi);
        }
        private List<sanphamHome> Giaytheothuonghieu(int id,int count)
        {
            var query = from Giay in data.Giays
                        join TH in data.Thuonghieus on Giay.IDThuonghieu equals TH.IDThuonghieu    
                        orderby Giay.Ngaycapnhap descending
                        where Giay.IDThuonghieu == id            
                        select new sanphamHome()
                        {
                            Cover1 = Giay.Cover1,
                            Cover2 = Giay.Cover2,
                            Cover3 = Giay.Cover3,
                            Cover4 = Giay.Cover4,
                            Thuonghieu = TH.TenThuonghieu,
                            Tengiay = Giay.Tengiay,
                            Giaban = Giay.Giaban,
                            IDGiay=Giay.IDGiay
                        };
            return query.Take(count).ToList();
        }
        public ActionResult Nike(int ? page)
        {
            int pageSize = 5;
            int PageNum = (page ?? 1);
            var nike = Giaytheothuonghieu(2,10);
            return View(nike.ToPagedList(PageNum,pageSize));
        }
        public ActionResult Adidas(int? page)
        {
            int pageSize = 5;
            int PageNum = (page ?? 1);
            var adidas = Giaytheothuonghieu(1,10);
            return View(adidas.ToPagedList(PageNum, pageSize));
        }
        public ActionResult Vans(int? page)
        {
            int pageSize = 5;
            int PageNum = (page ?? 1);
            var vans = Giaytheothuonghieu(3,10);
            return View(vans.ToPagedList(PageNum, pageSize));
        }
        public ActionResult Converse(int? page)
        {
            int pageSize = 5;
            int PageNum = (page ?? 1);
            var converse = Giaytheothuonghieu(4,10);
            return View(converse.ToPagedList(PageNum, pageSize));
        }
        public ActionResult Details(int id)
        {

            var query = from Giay in data.Giays
                        join TH in data.Thuonghieus on Giay.IDThuonghieu equals TH.IDThuonghieu   
                        where Giay.IDGiay == id 
                        select new sanphamHome()
                        {
                            Cover1 = Giay.Cover1,
                            Cover2 = Giay.Cover2,
                            Cover3 = Giay.Cover3,
                            Cover4 = Giay.Cover4,
                            Thuonghieu = TH.TenThuonghieu,
                            Tengiay = Giay.Tengiay,
                            Giaban = Giay.Giaban,
                            Mota= Giay.Mota,
                            IDGiay=Giay.IDGiay,
                            Soluong=Giay.Soluongton
                            
                        };
            return View(query.Single());
        }
        private List<sanphamHome> Sizegiay(int id)
        {
            var query = from Giay in data.Giays
                        join CT in data.CTGiays on Giay.IDGiay equals CT.IDGiay
                        join S in data.Sizes on CT.IDSize equals S.IDSize
                        where CT.IDGiay == id
                        select new sanphamHome()
                        {
                            Size = S.SizeGiay,
                            IDSize=S.IDSize

                        };
            return query.ToList();
        }
        public ActionResult Size(int id)
        {
            var size = Sizegiay(id);
            return PartialView(size);
        }
       
    }
}