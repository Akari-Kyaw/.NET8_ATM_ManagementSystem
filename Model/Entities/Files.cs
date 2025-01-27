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
    [Table("Files")]

    public class Files:Common
    {
        [Key]
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ContentType {  get; set; }
    }
}
