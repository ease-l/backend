using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;
using MongoDB.Bson;

namespace DB.Interfaces
{
    public interface IImageRepository
    {
        Image AddImage(Image image);
        Image GetImageById(ObjectId id);
        List<Image> GetAllImage();
    }
}
