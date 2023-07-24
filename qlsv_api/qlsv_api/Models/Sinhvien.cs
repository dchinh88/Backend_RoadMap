using System;
using System.Collections.Generic;

namespace qlsv_api.Models;

public partial class Sinhvien
{
    public int Masv { get; set; }

    public string Tensv { get; set; } = null!;

    public DateTime Ngaysinh { get; set; }

    public bool Gioitinh { get; set; }

    public int? Makhoa { get; set; }

    public virtual Khoa? MakhoaNavigation { get; set; }
}
