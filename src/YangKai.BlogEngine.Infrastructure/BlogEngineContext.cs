using System;
using System.Data.Entity;
using YangKai.BlogEngine.Domain;
using YangKai.BlogEngine.Infrastructure.Mapping.BoardModule;
using YangKai.BlogEngine.Infrastructure.Mapping.Common;
using YangKai.BlogEngine.Infrastructure.Mapping.PostModule;

namespace YangKai.BlogEngine.Infrastructure
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V5_0_Net4)]
    public class BlogEngineContext : DbContext
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
