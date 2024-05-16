using Azure.Storage.Blobs;
using HRManager.Data;
using HRManager.Data.Repositories.Repositories;
using HRManager.Data.UnitOfWork;
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
            private readonly IUnitOfWork _unitOfWork;

            public PhotoService(BlobServiceClient blobServiceClient, IUnitOfWork unitOfWork)
            {
                _blobServiceClient = blobServiceClient;
                _unitOfWork = unitOfWork;
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


                var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

                if (photo != null && await blobInstance.ExistsAsync())
                {
                    await blobInstance.DeleteIfExistsAsync();
                    _unitOfWork.PhotoRepository.DeletePhotoAsync(photo.PhotoID);
                    return true;
                }
                return false;
            }


        }
}
