using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ImageWithoutObjectId
    {
        public String Id { get; set; }
        public String Author { get; set; }
        public uint Version { get; set; }
        public String Name { get; set; }
        public DateTime CreationelData { get; set; }
        public List<String> Comments { get; set; }
        public String Url { get; set; }
        public static ImageWithoutObjectId ImageToImageWithoutObjectId(Image image)
        {
            ImageWithoutObjectId result = new ImageWithoutObjectId();
            result.Author = image.Author.ToString();
            result.Url = image.Url;
            result.Version = image.Version;
            result.Name = image.Name;
            result.Id = image.Id.ToString();
            result.CreationelData = image.CreationelData;
            result.Comments = new List<string>();
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
    public class Image:BaseEntity
    {
        public List<ObjectId> Comments { get; set; }
        public String Url { get; set; }
        public Image()
        {
            this.Comments = new List<ObjectId>();
        }
    }
}
