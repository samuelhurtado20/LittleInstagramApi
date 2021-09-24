using System;
using System.Collections.Generic;

namespace LittleInstagramApi.Models
{
    public class UserEntity
    {
        public Guid UserEntityId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<PictureEntity> Pictures { get; set; }
    }
}
