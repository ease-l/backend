using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{
    public class Project:BaseEntity
    {
        public List<ObjectId> Projects { get; set; }
        public List<ObjectId> Images { get; set; }
        public List<ObjectId> Comments { get; set; }
    }
}
