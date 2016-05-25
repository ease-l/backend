using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{
    public class Comment
    {
        public ObjectId UserWhoMakeComment { get; set; }
        public String Text { get; set; }
    }
}
