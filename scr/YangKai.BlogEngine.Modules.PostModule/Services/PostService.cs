using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AtomLab.Domain;
using YangKai.BlogEngine.Modules.PostModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Repositories;

namespace YangKai.BlogEngine.Modules.PostModule.Services
{
  public  class PostService
    {
        readonly PostRepository _postRepository = InstanceLocator.Current.GetInstance<PostRepository>();

        public void Update(string postUrl, Post newData, bool existThumbnail)
        {
            var originalData = _postRepository.Get(p => p.Url == postUrl);

          originalData.Url = newData.Url;
          originalData.Title = newData.Title;
          originalData.Pages[0].Content = newData.Pages[0].Content;
          originalData.Description = newData.Description;
          originalData.PostStatus = newData.PostStatus;

          originalData.EditAdminId = newData.EditAdminId;
          originalData.EditIp = newData.EditIp;
          originalData.EditAddress = newData.EditAddress;
          originalData.EditDate = newData.EditDate;
          originalData.PubDate = newData.PubDate;
          originalData.GroupId = newData.GroupId;

          //删除Post.Categorys
          originalData.Categorys.Clear();
          //添加Post.Categorys
          newData.Categorys.ToList().ForEach(p => originalData.Categorys.Add(p));

          //删除Post.Tags
       originalData.Tags.ForEach(p=>EventProcessor.DeleteEntity(p));

          //添加Post.Tags
          newData.Tags.ToList().ForEach(p => originalData.Tags.Add(p));

          //编辑Post.Source
          bool existNewSource = newData.Source != null;
          bool existOriginalSource = originalData.Source != null;
          if (existNewSource)
          {
              if (existOriginalSource)
              {
                  originalData.Source.Title = newData.Source.Title;
                  originalData.Source.Url = newData.Source.Url;
              }
              else
              {
                  originalData.Source = new Source
                  {
                      Title = newData.Source.Title,
                      Url = newData.Source.Url
                  };
              }
          }
          else
          {
              if (existOriginalSource)
              {
                  EventProcessor.DeleteEntity(originalData.Source);
              }
          }

          //编辑Post.Thumbnail
          bool existNewThumbnail = newData.Thumbnail != null;
          bool existOriginalThumbnail = originalData.Thumbnail != null;
          if (existNewThumbnail)
          {
              if (existOriginalThumbnail)
              {
                  originalData.Thumbnail.Title = newData.Thumbnail.Title;
                  originalData.Thumbnail.Url = newData.Thumbnail.Url;
              }
              else
              {
                  originalData.Thumbnail = new Thumbnail
                  {
                      Title = newData.Thumbnail.Title,
                      Url = newData.Thumbnail.Url
                  };
              }
          }
          else
          {
              if (existOriginalThumbnail && !existThumbnail) //仅有当原来存在缩略图,并且无新缩略图,并且老图片选中删除,才删除
              {
                  EventProcessor.DeleteEntity(originalData.Thumbnail);
              }
          }

          originalData = FixPost(originalData);
      }

      private Post FixPost(Post data)
      {
          data.Title = data.Title.Trim();
          data.Url = data.Url.Trim().ToLower().Replace(" ", "-");
          return data;
      }
    }
}
