using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Web.Mvc.Models
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public int? Index { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string GroupUrl { get; set; }
        public string PostUrl { get; set; }
        public string PostTitle { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public static class CommentViewModelExtension
    {
        public static Comment ToCommentEntity(this CommentViewModel viewModel)
        {
            return new Comment()
                {
                    Author = viewModel.Author,
                    Content = viewModel.Content,
                };
        }

        public static CommentViewModel ToCommentViewModel(this Comment entity)
        {
            return new CommentViewModel()
            {
                CommentId = entity.CommentId,
                Author = entity.Author,
                Content = entity.Content,
                GroupUrl = entity.Post.Group.Url,
                PostUrl = entity.Post.Url,
                PostTitle = entity.Post.Title,
                CreateDate = entity.CreateDate,
            };
        }

        public static IList<CommentViewModel> ToCommentViewModels(this IList<Comment> entities)
        {
            var list = new List<CommentViewModel>();
            var index = 1;
            entities.ToList().ForEach(p =>
                {
                    var viewModel = p.ToCommentViewModel();
                    viewModel.Index = index++;
                    list.Add(viewModel);
                });
            return list;
        }
    }
}