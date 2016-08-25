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
            _projectCollection.DeleteOne(p => p.Id.Equals(id));
        }
        public void DeleteAll()
        {
            _projectCollection.DeleteMany(p => true);
        }

        public void DeleteCommentFromProject(ObjectId projectId, ObjectId commentId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(projectId)).Comments;
            list.Remove(commentId);
            var update = Builders<Project>.Update
                .Set("Comments", list)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(projectId), update);
        }
        public void DeleteProjectFromProject(ObjectId rootProjectId, ObjectId deletedProjectId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(rootProjectId)).Projects;
            list.Remove(deletedProjectId);
            var update = Builders<Project>.Update
                .Set("Projects", list)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(rootProjectId), update);
        }
        public void DeleteImageFromProject(ObjectId projectId, ObjectId imageId)
        {
            var list = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(projectId)).Images;
            list.Remove(imageId);
            var update = Builders<Project>.Update
                .Set("Images", list)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(projectId), update);
        }
        public Project AddProject(Project project)
        {
            _projectCollection.InsertOne(project);
            return project;
        }

        public List<Project> GetAllProject()
        {
            //ToDo delete this bad method
            return _projectCollection.AsQueryable().Where(p=>p.Root).ToList();
        }

        public Project GetProjectById(ObjectId id)
        {
            return _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(id));
        }

        public void AddProjectToProject(ObjectId newProject, ObjectId idRootProject)
        {
            var update = Builders<Project>.Update
                .AddToSet("Projects", newProject)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(idRootProject), update);
        }

        public void AddImageToProject(ObjectId newImage, ObjectId idProject)
        {
            var update = Builders<Project>.Update
                .AddToSet("Images", newImage)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(idProject), update);
        }

        public void AddCommentToProject(ObjectId newComment, ObjectId idProject)
        {
            var update = Builders<Project>.Update
                .AddToSet("Comments", newComment)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(idProject), update);
        }

        public List<Project> GetProjectsByIds(List<ObjectId> ids)
        {
            return _projectCollection.AsQueryable().Where(p => ids.Contains(p.Id)).ToList();
        }

        public void UpdateProject(ObjectId id, string name, uint version)
        {
            var update = Builders<Project>.Update
                .Set("Name", name)
                .Set("Version", version)
                .CurrentDate("LastModified");
            var result = _projectCollection.UpdateOne(p => p.Id.Equals(id), update);
        }
    }
}
