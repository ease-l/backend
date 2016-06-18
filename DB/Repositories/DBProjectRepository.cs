using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using DB.Interfaces;
using DB.Models;
using MongoDB.Driver;

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
        public Project AddProject(Project project)
        {
            _projectCollection.InsertOne(project);
            return project; 
        }

        public List<Project> GetAllProject()
        {
            //ToDo delet this bad method
            return _projectCollection.AsQueryable().ToList();
        }

        public Project GetProjectById(ObjectId id)
        {
            return _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(id));
        }

        public void AddProjectsToProject(List<ObjectId> newProjects, ObjectId iDRootProject)
        {
            var projects = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(iDRootProject)).Projects;    
            if(projects == null)
            {
                projects = new List<ObjectId>();
            }        
            foreach(ObjectId pId in newProjects)
            {
                projects.Add(pId);                
            }
            var update = Builders<Project>.Update.Set(p => p.Projects, projects);
            //var update = Builders<Project>.Update.AddToSet(p => p.Projects, pId);
            _projectCollection.FindOneAndUpdate(p => p.Id.Equals(iDRootProject), update);
        }

        public void AddImagesToProject(List<ObjectId> newImages, ObjectId iDProject)
        {
            var images = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(iDProject)).Images;
            if (images == null)
            {
                images = new List<ObjectId>();
            }
            foreach (ObjectId pId in newImages)
            {
                images.Add(pId);
            }
            var update = Builders<Project>.Update.Set(p => p.Images, images);
            _projectCollection.FindOneAndUpdate(p => p.Id.Equals(iDProject), update);
        }

        public void AddCommentsToProject(List<ObjectId> newComments, ObjectId iDProject)
        {
            var comments = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(iDProject)).Comments;
            if (comments == null)
            {
                comments = new List<ObjectId>();
            }
            foreach (ObjectId pId in newComments)
            {
                comments.Add(pId);
            }
            var update = Builders<Project>.Update.Set(p => p.Comments, comments);
            _projectCollection.FindOneAndUpdate(p => p.Id.Equals(iDProject), update);
        }
    }
}
