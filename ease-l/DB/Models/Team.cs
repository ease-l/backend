using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{
    public class Team
    {
        public ObjectId Id { get; set; }

        public Image Icon { get; set; }

        public String Description { get; set; }

        public List<ObjectId> UsersId { get; set; }

        public void AddOneUser(ObjectId id)
        {
            UsersId.Add(id);
        }


    }
}
