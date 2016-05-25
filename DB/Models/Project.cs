using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{
    public class Project
    {
        public ObjectId Id { get; set; }
        //ToDo Add list of users with toles
        public List<ImageAndComments> Designs { get; set; }
        public Image Icon { get; set; }
        public String Description { get; set; }
    }
}
