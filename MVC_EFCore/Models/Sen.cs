using System;
using System.Collections.Generic;

namespace MVC_EFCore.Models
{
    public partial class Sen
    {
        public Sen()
        {
            Pets = new HashSet<Pet>();
        }

        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public string? Sdt { get; set; }
        public string? DiaChi { get; set; }
        // ICollection (Có thể là List) để thể hiện quan hệ 1 Sen - n Pet
        public virtual ICollection<Pet> Pets { get; set; }
    }
}
