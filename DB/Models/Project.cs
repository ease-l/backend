using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DB.Models
{    
    public class Project:BaseEntity
    {
        public List<ObjectId> Projects { get; set; }
        public List<ObjectId> Images { get; set; }
        public List<ObjectId> Comments { get; set; }
        public Project()
        {
            this.Projects = new List<ObjectId>();
            this.Images = new List<ObjectId>();
            this.Comments = new List<ObjectId>();
        }
    }
    public partial class ProjectWithoutObjectId
    {
        public List<String> Projects { get; set; }
        public List<String> Images { get; set; }
        public List<String> Comments { get; set; }
        public String Id { get; set; }
        public String Author { get; set; }
        public uint Version { get; set; }
        public String Name { get; set; }
        public DateTime CreationelData { get; set; }
    }
    public partial class ProjectWithoutObjectId
    {
        public static List<ProjectWithoutObjectId> ProjectsToProjectWithoutObjectId(List<Project> list)
        {
            List<ProjectWithoutObjectId> result = new List<ProjectWithoutObjectId>();
            foreach (Project p in list)
            {
                result.Add(ProjectToProjectWithoutObjectId(p));
            }
            return result;
        }
        public static ProjectWithoutObjectId ProjectToProjectWithoutObjectId(Project project)
        {
            ProjectWithoutObjectId resultProject = new ProjectWithoutObjectId {
               CreationelData = project.CreationelData,
            Author = project.Author.ToString(),
            Id = project.Id.ToString(),
            Name = project.Name,
            Version = project.Version,
            Comments = new List<string>(),
            Images = new List<string>(),
            Projects = new List<string>()};            
            if (project.Comments != null)
            {
                foreach (ObjectId id in project.Comments)
                {
                    resultProject.Comments.Add(id.ToString());
                }
            }
            if (project.Images != null)
            {
                foreach (ObjectId id in project.Images)
                {
                    resultProject.Images.Add(id.ToString());
                }
            }
            if (project.Projects != null)
            {
                foreach (ObjectId id in project.Projects)
                {
                    resultProject.Projects.Add(id.ToString());
                }
            }
            return resultProject;
        }
    }
}
