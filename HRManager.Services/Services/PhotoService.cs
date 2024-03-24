using Azure.Storage.Blobs;
using HRManager.Data;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
        public class PhotoService : IPhotoService
        {
            private readonly BlobServiceClient _blobServiceClient;
            private readonly HRManagerDbContext _context;

            public PhotoService(BlobServiceClient blobServiceClient, HRManagerDbContext context)
            {
                _blobServiceClient = blobServiceClient;
                _context = context;
            }


            public async Task<BlobClient> UploadPhotoAsync(IFormFile photoFile)
            {
                var containerInstance = _blobServiceClient.GetBlobContainerClient("avatars");
                var blobInstance = containerInstance.GetBlobClient(photoFile.FileName);

                await blobInstance.UploadAsync(photoFile.OpenReadStream());
                return blobInstance;
            }

            public async Task<bool> DeletePhotoAsync(int photoId, string blobUri, string Filename)
            {

                var containerInstance = _blobServiceClient.GetBlobContainerClient("avatars");
                var blobName = Filename;
                var blobInstance = containerInstance.GetBlobClient(blobName);


                var photo = await _context.Photos.FindAsync(photoId);

                if (photo != null && await blobInstance.ExistsAsync())
                {
                    await blobInstance.DeleteIfExistsAsync();
                    _context.Photos.Remove(photo);
                    return true;
                }
                return false;
            }


        }
}
