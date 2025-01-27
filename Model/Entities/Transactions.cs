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
    [Table("Transactions")]

    public class Transactions :Common
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
