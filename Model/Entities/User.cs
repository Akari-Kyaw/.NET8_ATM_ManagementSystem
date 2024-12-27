using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.ApplicationConfig;

namespace Model.Entities
{
    [Table("User")]

    public class User:Common
    {

        [Key]
        public  Guid UserId { get; set; }
        public string Name { get; set; }
        public string Password {  get; set; }
        public decimal Amount {  get; set; }
    }
}
