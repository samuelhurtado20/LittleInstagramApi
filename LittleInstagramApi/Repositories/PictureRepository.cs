using LittleInstagramApi.Context;
using LittleInstagramApi.Interfaces;
using LittleInstagramApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LittleInstagramApi.Repositories
{
    public class PictureRepository : IPictureRepository, IDisposable
    {
        private ApiDbContext _context;
        private bool _disposed = false;

        public PictureRepository(ApiDbContext context)
        {
            this._context = context;
        }

        public void Delete(Guid pictureId)
        {
            try
            {
                PictureEntity entity = _context.Pictures.Find(pictureId);
                _context.Pictures.Remove(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PictureEntity GetById(Guid pictureId)
        {
            try
            {
                return _context.Pictures.Find(pictureId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PictureEntity> GetList()
        {
            try
            {
                return _context.Pictures.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<PictureEntity> GetListByPage(int page, int size)
        {
            try
            {
                return _context.Pictures.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Insert(PictureEntity picture)
        {
            try
            {
                _context.Pictures.Add(picture);
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

        public void Update(PictureEntity picture)
        {
            try
            {
                _context.Entry(picture).State = EntityState.Modified;
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
