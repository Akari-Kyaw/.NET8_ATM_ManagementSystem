using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class BlobDTO
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string ContentType { get; set; }


    }
    public class FileResponseDTO
    {
        public string? Name { get; set; }
        public string? ContentType { get; set; }
        public Stream? Content { get; set; }
    }
    public class DeleteFileDTO
    {
        public string Name { get; set; }
    }   
}
