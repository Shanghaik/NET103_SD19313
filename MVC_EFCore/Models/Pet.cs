using System;
using System.Collections.Generic;

namespace MVC_EFCore.Models
{
    public partial class Pet
    {
        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public int? SoChan { get; set; }
        public string? Loai { get; set; }
        public string? ImgUrl { get; set; }
        public int? SenId { get; set; }

        // Sen bên dưới không phải 1 thuộc tính mà là Navigation được sử dụng để kết nối
        // và thể hiện quan hệ giữa các bảng trong DB
        // virtual để hỗ trợ cho 1 số hoạt động loading (lazy loading)
        public virtual Sen? Sen { get; set; }
    }
}
