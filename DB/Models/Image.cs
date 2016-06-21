using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Image:BaseEntity
    {
        public List<Comment> Comments { get; set; }
        public String Url { get; set; }

    }
}
