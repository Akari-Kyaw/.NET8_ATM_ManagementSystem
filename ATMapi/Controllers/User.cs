using Asp.Versioning;
using AutoMapper;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.ApplicationConfig;
using Model.DTO;
using Repository.UnitOfWork;

namespace ATMapi.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUnitOfWork unitOfWork, IUserService userService,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {

            try
            {
                var userdata = await _unitOfWork.Users.GetByCondition(x => x.ActiveFlag);
                var user=_mapper.Map<IEnumerable<AddUserDTO>>(userdata);
               

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = user});
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetUsertByID(Guid id)
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

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(string userName, string password)
        {
            try
            {
                var returnData = _userService.UserLogin(userName, password);
                return Ok(new ResponseModel { Message ="LogIn Success", Status = APIStatus.Successful, Data = returnData });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.SystemError });
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(AddUserDTO inputModel)
        {
            try
            {
                await _userService.AddUser(inputModel);
               // AddUserDTO Adduserdto = _mapper.Map < AddUserDTO >(inputModel)

                return Ok(new ResponseModel { Message = "Add Success", Status = APIStatus.Successful });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });
                throw;
            }

        }
        [Authorize]
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

        [Authorize]
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
