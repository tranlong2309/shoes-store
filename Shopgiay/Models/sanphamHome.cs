using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopgiay.Models
{
    public class sanphamHome
    {
        MydataDataContext data = new MydataDataContext();
        public int IDGiay { set; get; }
        public int IDSize { set; get; }
        public double? Size { set; get; }  
        public string Cover4 { set; get; }
        public string Cover1 { set; get; }
        public string Cover2 { set; get; }
        public string Cover3 { set; get; }
        public string Thuonghieu { set; get; }
        public string Tengiay { set; get; }
        public Decimal? Giaban{ set; get; }
        public string Mota { set; get; }
        public DateTime? Ngaycapnhap { set; get; }
        public int? Soluong { set; get; }
      


    }
}