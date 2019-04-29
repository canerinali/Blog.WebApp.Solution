using BLL;
using Blog.AdminPanel.Filters;
using Blog.AdminPanel.Models;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Blog.AdminPanel.Controllers
{
    public class CommentController : Controller
    {
        private BlogUser blogUser = new BlogUser();
        private PostManager postManager = new PostManager();
        private CommentManager commentManager = new CommentManager();

        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Note note = noteManager.Find(x => x.Id == id);
            Post post = postManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", post.Comments);
        }
        [Auth]
        [HttpPost]
        public ActionResult Edit(int? id, string text)
        {
           
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Comment comment = commentManager.Find(x => x.Id == id);

                if (comment == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Text = text;

                if (commentManager.Update(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            
        }

        [Auth]
        public ActionResult Delete(int? id)
        {
            
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Comment comment = commentManager.Find(x => x.Id == id);

                if (comment == null)
                {
                    return new HttpNotFoundResult();
                }

                if (commentManager.Delete(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { result = false }, JsonRequestBehavior.AllowGet);
            
        }

        [Auth]
        [HttpPost]
        public ActionResult Create(Comment comment, int? noteid)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedName");

            if (ModelState.IsValid)
            {
                if (noteid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Post note = postManager.Find(x => x.Id == noteid);

                if (note == null)
                {
                    return new HttpNotFoundResult();
                }

                comment.Post = note;
                comment.Owner = CurrentSession.User;

                if (commentManager.Insert(comment) > 0)
                {
                    return Json(new { result = true }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { result = false }, JsonRequestBehavior.AllowGet);
        }
    }
}