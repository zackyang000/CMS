using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using AtomLab.Domain.Infrastructure;
using YangKai.BlogEngine.Infrastructure.ModelConfiguration.Board;
using YangKai.BlogEngine.Infrastructure.ModelConfiguration.Common;
using YangKai.BlogEngine.Infrastructure.ModelConfiguration.Post;
using YangKai.BlogEngine.Modules.BoardModule.Objects;
using YangKai.BlogEngine.Modules.CommonModule.Objects;
using YangKai.BlogEngine.Modules.PostModule.Objects;

namespace YangKai.BlogEngine.Infrastructure
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0_Net4)]
    public class BlogEngineContext : UnitOfWork
    { 
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Board> Board { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        { 
            modelBuilder.Configurations.Add(new BoardConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new ChannelConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new SourceConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new ThumbnailConfiguration());
            modelBuilder.Configurations.Add(new QrCodeConfiguration());
        }
    }
}
