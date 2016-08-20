using DB.Models;
using System;
using System.Collections.Generic;

namespace ControllersLogic.Interfaces
{
    public interface IProjectLogic
    {
        String AddCommentToProject(String text, String name, String sprojectId, String username);
        String AddImageToProject(String url, String name, String sprojectId);
        String AddProject(String name);
        String AddProjectToProject(String sidRoot, String name);
        String DeleteById(String id);
        String DeleteCommentFromProject(String projectId, String commentId);
        String DeleteImageFromProject(String projectId, String imageId);
        List<ProjectWithoutObjectId> GetAllProjects();
        ProjectWithoutObjectId GetById(String id);
        List<CommentWithoutObjectId> GetCommentsFormProject(String sidRoot);
        List<ImageWithoutObjectId> GetImagesFormProject(String sidRoot);
        List<ProjectWithoutObjectId> GetPtojectsFormProject(String sidRoot);
        ProjectWithoutObjectId UpdateById(String id, String name);
    }
}
