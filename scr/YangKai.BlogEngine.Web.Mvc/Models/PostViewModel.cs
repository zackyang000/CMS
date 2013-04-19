using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Web.Mvc.Models
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public int ViewCount { get; set; }
        public int ReplyCount { get; set; }
        public DateTime PubDate { get; set; }

        public string PostStatus { get; set; }
        public string CommentStatus { get; set; }

        public string ChannelName { get; set; }
        public string ChannelUrl { get; set; }
        public string GroupName { get; set; }
        public string GroupUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string QrCodeUrl { get; set; }

        public Dictionary<string, string> Categories { get; set; }
        public IList<string> Tags { get; set; }
    }

    public static class PostViewModelExtension
    {
        public static PostViewModel ToViewModel(this Post entity)
        {
            var viewModel = new PostViewModel()
                {
                    PostId = entity.PostId,
                    Url = entity.Url,
                    Title = entity.Title,
                    Content = entity.Pages.Count>0?entity.Pages[0].Content:"文章丢失.",
                    Description = entity.Description,
                    Author = entity.PubAdmin.UserName,
                    ViewCount = entity.ViewCount,
                    ReplyCount = entity.ReplyCount,
                    PubDate = entity.PubDate,
                    PostStatus = ((PostStatusEnum)entity.PostStatus).ToString(),
                    CommentStatus = ((PostStatusEnum)entity.CommentStatus).ToString(),
                    ChannelName = entity.Group.Channel.Name,
                    ChannelUrl = entity.Group.Channel.Url,
                    GroupName = entity.Group.Name,
                    GroupUrl = entity.Group.Url,
                    ThumbnailUrl = entity.Thumbnail!=null?entity.Thumbnail.Url:null,
                    QrCodeUrl = entity.QrCode != null ? entity.QrCode.Url : null,
                };
            viewModel.Categories = new Dictionary<string, string>();
            entity.Categorys.ForEach(p => viewModel.Categories.Add(p.Url, p.Name));
            viewModel.Tags=new List<string>();
            entity.Tags.ForEach(p => viewModel.Tags.Add(p.Name));

            return viewModel;
        }

        public static IList<PostViewModel> ToViewModels(this IList<Post> entities)
        {
            var list = new List<PostViewModel>();
            entities.ToList().ForEach(p =>
            {
                var viewModel = p.ToViewModel();
                list.Add(viewModel);
            });
            return list;
        }
    }
}