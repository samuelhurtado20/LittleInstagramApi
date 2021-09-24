using LittleInstagramApi.Models;
using System;
using System.Collections.Generic;

namespace LittleInstagramApi.Interfaces
{
    public interface IPictureRepository : IDisposable
    {
        IEnumerable<PictureEntity> GetList();
        PictureEntity GetById(Guid pictureId);
        void Insert(PictureEntity picture);
        void Delete(Guid pictureId);
        void Update(PictureEntity picture);
        void Save();
        IEnumerable<PictureEntity> GetListByPage(int page, int size);
    }
}