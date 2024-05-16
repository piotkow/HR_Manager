using HRManager.Data.Repositories.Interfaces;
using HRManager.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Data.Repositories.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly HRManagerDbContext _context;

        public PhotoRepository(HRManagerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> GetPhotoByIdAsync(int photoId)
        {
            return await _context.Photos.FindAsync(photoId);
        }

        public async Task InsertPhotoAsync(Photo photo)
        {
            await _context.Photos.AddAsync(photo);
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            Photo photo = await _context.Photos.FindAsync(photoId);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
            }
        }

        public async Task UpdatePhotoAsync(Photo photo)
        {
            _context.Photos.Update(photo);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
