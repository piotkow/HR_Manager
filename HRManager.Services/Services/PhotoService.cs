using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using HRManager.Data;
using HRManager.Data.Migrations;
using HRManager.Data.Repositories.Repositories;
using HRManager.Data.UnitOfWork;
using HRManager.Models.Entities;
using HRManager.Services.DTOs.PhotoDTO;
using HRManager.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
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


            public async Task<BlobClient> UploadPhotoAsync(IFormFile photoFile, int employeeId)
            {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(employeeId);
            string containerName = "container-" + employee.FirstName.ToLower() + "-" + employee.LastName.ToLower();
            var containerInstance = _blobServiceClient.GetBlobContainerClient(containerName);

            await containerInstance.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            var blobInstance = containerInstance.GetBlobClient(photoFile.FileName);

            try
            {
                await blobInstance.UploadAsync(photoFile.OpenReadStream());
            }
            catch (Azure.RequestFailedException ex) when (ex.ErrorCode == "BlobAlreadyExists")
            {
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture);
                string newFilename = $"{Path.GetFileNameWithoutExtension(photoFile.FileName)}({timestamp}){Path.GetExtension(photoFile.FileName)}";

                blobInstance = containerInstance.GetBlobClient(newFilename);

                await blobInstance.UploadAsync(photoFile.OpenReadStream());
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return blobInstance;
        }

            public async Task<bool> DeletePhotoAsync(int photoId, string blobUri, string Filename, int employeeId)
            {
            var employee = await _unitOfWork.EmployeeRepository.GetEmployeeByIdAsync(employeeId);
            string containerName = "container-" + employee.FirstName.ToLower() + "-" + employee.LastName.ToLower();
            var containerInstance = _blobServiceClient.GetBlobContainerClient(containerName);
            if (containerInstance.Exists())
            {
                try
                {
                    var blobName = Filename;
                    var blobInstance = containerInstance.GetBlobClient(blobName);
                    var photo = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);
                    bool ifExistInAzure = await blobInstance.ExistsAsync();

                    if (photo != null && ifExistInAzure)
                    {
                        await blobInstance.DeleteIfExistsAsync();
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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

        public async Task UpdatePhotoAsync(int photoId, IFormFile photo, int employeeId)
        {
            await _unitOfWork.BeginTransactionAsync();
            var photoToUpdate = await _unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);
            await DeletePhotoAsync(photoId, photoToUpdate.Uri, photoToUpdate.Filename, employeeId);
            var blobInstance = await UploadPhotoAsync(photo, employeeId);
            photoToUpdate.Uri = blobInstance.Uri.OriginalString;
            photoToUpdate.Filename = blobInstance.Name;
            await _unitOfWork.PhotoRepository.UpdatePhotoAsync(photoToUpdate);
            await _unitOfWork.CommitAsync();
        }
    }
}
