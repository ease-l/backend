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
        public void DeleteAll()
        {
            _projectCollection.RemoveAll();
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

        public void AddProjectToProject(ObjectId newProjects, ObjectId iDRootProject)
        {
            var projects = _projectCollection.FindAll().ToList();
            _projectCollection.RemoveAll();
            foreach (Project p in projects)
            {
                if (p.Id.Equals(iDRootProject))
                {
                    p.Projects.Add(newProjects);
                }
                _projectCollection.Insert(p);
            }
        }

        public void AddImageToProject(ObjectId newImages, ObjectId iDProject)
        {
            var projects = _projectCollection.FindAll().ToList();
            _projectCollection.RemoveAll();
            foreach (Project p in projects)
            {
                if (p.Id.Equals(iDProject))
                {
                    p.Images.Add(newImages);
                }
                _projectCollection.Insert(p);
            }
        }

        public void AddCommentToProject(ObjectId newComments, ObjectId iDProject)
        {
            var projects = _projectCollection.FindAll().ToList();
            _projectCollection.RemoveAll();
            foreach (Project p in projects)
            {
                if (p.Id.Equals(iDProject))
                {
                    p.Comments.Add(newComments);
                }
                _projectCollection.Insert(p);
            }
        }

        public List<Project> GetProjectsByIds(List<ObjectId> ids)
        {
            var list = _projectCollection.FindAll().ToList();
            HashSet<ObjectId> id = new HashSet<ObjectId>();
            foreach (ObjectId i in ids)
            {
                id.Add(i);
            }
            var projects = new List<Project>();
            foreach (Project p in list)
            {
                if (id.Contains(p.Id))
                {
                    projects.Add(p);
                }
            }
            return projects;
        }
    }
}
