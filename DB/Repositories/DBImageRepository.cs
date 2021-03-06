﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.Interfaces;
using DB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;

namespace DB.Repositories
{
    public class DBImageRepository : IImageRepository
    {
        private readonly MongoCollection<Image> _imageCollection;

        public DBImageRepository()
        {
            var database = MongoClientFactory.GetMongoDatabase();
            _imageCollection = database.GetCollection<Image>("images");
        }
        public void DeleteAll()
        {
            _imageCollection.RemoveAll();
        }
        public void DeleteById(ObjectId id)
        {
            _imageCollection.Remove(Query.EQ("_id", id));
        }
        public void DeleteCommentFromImage(ObjectId imageId, ObjectId commentId)
        {
            var image = _imageCollection.AsQueryable().FirstOrDefault(i => i.Id.Equals(imageId));
            _imageCollection.Remove(Query.EQ("_id", imageId));
            image.Comments.Remove(commentId);
            _imageCollection.Insert(image);
        }
        public void AddCommentToImage(ObjectId newComments, ObjectId idImage)
        {
            var image = _imageCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(idImage));
            _imageCollection.Remove(Query.EQ("Id", idImage));
            image.Comments.Add(newComments);
            _imageCollection.Insert(image);
        }

        public Image AddImage(Image image)
        {
            _imageCollection.Insert(image);
            return image;
        }

        public List<Image> GetAllImage()
        {
            //ToDo delet this bad method
            return _imageCollection.AsQueryable().ToList();
        }

        public Image GetImageById(ObjectId id)
        {
            return _imageCollection.AsQueryable().FirstOrDefault(im => im.Id.Equals(id));
        }
        public List<Image> GetImagesByIds(List<ObjectId> ids)
        {
            var list = _imageCollection.FindAll().ToList();
            HashSet<ObjectId> id = new HashSet<ObjectId>();
            foreach (ObjectId i in ids)
            {
                id.Add(i);
            }
            var images = new List<Image>();
            foreach (Image i in list)
            {
                if (id.Contains(i.Id))
                {
                    images.Add(i);
                }
            }
            return images;
        }
    }
}
