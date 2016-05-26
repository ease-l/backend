﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DB.Models;
using DB.Interfaces;
using System.Web.Mvc;
using MongoDB.Bson;

namespace WebApp.Controllers
{
    public class CommentController : Controller
    {
        private DB.Interfaces.ICommentRepository _commentRepository = new DB.Repositories.DBCommentRepository();
        public JsonResult Index()
        {
            var movies = _commentRepository.GetAllComment();
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(ObjectId id)
        {
            var movies = _commentRepository.GetCommentById(id);
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddComment(String text)
        {
            Comment comment = new Comment();
            comment.Text = text;
            _commentRepository.AddComment(comment);
            var movies = new List<object>();
            movies.Add(new { Result = "OK. Comment add" });
            return Json(movies, JsonRequestBehavior.AllowGet);
        }
    }
}