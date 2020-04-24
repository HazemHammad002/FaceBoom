using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Models.Entities;

namespace MiniFacebook.Data
{
    public class ApplicationDbContext : IdentityDbContext<User,Role,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Friend>()
                       .HasKey(f => new { f.UserID, f.FriendID });
            modelBuilder.Entity<PostLike>()
                       .HasKey(p => new { p.UserID, p.PostID });
            modelBuilder.Entity<CommentLike>()
                       .HasKey(c => new { c.CommentID, c.UserID });
            modelBuilder.Entity<User>()
                        .Property(u => u.UserState)
                        .HasDefaultValue(UserState.notblocked);
            modelBuilder.Entity<Post>()
                        .Property(p => p.Feeling)
                        .HasDefaultValue(Feeling.normal);
            modelBuilder.Entity<Post>()
                        .Property(p => p.PostState)
                        .HasDefaultValue(State.visible);
            modelBuilder.Entity<Comment>()
                        .Property(c => c.CommentState)
                        .HasDefaultValue(State.visible);
            

        }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLike> CommentLikes { get; set; }
    }
}
