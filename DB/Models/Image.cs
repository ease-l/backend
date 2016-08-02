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
        public Image()
        {
            this.Comments = new List<ObjectId>();
        }
    }
    public partial class ImageWithoutObjectId
    {
        public String Id { get; set; }
        public String Author { get; set; }
        public uint Version { get; set; }
        public String Name { get; set; }
        public DateTime CreationelData { get; set; }
        public List<String> Comments { get; set; }
        public String Url { get; set; }
       }

    public partial class ImageWithoutObjectId
    {
        public static ImageWithoutObjectId ImageToImageWithoutObjectId(Image image)
        {
            ImageWithoutObjectId result = new ImageWithoutObjectId {
            Author = image.Author.ToString(),
            Url = image.Url,
            Version = image.Version,
            Name = image.Name,
            Id = image.Id.ToString(),
            CreationelData = image.CreationelData,
            Comments = new List<string>()};           
            if (image.Comments != null)
            {
                foreach (ObjectId id in image.Comments)
                {
                    result.Comments.Add(id.ToString());
                }
            }
            return result;
        }
        public static List<ImageWithoutObjectId> ImagesToImageWithoutObjectId(List<Image> images)
        {
            List<ImageWithoutObjectId> result = new List<ImageWithoutObjectId>();
            foreach (Image image in images)
            {
                result.Add(ImageToImageWithoutObjectId(image));
            }
            return result;
        }
    }
}

