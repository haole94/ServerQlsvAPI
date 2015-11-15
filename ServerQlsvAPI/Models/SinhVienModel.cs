using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerQlsvAPI.Models
{
    public class SinhVienModel
    {
        public string MaSoSinhVien { get; set; }
        public string HoTen { get; set; }
        public string NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public Nullable<double> DiemTichLuy { get; set; }
    }
}