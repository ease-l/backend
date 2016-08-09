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
        void DeleteById(ObjectId id);
        void DeleteAll();
        void DeleteCommentFromProject(ObjectId projectId, ObjectId commentId);
        void DeleteProjectFromProject(ObjectId rootProjectId, ObjectId deletedProjectId);
        void DeleteImageFromProject(ObjectId projectId, ObjectId imageId);
        Project AddProject(Project project);
        Project GetProjectById(ObjectId id);
        List<Project> GetAllProject();
        void AddProjectToProject(ObjectId newProjects, ObjectId iDRootProject);
        void AddImageToProject(ObjectId newImages, ObjectId iDProject);
        void AddCommentToProject(ObjectId newComments, ObjectId iDProject);
        List<Project> GetProjectsByIds(List<ObjectId> ids);
        void UpdateProject(ObjectId id, String name, uint version);
    }
}
