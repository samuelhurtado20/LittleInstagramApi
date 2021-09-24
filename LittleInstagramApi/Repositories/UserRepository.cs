using LittleInstagramApi.Context;
using LittleInstagramApi.Interfaces;
using LittleInstagramApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleInstagramApi.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private ApiDbContext _context;
        private bool _disposed = false;

        public UserRepository(ApiDbContext context)
        {
            this._context = context;
        }

        public void Delete(Guid userId)
        {
            try
            {
                UserEntity entity = _context.Users.Find(userId);
                _context.Users.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserEntity GetById(Guid id)
        {
            try
            {
                return _context.Users.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserEntity GetByEmailAndPassword(string email, string password)
        {
            try
            {
                return _context.Users.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserEntity> GetList()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(UserEntity user)
        {
            try
            {
                _context.Users.Add(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(UserEntity user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
