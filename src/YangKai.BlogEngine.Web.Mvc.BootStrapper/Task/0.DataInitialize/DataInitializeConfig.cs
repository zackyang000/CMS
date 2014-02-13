using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Bootstrap.Extensions.StartupTasks;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Infrastructure;
using YangKai.BlogEngine.Infrastructure.Migrations;

namespace YangKai.BlogEngine.Web.Mvc.BootStrapper
{
    public class DataInitializeConfig : IStartupTask
    {
        public void Run()
        {
            //若Model发生改变,则自动更新数据库.
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogEngineContext, Configuration>());

            using (var context = new BlogEngineContext())
            {
                if (!context.Database.Exists())
                {
                    //初始化数据
                    var user = CreateUser();
                    var channel = CreateChannel();
                    var group = CreateGroup(channel);
                    var post = CreatePost(user, group);
                    var comment = CreateComment(post);
                    var board = CreateBoard();

                    //保存数据
                    context.Set<User>().Add(user);
                    context.Set<Channel>().Add(channel);
                    context.Set<Group>().Add(group);
                    context.Set<Post>().Add(post);
                    context.Set<Comment>().Add(comment);
                    context.Set<Board>().Add(board);
                    context.SaveChanges();
                }
            }
        }

        private static User CreateUser()
        {
            return new User()
                {
                    UserId = Guid.NewGuid(),
                    UserName = "Administrator",
                    LoginName = "admin",
                    Password = "123",
                    Email="test@test.com",
                };
        }

        private static Channel CreateChannel()
        {
            return new Channel()
                {
                    ChannelId = Guid.NewGuid(),
                    Name = "channel",
                    Url = "channel",
                    Description = "This is default Channel.",
                    OrderId = 1,
                    IsDefault = true,
                };
        }

        private static Group CreateGroup(Channel channel)
        {
            return new Group()
                {
                    GroupId = Guid.NewGuid(),
                    Name = "group",
                    Url = "group",
                    Description = "This is default Category.",
                    OrderId = 1,
                    Channel = channel,
                };
        }

        private static Post CreatePost(User user, Group group)
        {
            return new Post()
                {
                    PostId = Guid.NewGuid(),
                    Url = "test",
                    Title = "Test article",
                    Content = "<p>This is content.</p>",
                    Description = "<p>This is description.</p>",
                    CreateUser = user.UserName,
                    PubDate = DateTime.Now,
                    Group = group,
                    Tags = new List<Tag> {new Tag {Name = "Test Tag"}},
                };
        }

        private static Comment CreateComment(Post post)
        {
            return new Comment
                {
                    CommentId = Guid.NewGuid(),
                    Content = "This is commit.",
                    Author="guest",
                    Post = post,
                };
        }

        private static Board CreateBoard()
        {
            return new Board
            {
                BoardId = Guid.NewGuid(),
                Content = "This is message.",
                Author = "guest",
            };
        }

        public void Reset()
        {
        }
    }
}
