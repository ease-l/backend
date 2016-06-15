using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Image:BaseEntity
    {
        public List<ObjectId> Comments { get; set; }
        public String Url { get; set; }

    }
}
