using BLL;
using Blog.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.WebApp.Controllers
{
    public class CommentController : Controller
    {
        private PostManager postManager = new PostManager();
        private CommentManager commentManager = new CommentManager();
        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.Id == id);

            if (post == null)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialComments", post.Comments);
        }
    }
}