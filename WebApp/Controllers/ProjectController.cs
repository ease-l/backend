using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MongoDB.Bson;
using DB.Models;
using ControllersLogic.Interfaces;
using ControllersLogic.Logic;
using WebApp.Infrastructure;
using System.Net.Http;
using System.Net;

namespace WebApp.Controllers
{    
    public class ProjectController : Controller
    {
        private IProjectLogic _projectLogic = new ProjectLogic();

        [HttpPost, Route(nameof(Project) + "/{sprojectId}/" + nameof(Comment))]
        public JsonResult AddCommentToProject(String text, String name, String sprojectId)
        {
            return Json(new { Result = _projectLogic.AddCommentToProject(text, name, sprojectId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Project) + "/{sprojectId}/" + nameof(Image))]
        public JsonResult AddImageToProject(String url, String name, String sprojectId)
        {            
            return Json(new { Result = _projectLogic.AddImageToProject(url, name, sprojectId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Project))]
        public JsonResult AddProject(String name)
        {
            return Json(new { Result = _projectLogic.AddProject(name) }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost, Route(nameof(Project) + "/{sidRoot}/" + nameof(Project))]
        public JsonResult AddProjectToProject(String sidRoot, String name)
        {
            return Json(new { Result = _projectLogic.AddProjectToProject(sidRoot, name) }, JsonRequestBehavior.AllowGet);
        }        
        [HttpDelete, Route(nameof(Project) + "/{id}")]
        public JsonResult DeleteById(String id)
        {
            return Json(new { Result = _projectLogic.DeleteById(id) }, JsonRequestBehavior.AllowGet);
        }
        [HttpOptions, Route(nameof(Project) + "/{projectId}/" + nameof(Comment)+"/{commentId}")]
        public HttpResponseMessage OptionsProjectCommentDel(String projectId, String commentId)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpDelete, Route(nameof(Project) + "/{projectId}/" + nameof(Comment) + "/{commentId}")]
        public JsonResult DeleteCommentFromProject(String projectId, String commentId)
        {
            return Json(new { Result = _projectLogic.DeleteCommentFromProject(projectId, commentId)}, JsonRequestBehavior.AllowGet);
        }
        [HttpOptions, Route(nameof(Project) + "/{projectId}/" + nameof(Image) + "/{imageId}")]
        public HttpResponseMessage OptionsProjectImageDel(String projectId, String imageId)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpDelete, Route(nameof(Project) + "/{projectId}/" + nameof(Image) + "/{imageId}")]
        public JsonResult DeleteImageFromProject(String projectId, String imageId)
        {
            return Json(new { Result = _projectLogic.DeleteImageFromProject(projectId, imageId) }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet, Route(nameof(Project) + "/{id}")]
        public JsonResult GetById(String  id)
        {
            try
            {
                return Json(_projectLogic.GetById(id), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpOptions, Route(nameof(Project) + "/{sidRoot}/" + nameof(Comment) )]
        public HttpResponseMessage OptionsProjectComment(String sidRoot)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpGet, Route(nameof(Project) + "/{sidRoot}/" + nameof(Comment))]
        public JsonResult GetCommentsFormProject(String sidRoot)
        {
            try
            {
                return Json(_projectLogic.GetCommentsFormProject(sidRoot), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpOptions, Route(nameof(Project) + "/{sidRoot}/" + nameof(Image))]
        public HttpResponseMessage OptionsProjectImage(String sidRoot)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpGet, Route(nameof(Project) + "/{sidRoot}/" + nameof(Image) )]
        public JsonResult GetImagesFormProject(String sidRoot)
        {
            try
            {
                return Json(_projectLogic.GetImagesFormProject(sidRoot), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpOptions, Route(nameof(Project) + "/{sidRoot}/" + nameof(Project))]
        public HttpResponseMessage OptionsProjectProject(String sidRoot)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpGet, Route(nameof(Project) + "/{sidRoot}/" + nameof(Project))]
        public JsonResult GetPtojectsFormProject(String sidRoot)
        {
            try
            { 
                return Json(_projectLogic.GetPtojectsFormProject(sidRoot), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [ProfileResult]
        [HttpGet, Route(nameof(Project))]
        public JsonResult Index()
        {
           
            return Json(_projectLogic.GetAllProjects(), JsonRequestBehavior.AllowGet);
        }
        [HttpPut, Route(nameof(Project) + "/{id}")]
        public JsonResult UpdateById(String id, String name)
        {
            try
            {
                return Json(_projectLogic.UpdateById(id, name), JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Result = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpOptions, Route(nameof(Project))]
        public HttpResponseMessage OptionsProject()
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        [HttpOptions, Route(nameof(Project) + "/{id}")]
        public HttpResponseMessage OptionsProjectId(String id)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}