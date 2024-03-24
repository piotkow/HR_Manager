using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
namespace HRManager.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<BlobClient> UploadPhotoAsync(IFormFile photoFile);
        Task<bool> DeletePhotoAsync(int photoId, string blobUri, string Filename);
    }
}
