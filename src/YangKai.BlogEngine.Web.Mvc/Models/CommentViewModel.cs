using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtomLab.Utility;
using YangKai.BlogEngine.Common;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Web.Mvc.Models
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid PostId { get; set; }
        public string Author { get; set; }
        public string Email { get; set; }
        public string Url { get; set; }
        public string Gravatar { get; set; }
        public string Content { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public string GroupUrl { get; set; }
        public string PostUrl { get; set; }
        public string PostTitle { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public static class CommentViewModelExtension
    {
        public static Comment ToEntity(this CommentViewModel viewModel)
        {
            return new Comment()
                {
                    Author = viewModel.Author,
                    PostId = viewModel.PostId,
                    Email = viewModel.Email,
                    Url = viewModel.Url,
                    Content = viewModel.Content,
                    IsAdmin = viewModel.IsAdmin,
                    IsDeleted = viewModel.IsDeleted,
                };
        }

        public static CommentViewModel ToViewModel(this Comment entity)
        {
            return new CommentViewModel()
            {
                CommentId = entity.CommentId,
                PostId = entity.PostId,
                Author = entity.Author,
                //Email = entity.Email,
                Url = entity.Url,
                Content = entity.Content,
                IsAdmin = entity.IsAdmin,
                IsDeleted = entity.IsDeleted,
                GroupUrl = entity.Post.Group.Url,
                PostUrl = entity.Post.Url,
                PostTitle = entity.Post.Title,
                CreateDate = entity.CreateDate,
                Gravatar = GravatarHelper.GetImageSource(entity.Email, Config.Path.AUTHOR_DEFAULT_AVATAR_PATH)
            };
        }

        public static IList<CommentViewModel> ToViewModels(this IList<Comment> entities)
        {
            var list = new List<CommentViewModel>();
            entities.ToList().ForEach(p =>
                {
                    var viewModel = p.ToViewModel();
                    list.Add(viewModel);
                });
            return list;
        }
    }
}