using System;
using System.Data.Entity;
using YangKai.BlogEngine.Domain;

namespace YangKai.BlogEngine.Infrastructure
{
    [DbModelBuilderVersion(DbModelBuilderVersion.V6_0)]
    public class BlogEngineContext : DbContext
    { 
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<Channel> Channels { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Gallery> Gallery { get; set; }

        public DbSet<Issue> Issue { get; set; }

        public DbSet<Board> Board { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }
    }
}
