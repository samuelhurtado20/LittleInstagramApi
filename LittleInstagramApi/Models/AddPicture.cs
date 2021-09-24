using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleInstagramApi.Models
{
    public class AddPicture
    {
        public string ImageBase64 { get; set; }
        public string Email { get; set; }
    }
}
