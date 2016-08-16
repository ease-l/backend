using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ControllersLogic.Interfaces
{
    public interface IImageLogic
    {
        String AddCommentToImage(String simageId, String text, String name, int[] area);
        Task<String> AddImage(String url, String name);
        String DeleteCommentFromImage(String idImage, String idComment);
        String DownloadImage(HttpPostedFileBase uploadImage);
        List<ImageWithoutObjectId> GetAllImage();
        ImageWithoutObjectId GetById(String id);
        ImageWithoutObjectId GetByIdAndVersion(String id, int version);
        List<CommentWithoutObjectId> GetCommentFromImage(String simageId);
        ImageWithoutObjectId UpdateById(String id, String name, String url);
    }
}
