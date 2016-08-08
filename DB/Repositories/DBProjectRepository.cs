using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using DB.Interfaces;
using DB.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace DB.Repositories
{
    public class DBProjectRepository : IProjectRepository
    {
        private readonly IMongoCollection<Project> _projectCollection;

        public DBProjectRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _projectCollection = database.GetCollection<Project>("projects");
        }
        public void DeleteById(ObjectId id)
        {
            _projectCollection.DeleteOne(prop => true);
        }
        public void DeleteAll()
        {
            _projectCollection.DeleteMany(p => true);
        }

        public void DeleteCommentFromProject(ObjectId projectId, ObjectId commentId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(projectId)).Comments;
            list.Remove(commentId);
            var filter = Builders<Project>.Filter.Eq("_Id", projectId);
            var update = Builders<Project>.Update
                .Set("Comments", list)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);
        }
        public void DeleteProjectFromProject(ObjectId rootProjectId, ObjectId deletedProjectId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(rootProjectId)).Projects;
            list.Remove(deletedProjectId);
            var filter = Builders<Project>.Filter.Eq("_Id", rootProjectId);
            var update = Builders<Project>.Update
                .Set("Projects", list)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);
        }
        public void DeleteImageFromProject(ObjectId projectId, ObjectId imageId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(projectId)).Images;
            list.Remove(imageId);
            var filter = Builders<Project>.Filter.Eq("_Id", projectId);
            var update = Builders<Project>.Update
                .Set("Images", list)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);
        }
        public Project AddProject(Project project)
        {
            _projectCollection.InsertOne(project);
            return project;
        }

        public List<Project> GetAllProject()
        {
            //ToDo delete this bad method
            return _projectCollection.AsQueryable().ToList();
        }

        public Project GetProjectById(ObjectId id)
        {
            return _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(id));
        }

        public void AddProjectToProject(ObjectId newProject, ObjectId idRootProject)
        {
            /*var filter = Builders<Project>.Filter.Eq("_Id", idRootProject);
            var update = Builders<Project>.Update
                .AddToSet("Projects", newProject)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);*/
            var project = GetProjectById(idRootProject);
            DeleteById(idRootProject);
            project.Projects.Add(newProject);
            AddProject(project);
        }

        public void AddImageToProject(ObjectId newImage, ObjectId iDProject)
        {
            /*var filter = Builders<Project>.Filter.Eq("_Id", iDProject);
            var update = Builders<Project>.Update
                .AddToSet("Images", newImage)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);*/
            var project = GetProjectById(iDProject);
            DeleteById(iDProject);
            project.Projects.Add(newImage);
            AddProject(project);
        }

        public void AddCommentToProject(ObjectId newComment, ObjectId iDProject)
        {
            /*var filter = Builders<Project>.Filter.Eq("_Id", iDProject);
            var update = Builders<Project>.Update
                .AddToSet("Comments", newComment)
                .CurrentDate("lastModified");
            var result = _projectCollection.UpdateOne(filter, update);*/
            var project = GetProjectById(iDProject);
            DeleteById(iDProject);
            project.Projects.Add(newComment);
            AddProject(project);
        }

        public List<Project> GetProjectsByIds(List<ObjectId> ids)
        {
            return _projectCollection.AsQueryable().Where(p => ids.Contains(p.Id)).ToList();
        }
    }
}
