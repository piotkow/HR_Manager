﻿using Azure.Storage.Blobs;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.PhotoDTO;
using Microsoft.AspNetCore.Http;
namespace HRManager.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<BlobClient> UploadPhotoAsync(IFormFile photoFile);
        Task<IEnumerable<FileResponse>> GetPhotosAsync();
        Task<FileResponse> GetPhotoByIdAsync(int photoId);
        Task<Photo> InsertPhotoAsync(PhotoRequest photoReq);
        Task DeletePhotoAsync(int photoId);
        Task UpdatePhotoAsync(int photoId, IFormFile photo);
        Task<bool> DeletePhotoAsync(int photoId, string blobUri, string Filename);
    }
}
