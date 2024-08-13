using AutoMapper;
using Azure.Storage.Blobs;
using HRManager.Data;
using HRManager.Data.Repositories.Repositories;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Services.Services
{
        public class PhotoService : IPhotoService
        {
            private readonly BlobServiceClient _blobServiceClient;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;

        public PhotoService(BlobServiceClient blobServiceClient, IUnitOfWork unitOfWork, IMapper mapper)
            {
                _blobServiceClient = blobServiceClient;
                _unitOfWork = unitOfWork;
                _mapper = mapper;
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
                    //_unitOfWork.PhotoRepository.DeletePhotoAsync(photo.PhotoID);
                    //_unitOfWork.SaveAsync();
                    return true;
                }
                return false;
            }

        public async Task<IEnumerable<FileResponse>> GetPhotosAsync()
        {
            var photos = await _unitOfWork.PhotoRepository.GetPhotosAsync();
            return _mapper.Map<IEnumerable<FileResponse>>(photos);
        }

        public async Task<FileResponse> GetPhotoByIdAsync(int photoId)
        {
            var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);
            return _mapper.Map<FileResponse>(photo);
        }

        public async Task<Photo> InsertPhotoAsync(PhotoRequest photoReq)
        {
            await _unitOfWork.BeginTransactionAsync();
            var photo = _mapper.Map<Photo>(photoReq);
            await _unitOfWork.PhotoRepository.InsertPhotoAsync(photo);
            await _unitOfWork.CommitAsync();
            return photo;
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            await _unitOfWork.PhotoRepository.DeletePhotoAsync(photoId);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePhotoAsync(int photoId, IFormFile photo)
        {
            await _unitOfWork.BeginTransactionAsync();
            var photoToUpdate = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);
            DeletePhotoAsync(photoId, photoToUpdate.Uri, photoToUpdate.Filename);
            var blobInstance = await UploadPhotoAsync(photo);
            photoToUpdate.Uri = blobInstance.Uri.OriginalString;
            photoToUpdate.Filename = blobInstance.Name;
            await _unitOfWork.PhotoRepository.UpdatePhotoAsync(photoToUpdate);
            await _unitOfWork.CommitAsync();
        }
    }
}
