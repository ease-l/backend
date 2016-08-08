using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
        public ObjectId Author { get; set; }
        public uint Version { get; set; }
        public String Name { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreationelData { get; set; }
    }
}
