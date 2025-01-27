using Asp.Versioning;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.ApplicationConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace ATMapi.Controllers

{
    [Authorize]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FileController : ControllerBase

    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public FileController(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        [HttpGet("GetAllFile")]
        public async Task<IActionResult> GetAllFile()
        {

            try
            {
                var userdata = await _unitOfWork.Files.GetAll();
                var activeFile = userdata.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = activeFile });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var returnData = await _fileService.UploadFile(file);
                if (returnData != null)
                {
                    return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful, Data = returnData });
                }
                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }


        }
        [HttpGet("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            try
            {
                var result = await _fileService.Download(filename);

                if (result != null)
                {
                    return File(result.Content, result.ContentType, result.Name);
                }
                return Ok(new ResponseModel { Message ="Download Success", Status = APIStatus.Error });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }

        }
        [HttpDelete("Delete File")]
        public async Task<IActionResult> DeleteFile(DeleteFileDTO inputModel)
        {

            try
            {
                await _fileService.DeleteFile(inputModel);
                return Ok(new ResponseModel { Message = "Delete Success", Status = APIStatus.Successful });


            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });

            }
        }
    }
}
