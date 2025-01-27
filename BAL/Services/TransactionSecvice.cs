using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Common;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class TransactionSecvice : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionSecvice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Transactions>> GetAllTransactions()
        {
            try
            {
                var model = await _unitOfWork.Transactions.GetAll();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Transactions> GetTransactionByID(Guid Id)
        {
            try
            {
                var result = (await _unitOfWork.Transactions.GetByCondition(x => x.ID == Id)).FirstOrDefault();
                return result;
            }
             catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<Transactions>> GetTransactionByUserID(Guid UserId)
        {
            try
            {
                var data = await _unitOfWork.Transactions.GetByCondition(x => x.UserId == UserId && x.ActiveFlag);
                if (data == null || !data.Any()) {
                    throw new Exception("Transaction is empty.");      
                }
                return data;

            }
            catch (Exception ex)
            {
                throw ;
            }

        }
        public async Task Deposit(DepositDTO inputModel)
        {
            try
            {
                var user = (await _unitOfWork.Users.GetByCondition(u => u.UserId == inputModel.UserId)).FirstOrDefault();
                if (user is null)
                {
                    return;
                }
                if (inputModel.Amount <= 0)
                {
                    Console.WriteLine("Deposit amount must be greater than zero.");
                    return;
                }

                user.Amount += inputModel.Amount;
                var deposit = new Transactions()
                {
                    UserId = inputModel.UserId,
                    Amount = inputModel.Amount,
                    TransactionType = "Deposit",
                    Created_by = inputModel.Created_by,

                };
                await _unitOfWork.Transactions.Add(deposit);
                await _unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }




        }
        public async Task Withdraw(WithdrawDTO inputModel)
        {
            var user = (await _unitOfWork.Users.GetByCondition(u => u.UserId == inputModel.UserId && u.ActiveFlag == true)).FirstOrDefault();
            if (inputModel.Amount <= 0)
            {
                Console.WriteLine("Withdraw amount must be greater than zero.");
                return;
            }
            if (user.Amount < inputModel.Amount)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            user.Amount -= inputModel.Amount;
            await _unitOfWork.SaveChangesAsync();
            var withdraw = new Transactions()
            {
                UserId = inputModel.UserId,
                Amount = inputModel.Amount,
                TransactionType = "Withdraw",
                Created_by = inputModel.Created_by,

            };
            await _unitOfWork.Transactions.Add(withdraw);
            await _unitOfWork.SaveChangesAsync();

        }

    }
}
