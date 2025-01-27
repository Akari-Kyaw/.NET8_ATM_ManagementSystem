using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DepositDTO
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Created_by {  get; set; }
    }
    public class WithdrawDTO
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public string Created_by { get; set; }
    }
}
