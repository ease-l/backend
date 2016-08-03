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
        private readonly MongoCollection<Project> _projectCollection;

        public DBProjectRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _projectCollection = database.GetCollection<Project>("projects");
        }
        public void DeleteById(ObjectId id)
        {
            _projectCollection.Remove(Query.EQ("_id", id));
        }
        public void DeleteAll()
        {
            _projectCollection.RemoveAll();
        }

        public void DeleteCommentFromProject(ObjectId projectId, ObjectId commentId)
        {
            var project = _projectCollection.AsQueryable().FirstOrDefault(p=>p.Id.Equals(projectId));
            _projectCollection.Remove(Query.EQ("_id", projectId));
            project.Comments.Remove(commentId);
            _projectCollection.Insert(project);
        }
        public void DeleteImageFromProject(ObjectId projectId, ObjectId imageId)
        {
            var project = _projectCollection.AsQueryable().FirstOrDefault(p => p.Id.Equals(projectId));
            _projectCollection.Remove(Query.EQ("_id", projectId));
            project.Images.Remove(imageId);
            _projectCollection.Insert(project);
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

        public void AddProjectToProject(ObjectId newProject, ObjectId iDRootProject)
        {
            var project = _projectCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(iDRootProject));
            _projectCollection.Remove(Query.EQ("_id", iDRootProject));
            project.Comments.Add(newProject);
            _projectCollection.Insert(project);            
        }

        public void AddImageToProject(ObjectId newImage, ObjectId iDProject)
        {
            var project = _projectCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(iDProject));
            _projectCollection.Remove(Query.EQ("_id", iDProject));
            project.Comments.Add(newImage);
            _projectCollection.Insert(project);           
        }

        public void AddCommentToProject(ObjectId newComment, ObjectId iDProject)
        {
            var project = _projectCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(iDProject));
            _projectCollection.Remove(Query.EQ("_id", iDProject));
            project.Comments.Add(newComment);
            _projectCollection.Insert(project);
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
