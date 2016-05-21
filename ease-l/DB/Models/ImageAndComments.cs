using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ImageAndComments
    {
        public Image Design { get; set; }
        public List<Comment> Comments{ get; set;}
    }
}
