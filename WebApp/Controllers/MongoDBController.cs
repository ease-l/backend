using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.Models;
using MongoDB.Bson;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class MongoDBController : Controller
    {
        // GET: MongoDB
        private DB.Interfaces.IProjectRepository _projectRepository = new DB.Repositories.DBProjectRepository();
        private DB.Interfaces.IImageRepository _imageRepository = new DB.Repositories.DBImageRepository();
        private DB.Interfaces.ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MakeData()
        {
            /*Make this project
            Game
                Heroes
                    Wizards
                        AttakingWizard.image
                        StandingWizard.image
                        RastingWizard.image
                    Warriors
                        AttakingWarrior.image
                        StandingWarrior.image
                Maps
                    map1.image
                    map2.image
                    map3.image
             */
            var list = new List<ObjectId>();
            var listcom = new List<ObjectId>();
            Project project = new Project();
            project.Name = "Game";
            project.Version = 39;
            project.CreationelData = DateTime.Today;
            var idRootProject = _projectRepository.AddProject(project).Id;
            Comment comment = new Comment();
            comment.Text = "Make icon!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Icon";
            comment.Version = 1;
            list.Add(_commentRepository.AddComment(comment).Id);
            _projectRepository.AddCommentsToProject(list, idRootProject);
            list.Clear();

            project.Id = new ObjectId();
            project.Name = "Heroes";
            project.Version = 26;
            project.CreationelData = DateTime.Today;
            var idHeroes = _projectRepository.AddProject(project).Id;
            list.Add(idHeroes);
            comment.Id = new ObjectId();
            comment.Text = "Make more heroes for example archer!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Archer";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _projectRepository.AddCommentsToProject(listcom, idHeroes);
            listcom.Clear();

            project.Id = new ObjectId();
            project.Name = "Maps";
            project.Version = 14;
            project.CreationelData = DateTime.Today;
            var idMaps = _projectRepository.AddProject(project).Id;
            list.Add(idMaps);
            comment.Id = new ObjectId();
            comment.Text = "Make map for 2 people!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Map for 2";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _projectRepository.AddCommentsToProject(listcom, idMaps);
            listcom.Clear();

            _projectRepository.AddProjectsToProject(list, idRootProject);

            project.Name = "Wizards";
            project.Id = new ObjectId();
            project.Version = 9;
            project.CreationelData = DateTime.Today;
            var idWizards = _projectRepository.AddProject(project).Id;
            comment.Id = new ObjectId();
            comment.Text = "Make more detailed!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Details";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _projectRepository.AddCommentsToProject(listcom, idWizards);
            listcom.Clear();

            project.Id = new ObjectId();
            project.Name = "Warriors";
            project.Version = 17;
            project.CreationelData = DateTime.Today;
            var idWarriors = _projectRepository.AddProject(project).Id;
            comment.Id = new ObjectId();
            comment.Text = "This warriors terrible, make new!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Terrible";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _projectRepository.AddCommentsToProject(listcom, idWarriors);
            listcom.Clear();

            list.Clear();
            list.Add(idWarriors);
            list.Add(idWizards);
            _projectRepository.AddProjectsToProject(list, idHeroes);


            Image image = new Image();
            image.Url = "http://clash-wiki.com/images/army/wizard/wizard_level6_attacks.png";
            image.Version = 4;
            image.Name = "Attacking";
            image.CreationelData = DateTime.UtcNow;
            var idAttackingWizard = _imageRepository.AddImage(image).Id;
            comment.Id = new ObjectId();
            comment.Text = "This Wizards very beautiful!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Beautiful";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _imageRepository.AddCommentToImage(listcom, idAttackingWizard);
            listcom.Clear();

            image.Id = new ObjectId();
            image.Url = "http://clash-wiki.com/images/army/wizard/wizard_level6_attacks.png";
            image.Version = 2;
            image.Name = "Standing";
            image.CreationelData = DateTime.UtcNow;
            var idStandingWizard = _imageRepository.AddImage(image).Id;

            image.Id = new ObjectId();
            image.Url = "http://i0.wp.com/fc09.deviantart.net/fs71/f/2014/324/3/6/_render__clash_of_clans___wizard_by_aaa13xxx-d871py5.png?w=1024&h=1024";
            image.Version = 2;
            image.Name = "Resting";
            image.CreationelData = DateTime.UtcNow;
            var idRastingWizard = _imageRepository.AddImage(image).Id;

            list.Clear();
            list.Add(idAttackingWizard);
            list.Add(idRastingWizard);
            list.Add(idStandingWizard);
            _projectRepository.AddImagesToProject(list, idWizards);

            image.Id = new ObjectId();
            image.Url = "http://vignette1.wikia.nocookie.net/anime-characters-fight/images/5/5b/Warrior_cg_render.png/revision/latest?cb=20140816083714&path-prefix=ru";
            image.Version = 2;
            image.Name = "Standing";
            image.CreationelData = DateTime.UtcNow;
            var idStandingWarrior = _imageRepository.AddImage(image).Id;

            image.Id = new ObjectId();
            image.Url = "http://cdn3.volusion.com/evkq6.5ok79/v/vspfiles/photos/NOV-4980-A1-2.jpg?1407836711";
            image.Version = 2;
            image.Name = "Attacking";
            image.CreationelData = DateTime.UtcNow;
            var idAttackingWarrior = _imageRepository.AddImage(image).Id;

            list.Clear();
            list.Add(idAttackingWarrior);
            list.Add(idStandingWarrior);
            _projectRepository.AddImagesToProject(list, idWarriors);

            image.Id = new ObjectId();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-9589.jpg";
            image.Version = 2;
            image.Name = "For 4+ People";
            image.CreationelData = DateTime.UtcNow;
            var idMap1 = _imageRepository.AddImage(image).Id;
            comment.Id = new ObjectId();
            comment.Text = "Make more water!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Water";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _imageRepository.AddCommentToImage(listcom, idMap1);
            listcom.Clear();

            image.Id = new ObjectId();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-8080.jpg";
            image.Version = 2;
            image.Name = "For 6 People";
            image.CreationelData = DateTime.UtcNow;
            var idMap2 = _imageRepository.AddImage(image).Id;

            image.Id = new ObjectId();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-11731.jpg";
            image.Version = 2;
            image.Name = "For 4 People";
            image.CreationelData = DateTime.UtcNow;
            var idMap3 = _imageRepository.AddImage(image).Id;
            comment.Id = new ObjectId();
            comment.Text = "Make one more island!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Island";
            comment.Version = 1;
            listcom.Add(_commentRepository.AddComment(comment).Id);
            _imageRepository.AddCommentToImage(listcom, idMap3);
            listcom.Clear();

            list.Clear();
            list.Add(idMap1);
            list.Add(idMap2);
            list.Add(idMap3);
            _projectRepository.AddImagesToProject(list, idMaps);
            return View(new MongoDBViewModels
            {
                IdRoot1 = idRootProject.ToString()
            });
        }
    }
}