namespace qlsv_api.DTO
{
    public class SinhvienDTO
    {
        public int Masv { get; set; }

        public string Tensv { get; set; } = null!;

        public DateTime Ngaysinh { get; set; }

        public bool Gioitinh { get; set; }

        public int? Makhoa { get; set; }
    }
}
