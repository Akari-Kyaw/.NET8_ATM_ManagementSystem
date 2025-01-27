using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAL.Common;
using BAL.IServices;
using Microsoft.Identity.Client;
using Model.DTO;
using Model.Entities;
using Repository.UnitOfWork;


namespace BAL.Services
{
    internal class UserServices : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TokenProvider _tokenProvider;

        public UserServices(IUnitOfWork unitOfWork,TokenProvider tokenProvider,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _tokenProvider = tokenProvider;
            _mapper = mapper;
        }

        public async Task AddUser(AddUserDTO inputModel)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(inputModel.Name))
                {
                    throw new ArgumentException("UserName cannot be null or empty.");
                }

                if (string.IsNullOrWhiteSpace(inputModel.Password))
                {
                    throw new ArgumentException("Password cannot be null or empty.");
                }
                string hashedPassword = "";
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputModel.Password));
                    hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }
                var adduser = new User()
                {
                    Name = inputModel.Name,
                    Password = hashedPassword,
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
                    string hashedPassword = "";
                    using (var sha256 = System.Security.Cryptography.SHA256.Create())
                    {
                        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(inputModel.Password));
                        hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                    }
                    updateuser.Name = inputModel.Name;
                            updateuser.Password = hashedPassword;
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
                var deleteuser = (await _unitOfWork.Users.GetByCondition(u => u.UserId == inputModel.UserId&&u.ActiveFlag == true)).FirstOrDefault();
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
        public async Task<string> UserLogin(string userName, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("UserName and Password cannot be null or empty.");
                }

                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    password = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                var verifyUser = (await _unitOfWork.Users
                    .GetByCondition(x => x.Name == userName && x.Password == password))
                    .FirstOrDefault();

                if (verifyUser == null)
                {
                    throw new UnauthorizedAccessException("Invalid username or password.");
                }

                bool isAdmin = verifyUser.is_admin == false; 

                var role = isAdmin ? "Admin" : "User";

                string token = _tokenProvider.Create(verifyUser, role);

                return token;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during login: {ex.Message}");
                throw;
            }
        }




    }
}
