using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;

namespace BAL.Services
{
    internal class UserServices : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddUser(AddUserDTO inputModel)
        {
            try
            {
                var adduser = new User()
                {
                    Name = inputModel.Name,
                    Password = inputModel.Password,
                    Amount = inputModel.Amount,
                    Created_by = inputModel.Created_by,

                };
                await _unitOfWork.Users.Add(adduser);
                await _unitOfWork.SaveChangesAsync();

            }
            catch(Exception) {
                throw;

            }
        }
        public async Task UpdateUser(UpdateUserDTO inputModel)
        {
            try
            {
                var updateuser = (await _unitOfWork.Users.GetByCondition(u => u.UserId == inputModel.UserId && u.ActiveFlag == true)).FirstOrDefault();
                if (updateuser != null)

                {
                            updateuser.Name = inputModel.Name;
                            updateuser.Password = inputModel.Password;
                            updateuser.Amount = inputModel.Amount;
                            updateuser.Updated_by = inputModel.Updated_by;
                            _unitOfWork.Users.Update(updateuser);
                }
                    else
                    {
                        throw new Exception("User not found.");
                    }
                    await _unitOfWork.SaveChangesAsync();
                }
            
            catch (Exception)
            {
                throw;
            }

        }
        public async Task DeleteUser(DeleteUserDTO inputModel)
        {
            try
            {
                var deleteuser = (await _unitOfWork.Users.GetByCondition(u => u.UserId == inputModel.UserId)).FirstOrDefault();
                if (deleteuser != null)
                {
                    deleteuser.ActiveFlag = false;
                    await _unitOfWork.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("User not found.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
