using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shopgiay.Models
{
    public class Giohang
    {
        MydataDataContext data = new MydataDataContext();
        public int IdGiay { set; get; }
        public string Tengiay { set; get; }
        public string Cover{ set; get; }
        public double Size { set; get; }
        public Double Dongia { set; get; }
        public int Soluong { set; get; }
        public Double Thanhtien
        {
            get { return Soluong * Dongia; }
        }
        public Giohang(int sl,double size ,int Idgiay)
        {
            IdGiay = Idgiay;
            Giay giay = data.Giays.Single(n => n.IDGiay == IdGiay);
            Tengiay = giay.Tengiay;
            Cover = giay.Cover1;
            Dongia = double.Parse(giay.Giaban.ToString());
            Soluong = sl;
            Size = size;
        }
    }
}