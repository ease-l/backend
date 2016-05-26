using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Image
    {
        public List<Comment> Comments { get; set; }
        public byte[] ImageInByte { get; set; }

    }
}
