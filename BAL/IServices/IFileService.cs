using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Model.DTO;


namespace BAL.IServices
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile formFile);
        Task DeleteFile(DeleteFileDTO inputModel);
        Task<FileResponseDTO> Download(string blobFilename);

    }
}
