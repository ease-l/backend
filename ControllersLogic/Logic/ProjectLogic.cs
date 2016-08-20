using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControllersLogic.Interfaces;
using DB.Interfaces;
using DB.Repositories;
using DB.Models;
using MongoDB.Bson;

namespace ControllersLogic.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private IProjectRepository _projectRepository = new DBProjectRepository();
        private IImageRepository _imageRepository = new DBImageRepository();
        private ICommentRepository _commentRepository = new DBCommentRepository();

        public String AddCommentToProject(String text, String name, String sprojectId, String username)
        {
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(sprojectId, out projectId))
            {
                return "Bad id it's not objectId";
            }
            if (_projectRepository.GetProjectById(projectId) == null)
            {
                return "Bad id it's not found in DB" ;
            }
            Comment comment = new Comment();
            comment.Text = text;
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = name;
            comment.Version = 1;
            comment.UserName = username;
            var commentId = _commentRepository.AddComment(comment).Id;
            _projectRepository.AddCommentToProject(commentId, projectId);
            return commentId.ToString();
        }
        public String AddImageToProject(String url, String name, String sprojectId)
        {
            var projectId = new ObjectId();
            if (!ObjectId.TryParse(sprojectId, out projectId))
            {
                return "Bad id it's not objectId";
            }
            if (_projectRepository.GetProjectById(projectId) == null)
            {
                return "Bad id it's not found in DB";
            }
            Image image = new Image();
            image.Url = url;
            image.Version = 1;
            image.Name = name;
            image.CreationelData = DateTime.UtcNow;
            var imageId = _imageRepository.AddImage(image).Id;
            image.StartId = imageId.ToString();
            _imageRepository.DeleteByIdAsync(imageId);
            _imageRepository.AddImage(image);
            _projectRepository.AddImageToProject(imageId, projectId);
            return imageId.ToString();
        }
        public String AddProject(String name)
        {
            Project project = new Project();
            project.Name = name;
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            var id = _projectRepository.AddProject(project).Id;
            return id.ToString();
        }
        public String AddProjectToProject(String sidRoot, String name)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                return "Bad id it's not objectId";
            }
            if (_projectRepository.GetProjectById(idRoot) == null)
            {
                return "Bad id it's not found in DB" ;
            }
            Project project = new Project();
            project.Name = name;
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            var idNew = _projectRepository.AddProject(project).Id;
            _projectRepository.AddProjectToProject(idNew, idRoot);
            return idNew.ToString();
        }
        public String DeleteById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                return "Bad id it's not objectId" ;
            }
            if (objectId == null)
            {
                return "Bad id";
            }
            if (_projectRepository.GetProjectById(objectId) == null)
            {
                return "Bad id";
            }
            _projectRepository.DeleteById(objectId);
            return "OK";
        }
        public String DeleteCommentFromProject(String projectId, String commentId)
        {
            var objectIdProject = new ObjectId();
            var objectIdComment = new ObjectId();
            if (!ObjectId.TryParse(projectId, out objectIdProject))
            {
                return "Bad id project it's not objectId";
            }
            if (!ObjectId.TryParse(commentId, out objectIdComment))
            {
                return "Bad id coomment it's not objectId";
            }
            if (_projectRepository.GetProjectById(objectIdProject) == null)
            {
                return "Bad id";
            }
            _projectRepository.DeleteCommentFromProject(objectIdProject, objectIdComment);
            _commentRepository.DeleteById(objectIdComment);
            return "OK";
        }
        public String DeleteImageFromProject(String projectId, String imageId)
        {
            var objectIdProject = new ObjectId();
            var objectIdImage = new ObjectId();
            if (!ObjectId.TryParse(projectId, out objectIdProject))
            {
                return "Bad id project it's not objectId";
            }
            if (!ObjectId.TryParse(imageId, out objectIdImage))
            {
                return "Bad id image it's not objectId";
            }
            if (_projectRepository.GetProjectById(objectIdProject) == null)
            {
                return "Bad id";
            }
            _projectRepository.DeleteImageFromProject(objectIdProject, objectIdImage);
            _imageRepository.DeleteById(objectIdImage);
            return "OK";
        }
        public List<ProjectWithoutObjectId> GetAllProjects()
        {
            var projects = ProjectWithoutObjectId.ProjectsToProjectWithoutObjectId(_projectRepository.GetAllProject());
            return projects;
        }
        public ProjectWithoutObjectId GetById(String id)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            if (objectId == null)
            {
                throw new Exception("Bad id");
            }
            var project = ProjectWithoutObjectId.ProjectToProjectWithoutObjectId(_projectRepository.GetProjectById(objectId));
            if (project == null)
            {
                throw new Exception("Bad id");
            }
            return project;
        }
        public List<CommentWithoutObjectId> GetCommentsFormProject(String sidRoot)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                throw new Exception("Bad id it's not objectId");
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                throw new Exception("Bad id it's not found in DB");
            }
            var comments = CommentWithoutObjectId.CommentsToCommentWithoutObjectId(_commentRepository.GetCommentsByIds(project.Comments));
            return comments;
        }
        public List<ImageWithoutObjectId> GetImagesFormProject(String sidRoot)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
                throw new Exception("Bad id it's not objectId" );
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                throw new Exception("Bad id it's not found in DB");
            }
            var images = ImageWithoutObjectId.ImagesToImageWithoutObjectId(_imageRepository.GetImagesByIds(project.Images));
            return images;
        }
        public List<ProjectWithoutObjectId> GetPtojectsFormProject(String sidRoot)
        {
            var idRoot = new ObjectId();
            if (!ObjectId.TryParse(sidRoot, out idRoot))
            {
               throw new Exception("Bad id it's not objectId");
            }
            var project = _projectRepository.GetProjectById(idRoot);
            if (project == null)
            {
                throw new Exception("Bad id it's not found in DB");
            }
            var projects = ProjectWithoutObjectId.ProjectsToProjectWithoutObjectId(_projectRepository.GetProjectsByIds(project.Projects));
            return projects;
        }
        public ProjectWithoutObjectId UpdateById(String id, String name)
        {
            var objectId = new ObjectId();
            if (!ObjectId.TryParse(id, out objectId))
            {
                throw new Exception("Bad id it's not objectId");
            }
            if (objectId == null)
            {
                 throw new Exception("Bad id");
            }
            var project = _projectRepository.GetProjectById(objectId);
            if (project == null)
            {
                throw new Exception("Bad id");
            }
            project.Name = name;
            project.Version++;
            _projectRepository.UpdateProject(objectId, name, project.Version);
            return ProjectWithoutObjectId.ProjectToProjectWithoutObjectId(project);
        }
    }
}
