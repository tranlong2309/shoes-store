using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopgiay.Models
{
    public class dathang
    {
        MydataDataContext data = new MydataDataContext();
        public string TenKH { set; get; }
        public string SDTKH { set; get; }
        public string Diachi { set; get; }
        public int? Soluongmua { set; get; }
        public Decimal? Dongia { set; get; }
        public Boolean? Tinhtranggiaohang { set; get; }
        public Boolean? Dathanhtoan { set; get; }
        public DateTime? Ngaydat { set; get; }
        public DateTime? Ngaygiao { set; get; }
        public string TenGiay { set; get; }
        public double? Size { set; get; }
    }
}