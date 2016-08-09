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
        /// <summary>
        /// Add new comment to project
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="name">Name</param>
        /// <param name="sprojectId">Project id</param>
        /// <returns>Id new project</returns>
        [HttpPost, Route("api/Project/id{sprojectId}/Comment")]
        public String AddCommentToProject(String text, String name, String sprojectId)
        {
            return _projectLogic.AddCommentToProject(text, name, sprojectId);
        }
        /// <summary>
        /// Add new image to ptoject
        /// </summary>
        /// <param name="url">Image url</param>
        /// <param name="name">Name</param>
        /// <param name="sprojectId">Project id</param>
        /// <returns>Id new image</returns>
        [HttpPost, Route("api/Project/id{sprojectId}/Image")]
        public String AddImageToProject(String url, String name, String sprojectId)
        {
            return _projectLogic.AddImageToProject(url, name, sprojectId);
        }
        /// <summary>
        /// Add new project
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Id new project</returns>
        [HttpPost, Route("api/Project")]
        public String AddProject(String name)
        {
            return _projectLogic.AddProject(name);
        }
        /// <summary>
        /// Add new project into ptoject
        /// </summary>
        /// <param name="sidRoot">Id root project</param>
        /// <param name="name">Name new project</param>
        /// <returns>Id new project</returns>
        [HttpPost, Route("api/Project/id{sidRoot}/project")]
        public String AddProjectToProject(String sidRoot, String name)
        {
            return _projectLogic.AddProjectToProject(sidRoot, name);
        }
        /// <summary>
        /// Delete project by id
        /// </summary>
        /// <param name="id">Deleted project id</param>
        /// <returns>Result</returns>
        [HttpDelete, Route("api/Project/id{id}")]
        public String DeleteById(String id)
        {
            return  _projectLogic.DeleteById(id);
        }
        /// <summary>
        /// GetAllProject
        /// </summary>
        /// <returns>List project</returns>
        [HttpGet, Route("api/Project")]
        public List<ProjectWithoutObjectId> GetAllProject()
        {
            return _projectLogic.GetAllProjects();
        }
        /// <summary>
        /// Delete comment from project
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="commentId">Comment id</param>
        /// <returns>Result</returns>
        [HttpDelete, Route("api/Project/id{projectId}/comment/id{commentId}")]
        public String DeleteCommentFromProject(String projectId, String commentId)
        {
            return _projectLogic.DeleteCommentFromProject(projectId, commentId);
        }
        /// <summary>
        /// Delete image from project
        /// </summary>
        /// <param name="projectId">Project id</param>
        /// <param name="imageId">Image id</param>
        /// <returns>Result</returns>
        [HttpDelete, Route("api/Project/id{projectId}/image/id{imageId}")]
        public String DeleteImageFromProject(String projectId, String imageId)
        {
            return _projectLogic.DeleteImageFromProject(projectId, imageId);
        }
        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Project</returns>
        [HttpGet, Route("api/Project/id{id}")]
        public ProjectWithoutObjectId GetById(String id)
        {
            return _projectLogic.GetById(id);            
        }
        /// <summary>
        /// Get comment from project
        /// </summary>
        /// <param name="sidRoot">Id project</param>
        /// <returns>List id comments</returns>
        [HttpGet, Route("api/Project/id{sidRoot}/Comment")]
        public List<CommentWithoutObjectId> GetCommentsFormProject(String sidRoot)
        {
            return _projectLogic.GetCommentsFormProject(sidRoot);
        }
        /// <summary>
        /// Get image from project
        /// </summary>
        /// <param name="sidRoot">Id project</param>
        /// <returns>List id images</returns>
        [HttpGet, Route("api/Project/id{sidRoot}/Image")]
        public List<ImageWithoutObjectId> GetImagesFormProject(String sidRoot)
        {
            return _projectLogic.GetImagesFormProject(sidRoot);
        }
        /// <summary>
        /// Get project from project
        /// </summary>
        /// <param name="sidRoot">Id root project</param>
        /// <returns>List id projects</returns>
        [HttpGet, Route("api/Project/id{sidRoot}/Project")]
        public List<ProjectWithoutObjectId> GetPtojectsFormProject(String sidRoot)
        {            
            return _projectLogic.GetPtojectsFormProject(sidRoot);            
        }
        /// <summary>
        /// Update project by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">New name</param>
        /// <returns>New project</returns>
        [HttpPut, Route("api/Project/id{id}")]
        public ProjectWithoutObjectId UpdateById(String id, String name)
        {
            return _projectLogic.UpdateById(id, name);            
        }
    }
}