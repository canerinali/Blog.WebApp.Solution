using BLL.Abstract;
using BLL.Results;
using Blog.Entities;
using Blog.Entities.Messages;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryManager : ManagerBase<Category>
    {
        public override int Delete(Category category)
        {
            PostManager postManager = new PostManager();
            CommentManager commentManager = new CommentManager();

            // Kategori ile ilişkili notların silinmesi gerekiyor.
            foreach (Post post in category.Posts.ToList())
            {

                // Note ile ilişkili comment'lerin silinmesi
                foreach (Comment comment in post.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }

                postManager.Delete(post);
            }

            return base.Delete(category);
        }
        public BusinessLayerResult<Category> InsertCategoryFoto(Category category)
        {
            BusinessLayerResult<Category> res = new BusinessLayerResult<Category>();


            if (string.IsNullOrEmpty(category.CategoryImageFilename) == false)
            {
                res.Result.CategoryImageFilename = category.CategoryImageFilename;
            }

            if (base.Insert(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Kategory Eklenmedi.");
            }

            return res;
        }
        public BusinessLayerResult<Category> UpdateCategoryFoto(Category category)
        {
            BusinessLayerResult<Category> res = new BusinessLayerResult<Category>();
            res.Result = Find(x => x.Id == category.Id);

            if (string.IsNullOrEmpty(category.CategoryImageFilename) == false)
            {
                res.Result.CategoryImageFilename = category.CategoryImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Post Eklenmedi.");
            }

            return res;
        }

    }
}
