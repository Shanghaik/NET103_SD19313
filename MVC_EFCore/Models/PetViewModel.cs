namespace MVC_EFCore.Models
{
    public class PetViewModel // Trong trường hợp này ViewModel được sử dụng để tạo đối tượng cho kết quả Join
    {
        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public int? SoChan { get; set; }
        public string? Loai { get; set; }
        public string? ImgUrl { get; set; }
        public string SenName { get; set; }
        public string SDTSen { get; set; }
    }
}
