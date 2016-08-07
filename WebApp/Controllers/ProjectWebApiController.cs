using System.Web.Http;
using System;
using ControllersLogic.Interfaces;
using ControllersLogic.Logic;
using DB.Models;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class ProjectWebApiController:ApiController 
    {
        private IProjectLogic _projectLogic = new ProjectLogic();

        [HttpPost, Route("api/Project/id{sprojectId}/Comment")]
        public String AddCommentToProject(String text, String name, String sprojectId)
        {
            return _projectLogic.AddCommentToProject(text, name, sprojectId);
        }
        [HttpPost, Route("api/Project/id{sprojectId}/Image")]
        public String AddImageToProject(String url, String name, String sprojectId)
        {
            return _projectLogic.AddImageToProject(url, name, sprojectId);
        }
        [HttpPost, Route("api/Project")]
        public String AddProject(String name)
        {
            return _projectLogic.AddProject(name);
        }
        [HttpPost, Route("api/Project/id{sidRoot}/project")]
        public String AddProjectToProject(String sidRoot, String name)
        {
            return _projectLogic.AddProjectToProject(sidRoot, name);
        }
        [HttpDelete, Route("api/Project/id{id}")]
        public String DeleteById(String id)
        {
            return  _projectLogic.DeleteById(id);
        }
        [HttpDelete, Route("api/Project/id{projectId}/comment/id{commentId}")]
        public String DeleteCommentFromProject(String projectId, String commentId)
        {
            return _projectLogic.DeleteCommentFromProject(projectId, commentId);
        }
        [HttpDelete, Route("api/Project/id{projectId}/image/id{imageId}")]
        public String DeleteImageFromProject(String projectId, String imageId)
        {
            return _projectLogic.DeleteImageFromProject(projectId, imageId);
        }
        [HttpGet, Route("api/Project/id{id}")]
        public ProjectWithoutObjectId GetById(String id)
        {
            return _projectLogic.GetById(id);            
        }
        [HttpGet, Route("api/Project/id{sidRoot}/Comment")]
        public List<CommentWithoutObjectId> GetCommentsFormProject(String sidRoot)
        {
            return _projectLogic.GetCommentsFormProject(sidRoot);
        }
        [HttpGet, Route("api/Project/id{sidRoot}/Image")]
        public List<ImageWithoutObjectId> GetImagesFormProject(String sidRoot)
        {
            return _projectLogic.GetImagesFormProject(sidRoot);
        }
        [HttpGet, Route("api/Project/id{sidRoot}/Project")]
        public List<ProjectWithoutObjectId> GetPtojectsFormProject(String sidRoot)
        {            
            return _projectLogic.GetPtojectsFormProject(sidRoot);            
        }
        [HttpGet, Route("api/Project")]
        public List<ProjectWithoutObjectId> Index()
        {
            return _projectLogic.GetAllProjects();
        }
        [HttpPut, Route("api/Project/id{id}")]
        public ProjectWithoutObjectId UpdateById(String id, String name)
        {
            return _projectLogic.UpdateById(id, name);            
        }
    }
}