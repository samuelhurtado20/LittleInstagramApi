using System;

namespace LittleInstagramApi.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserLogin(string email)
        {
            this.Email = email;
            this.Password = string.Empty;
        }

        public UserEntity ToUserEntity(UserLogin user)
        {
            UserEntity userEntity = new UserEntity
            {
                UserEntityId = Guid.NewGuid(),
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.Now
            };
            return userEntity;
        }
    }
}
