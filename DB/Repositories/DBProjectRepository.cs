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

namespace DB.Repositories
{
    public class DBProjectRepository : IProjectRepository
    {
        private readonly MongoCollection<Project> _projectCollection;

        public DBProjectRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase2();
            _projectCollection = database.GetCollection<Project>("projects");
        }
        public Project AddProject(Project project)
        {
            _projectCollection.Insert(project);
            return project; 
        }

        public List<Project> GetAllProject()
        {
            //ToDo delete this bad method
            return _projectCollection.FindAll().ToList();
        }

        public Project GetProjectById(ObjectId id)
        {
            return _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(id));
        }

        public void AddProjectsToProject(ObjectId newProjects, ObjectId iDRootProject)
        {
            
        }

        public void AddImagesToProject(ObjectId newImages, ObjectId iDProject)
        {
            
        }

        public void AddCommentsToProject(ObjectId newComments, ObjectId iDProject)
        {
            
        }
    }
}
