﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DTO;

namespace BAL.IServices
{
    public interface IUserService 
    {
        //Task UserLogin(UserLoginDTO inputModel);
        Task<string> UserLogin(string userName, string password);

        Task AddUser(AddUserDTO inputModel);
        Task UpdateUser(UpdateUserDTO inputModel);
        Task DeleteUser(DeleteUserDTO inputModel);
    
    
    }
}
