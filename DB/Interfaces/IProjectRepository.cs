using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Interfaces
{
    public interface IProjectRepository
    {
        Project AddProject(Project project);
        Project GetProjectById(ObjectId id);
        List<Project> GetAllProject();
        void AddProjectsToProject(List<ObjectId> newProjects, ObjectId iDRootProject);
        void AddImagesToProject(List<ObjectId> newImages, ObjectId iDProject);
        void AddCommentsToProject(List<ObjectId> newComments, ObjectId iDProject);
    }
}
