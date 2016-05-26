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
    }
}
