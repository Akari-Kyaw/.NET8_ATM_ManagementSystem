using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ApplicationConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace ATMapi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public UserController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllProduct()
        {

            try
            {
                var userdata = await _unitOfWork.Users.GetAll();
                var activeUser= userdata.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = activeUser });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetProductByID(Guid id)
        {
            try
            {
                var userdata = await _unitOfWork.Users.GetByCondition(x => x.UserId == id);
                var activeuser = userdata.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = activeuser });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserDTO inputModel)
        {
            try
            {
                await _userService.AddUser(inputModel);
                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
                throw;
            }

        }
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateProduct(UpdateUserDTO inputModel)
        {
            try
            {
                await _userService.UpdateUser(inputModel);
                return Ok(new ResponseModel { Message = "Update Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteProduct(DeleteUserDTO inputModel)
        {
            try
            {
                await _userService.DeleteUser(inputModel);
                return Ok(new ResponseModel { Message = "Delete Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
