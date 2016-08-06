using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Models;

namespace ControllersLogic.Interfaces
{
    public interface ICommentLogic
    {
        String AddComment(String text, String name);
        List<CommentWithoutObjectId> GetAllComment();
        CommentWithoutObjectId GetById(String id);
        CommentWithoutObjectId UpdateById(String id, String name, String text);
    }
}
