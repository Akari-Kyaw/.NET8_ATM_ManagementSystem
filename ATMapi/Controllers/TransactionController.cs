using System.Transactions;
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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetAllTransaction")]
        public async Task<IActionResult> GetAllTransaction()
        {

            try
            {
                var transactions = await _transactionService.GetAllTransactions();
                var activeTransaction = transactions.Where(p => p.ActiveFlag).ToList();

                return Ok(new ResponseModel { Message = "Successfully", Status = APIStatus.Successful, Data = activeTransaction });
            }

            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }

        [HttpGet("GetTransactionByID")]
        public async Task<IActionResult> GetTransactionByID(Guid id)
        {
            try
            {
                var data = await _transactionService.GetTransactionByID(id);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = data });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }

        [HttpGet("GetTransactionByUserID")]
        public async Task<IActionResult> GetTransactionByUserID(Guid UserId)
        {
            try
            {
                var data = await _transactionService.GetTransactionByUserID(UserId);
                return Ok(new ResponseModel { Message = "Success", Status = APIStatus.Successful, Data = data });

            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }

        }

        [HttpPost("Deposit")]
        public async Task<IActionResult> Deposit(DepositDTO inputModel)
        {
            try
            {
                await _transactionService.Deposit(inputModel);
                return Ok(new ResponseModel { Message = "Deposit Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }
       

        [HttpPost("Withdraw")]
        public async Task<IActionResult> Withdraw(WithdrawDTO inputModel)
        {
            try
            {
                await _transactionService.Withdraw(inputModel);
                return Ok(new ResponseModel { Message = "Withdraw Success", Status = APIStatus.Successful });
            }
            catch (Exception ex)
            {
                return Ok(new ResponseModel { Message = ex.Message, Status = APIStatus.Error });

                throw;
            }
        }

    }
}
