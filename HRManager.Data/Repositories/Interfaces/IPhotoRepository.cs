using HRManager.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Data.Repositories.Interfaces
{
    public interface IPhotoRepository : IDisposable
    {
        Task<IEnumerable<Photo>> GetPhotosAsync();
        Task<Photo> GetPhotoByIdAsync(int photoId);
        Task InsertPhotoAsync(Photo photo);
        Task DeletePhotoAsync(int photoId);
        Task UpdatePhotoAsync(Photo photo);
        Task SaveAsync();
    }
}
