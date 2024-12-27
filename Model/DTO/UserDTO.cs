using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AddUserDTO
    {
        public string Name {  get; set; }   
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public string Created_by {  get; set; }

    }
    public class UpdateUserDTO
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Amount { get; set; }
        public string Updated_by { get; set; }

    }
    public class DeleteUserDTO
    {
        public Guid UserId { get; set; }

    }
}
