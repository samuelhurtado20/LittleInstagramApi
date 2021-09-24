using LittleInstagramApi.Models;
using System;
using System.Collections.Generic;

namespace LittleInstagramApi.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        IEnumerable<UserEntity> GetList();
        UserEntity GetById(Guid userId);
        void Insert(UserEntity user);
        void Delete(Guid userId);
        void Update(UserEntity user);
        void Save();
        UserEntity GetByEmailAndPassword(string email, string password);
    }
}
