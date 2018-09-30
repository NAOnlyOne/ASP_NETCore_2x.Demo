using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class CustomDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //设置连接字符串
            string connStr = "User ID=root;Password=123456;Host=localhost;Port=3306;Database=asp_demo";
            optionsBuilder.UseMySql(connStr);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //设置模型关联关系
            modelBuilder.Entity<Blog>()
                .HasOne(e => e.Owner).WithOne(e => e.Blog).HasForeignKey<Blog>(e => e.OwnerId);
            modelBuilder.Entity<Post>()
                .HasOne(e => e.Blog).WithMany(e => e.Posts).HasForeignKey(e => e.BlogId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
