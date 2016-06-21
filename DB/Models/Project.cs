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
        public List<Project> Projects { get; set; }
        public List<Image> Images { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
