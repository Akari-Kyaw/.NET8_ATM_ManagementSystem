using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using BAL.IServices;
using Repository.UnitOfWork;
using Model.DTO;
using Model.Entities;
using Microsoft.AspNetCore.Http;
using Azure.Storage;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;



namespace BAL.Services
{
    internal class FileService: IFileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly BlobContainerClient _blobContainerClient;
        public FileService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;

            var accountName = configuration["AppSetting:AzureStorageAccountName"];
            var accountKey = configuration["AppSetting:AzureAccessKey"];

            if (string.IsNullOrEmpty(accountName) || string.IsNullOrEmpty(accountKey))
            {
                throw new ArgumentException("Account name or account key is not set in the configuration.");
            }

            var name = new StorageSharedKeyCredential(accountName,accountKey);
            var blobUri = $"https://{configuration["AppSetting:AzureStorageAccountName"]}.blob.core.windows.net";
            var blobServiceClient = new BlobServiceClient(new Uri(blobUri), name);
            _blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration["AppSetting:AzureContainer"]);
        }


        public async Task<string> UploadFile(IFormFile formFile)
        {
            try
            {
                if (formFile == null ||
                    !(formFile.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                      formFile.ContentType.Equals("image/jpg", StringComparison.OrdinalIgnoreCase)) ||
                    !(Path.GetExtension(formFile.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                      Path.GetExtension(formFile.FileName).Equals(".jpeg", StringComparison.OrdinalIgnoreCase)))
                {
                    return ""; 
                }

                string dateTime = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formFile.FileName);
                string fileExtension = Path.GetExtension(formFile.FileName);

                string newFileName = $"{fileNameWithoutExtension}_{dateTime}{fileExtension}";

                BlobClient client = _blobContainerClient.GetBlobClient(newFileName);


                await using (Stream? data = formFile.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                var file = new Files()
                {
                    Name = newFileName,
                    Uri = client.Uri.AbsoluteUri,
                    ContentType = formFile.ContentType,
                };

                await _unitOfWork.Files.Create(file);
                await _unitOfWork.SaveChangesAsync();

                return client.Uri.AbsoluteUri;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<FileResponseDTO> Download(string blobFilename)
        {
            try
            {
                BlobClient file = _blobContainerClient.GetBlobClient(blobFilename);
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    var content = await file.DownloadContentAsync();

                    string name = blobFilename;
                    string contentType = content.Value.Details.ContentType;

                    return new FileResponseDTO { Name = name, ContentType = contentType, Content = blobContent };
                }

                return null;
            }
            catch (Exception ex) {
                throw;
            }
        }
        public async Task DeleteFile(DeleteFileDTO inputModel)
        {
            try
            {
                var deletefile = (await _unitOfWork.Files.GetByCondition(u => u.Name == inputModel.Name && u.ActiveFlag == true)).FirstOrDefault();
                if (deletefile != null)
                {
                    deletefile.ActiveFlag = false;
                    await _unitOfWork.SaveChangesAsync();
                }

            }
            catch (Exception ex) {
                throw;
            }
            
        }
    }
}
