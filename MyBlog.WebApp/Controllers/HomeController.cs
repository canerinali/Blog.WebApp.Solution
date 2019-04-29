﻿using BLL;
using BLL.Abstract;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private CategoryManager categoryManager = new CategoryManager();
        private PostManager postManager = new PostManager();
        public ActionResult Index()
        {
            return View(postManager.ListQueryable().Where(x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList());
        }
        public ActionResult ByCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Category cat = categoryManager.Find(x => x.Id == id.Value);

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home");
            //}

            //List<Note> notes = cat.Notes.Where(
            //    x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()

            List<Post> posts = postManager.ListQueryable().Where(
                x => x.IsDraft == false && x.CategoryId == id).OrderByDescending(
                x => x.ModifiedOn).ToList();

            return View("Category", posts);
        }
        public ActionResult DetailPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Category cat = categoryManager.Find(x => x.Id == id.Value);

            //if (cat == null)
            //{
            //    return HttpNotFound();
            //    //return RedirectToAction("Index", "Home");
            //}

            //List<Note> notes = cat.Notes.Where(
            //    x => x.IsDraft == false).OrderByDescending(x => x.ModifiedOn).ToList()

            List<Post> posts = postManager.ListQueryable().Where(
                x => x.IsDraft == false && x.Id == id).ToList();

            return View("DetailPost", posts);
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contant()
        {
            return View();
        }
    }
}