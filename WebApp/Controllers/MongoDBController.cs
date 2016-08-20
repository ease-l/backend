using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
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
        public void DeleteData()
        {
            _commentRepository.DeleteAll();
            _projectRepository.DeleteAll();
            _imageRepository.DeleteAll();
        }

        public void DeleteThis()
        {
         //   _projectRepository.DeleteImageFromProject(ObjectId(57b5c31303352810786dd888), 57b4eff80335270600ccb03a);
         //   _imageRepository.DeleteById(57b4eff80335270600ccb03a);
        }
        public async Task<ActionResult> MakeData()
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
            Project project = new Project();
            Comment comment = new Comment();
            Image image = new Image();
            image.Url = "http://clash-wiki.com/images/army/wizard/wizard_level6_attacks.png";
            image.Version = 4;
            image.Name = "Attacking";
            image.CreationelData = DateTime.UtcNow;
            comment.Id = new ObjectId();
            comment.Text = "This Wizards very beautiful!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Beautiful";
            comment.Version = 1;
            comment.attachment = new Attachment(10, 20, 30, 40);
            comment = _commentRepository.AddComment(comment);
            image.Comments.Add(comment.Id);
            var idAttackingWizard = _imageRepository.AddImage(image).Id;
            image.StartId = idAttackingWizard.ToString();
            await _imageRepository.DeleteByIdAsync(idAttackingWizard);
            _imageRepository.AddImage(image);

            image.Id = new ObjectId();
            image.Url = "http://clash-wiki.com/images/army/wizard/wizard_level6_attacks.png";
            image.Version = 2;
            image.Name = "Standing";
            image.CreationelData = DateTime.UtcNow;
            image.Comments = new List<ObjectId>();
            var idStandingWizard = _imageRepository.AddImage(image).Id;
            image.StartId = idStandingWizard.ToString();
            await _imageRepository.DeleteByIdAsync(idStandingWizard);
            _imageRepository.AddImage(image);

            image.Id = new ObjectId();
            image.Url = "http://i0.wp.com/fc09.deviantart.net/fs71/f/2014/324/3/6/_render__clash_of_clans___wizard_by_aaa13xxx-d871py5.png?w=1024&h=1024";
            image.Version = 2;
            image.Name = "Resting";
            image.Comments = new List<ObjectId>();
            image.CreationelData = DateTime.UtcNow;
            var idRastingWizard = _imageRepository.AddImage(image).Id;
            image.StartId = idRastingWizard.ToString();
            await _imageRepository.DeleteByIdAsync(idRastingWizard);
            _imageRepository.AddImage(image);

            project.Name = "Wizards";
            project.Id = new ObjectId();
            project.Version = 9;
            project.CreationelData = DateTime.Today;
            comment.Id = new ObjectId();
            comment.Text = "Make more detaileddddddddddd!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Details";
            comment.Version = 1;
            comment.attachment = new Attachment();
            comment = _commentRepository.AddComment(comment);
            project.Comments.Add(comment.Id);
            project.Images.Add(idStandingWizard);
            project.Images.Add(idRastingWizard);
            project.Images.Add(idAttackingWizard);
            var idWizards = _projectRepository.AddProject(project).Id;


            image.Id = new ObjectId();
            image.Url = "http://vignette1.wikia.nocookie.net/anime-characters-fight/images/5/5b/Warrior_cg_render.png/revision/latest?cb=20140816083714&path-prefix=ru";
            image.Version = 2;
            image.Name = "Standing";
            image.CreationelData = DateTime.UtcNow;
            image.Comments = new List<ObjectId>();
            var idStandingWarrior = _imageRepository.AddImage(image).Id;
            image.StartId = idStandingWarrior.ToString();
            await _imageRepository.DeleteByIdAsync(idStandingWarrior);
            _imageRepository.AddImage(image);

            image.Id = new ObjectId();
            image.Url = "http://cdn3.volusion.com/evkq6.5ok79/v/vspfiles/photos/NOV-4980-A1-2.jpg?1407836711";
            image.Version = 2;
            image.Name = "Attacking";
            image.CreationelData = DateTime.UtcNow;
            image.Comments = new List<ObjectId>();
            var idAttackingWarrior = _imageRepository.AddImage(image).Id;
            image.StartId = idAttackingWarrior.ToString();
            await _imageRepository.DeleteByIdAsync(idAttackingWarrior);
            _imageRepository.AddImage(image);

            project = new Project();
            project.Id = new ObjectId();
            project.Name = "Warriors";
            project.Version = 17;
            project.CreationelData = DateTime.Today;
            comment.Id = new ObjectId();
            comment.Text = "This warriors terrible, make new!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Terrible";
            comment.Version = 1;
            comment.attachment = new Attachment(30, 60);
            comment = _commentRepository.AddComment(comment);
            project.Comments.Add(comment.Id);
            project.Images.Add(idAttackingWarrior);
            project.Images.Add(idStandingWizard);
            var idWarriors = _projectRepository.AddProject(project).Id;

            project = new Project();
            project.Id = new ObjectId();
            project.Name = "Heroes";
            project.Version = 26;
            project.CreationelData = DateTime.Today;
            comment.Id = new ObjectId();
            comment.Text = "Make more heroes for example archer!!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Archer";
            comment.Version = 1;
            comment.attachment = new Attachment(100, 20);
            comment = _commentRepository.AddComment(comment);
            project.Comments.Add(comment.Id);
            project.Projects.Add(idWarriors);
            project.Projects.Add(idWizards);
            var idHeroes = _projectRepository.AddProject(project).Id;

            image.Id = new ObjectId();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-9589.jpg";
            image.Version = 2;
            image.Name = "For 4+ People";
            image.Comments = new List<ObjectId>();
            image.CreationelData = DateTime.UtcNow;
            comment.Id = new ObjectId();
            comment.Text = "Make more water!!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Water";
            comment.Version = 1;
            comment.attachment = new Attachment();
            comment = _commentRepository.AddComment(comment);
            image.Comments.Add(comment.Id);
            var idMap1 = _imageRepository.AddImage(image).Id;
            image.StartId = idMap1.ToString();
            await _imageRepository.DeleteByIdAsync(idMap1);
            _imageRepository.AddImage(image);

            image.Id = new ObjectId();
            image.Comments = new List<ObjectId>();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-8080.jpg";
            image.Version = 2;
            image.Name = "For 6 People";
            image.CreationelData = DateTime.UtcNow;
            var idMap2 = _imageRepository.AddImage(image).Id;
            image.StartId = idMap2.ToString();
            await _imageRepository.DeleteByIdAsync(idMap2);
            _imageRepository.AddImage(image);

            image.Id = new ObjectId();
            image.Comments = new List<ObjectId>();
            image.Url = "http://media.fatalgame.com/store/store-heroes3-map-11731.jpg";
            image.Version = 2;
            image.Name = "For 4 People";
            image.CreationelData = DateTime.UtcNow;
            comment.Id = new ObjectId();
            comment.Text = "Make one more island!!!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Island";
            comment.Version = 1;
            comment.attachment = new Attachment( 10, 10, 10, 10);
            comment = _commentRepository.AddComment(comment);
            image.Comments.Add(comment.Id);
            var idMap3 = _imageRepository.AddImage(image).Id;
            image.StartId = idMap3.ToString();
            await _imageRepository.DeleteByIdAsync(idMap3);
            _imageRepository.AddImage(image);

            project = new Project();
            project.Id = new ObjectId();
            project.Name = "Maps";
            project.Version = 14;
            project.CreationelData = DateTime.Today;
            comment.Id = new ObjectId();
            comment.Text = "Make map for 2 people!!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Map for 2";
            comment.Version = 1;
            comment.attachment = new Attachment(20, 20, 20, 20);
            comment = _commentRepository.AddComment(comment);
            project.Comments.Add(comment.Id);
            project.Images.Add(idMap1);
            project.Images.Add(idMap2);
            project.Images.Add(idMap3);
            var idMaps = _projectRepository.AddProject(project).Id;

            project = new Project();
            project.Id = new ObjectId();
            project.Name = "Game";
            project.Version = 39;
            project.CreationelData = DateTime.Today;
            comment.Id = new ObjectId();
            comment.Text = "Make icon!!!!!!!!";
            comment.CreationelData = DateTime.UtcNow;
            comment.Name = "Icon";
            comment.Version = 1;
            comment.attachment = new Attachment();
            comment = _commentRepository.AddComment(comment);
            project.Comments.Add(comment.Id);
            project.Projects.Add(idHeroes);
            project.Projects.Add(idMaps);
            var idRootProject = _projectRepository.AddProject(project).Id;

            project.Id = new ObjectId();
            project.Comments.Clear();
            project.Images.Clear();
            project.Projects.Clear();
            project.Name = "Project with Image with many version";
            project.Version = 1;
            project.CreationelData = DateTime.UtcNow;
            image.Id = new ObjectId();
            image.Comments.Clear();
            image.Name = "Version 1";
            image.Version = 1;
            image.Url = "http://veastrology.com/images/num1.jpg";
            var id = _imageRepository.AddImage(image).Id;
            image.StartId = id.ToString();
            await _imageRepository.DeleteByIdAsync(id);
            _imageRepository.AddImage(image);

            Image prev_image = _imageRepository.GetImageById(id);
            _imageRepository.UpdateImage(id, "Version 2", "http://cliparts.co/cliparts/rTL/o9o/rTLo9o5kc.png", 2);
            prev_image.Id = new ObjectId();
            prev_image.StartId = id.ToString();
            _imageRepository.AddImage(prev_image);

            prev_image = _imageRepository.GetImageById(id);
            _imageRepository.UpdateImage(id, "Version 3", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSdLFPMkUBGXErXUE13IUmX49gSpvXYn3nOdKKkf3zYreak4Wwflg", 3);
            prev_image.Id = new ObjectId();
            prev_image.StartId = id.ToString();
            _imageRepository.AddImage(prev_image);
            project.Images.Add(id);
            _projectRepository.AddProject(project);
            return View(new MongoDBViewModels
            {
                IdRoot = idRootProject.ToString()
            });
        }
    }
}