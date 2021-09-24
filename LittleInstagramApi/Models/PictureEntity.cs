using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LittleInstagramApi.Models
{
    public class PictureEntity
    {
        public Guid PictureEntityId { get; set; }
        public string ImageBase64 { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserEntity User { get; set; }
    }
}
