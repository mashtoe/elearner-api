﻿using Microsoft.EntityFrameworkCore;
using ELearner.Core.Entity;
using System.Collections.Generic;
using ELearner.Core.Entity.Entities;

namespace Elearner.Infrastructure.Data {
    public class ElearnerAppContext : DbContext {

        private static bool firstInstance = true;

        public ElearnerAppContext(DbContextOptions<ElearnerAppContext> options) : base(options) {
            if (firstInstance) {
                firstInstance = false;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<Section> Sections {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            #region Many-Many User Course
            modelBuilder.Entity<UserCourse>()
                .HasKey(t => new { t.UserID, t.CourseId });

            modelBuilder.Entity<UserCourse>()
                .HasOne(sc => sc.User)
                .WithMany(user => user.Courses)
                .HasForeignKey(sc => sc.UserID);

            modelBuilder.Entity<UserCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(crs => crs.Users)
                .HasForeignKey(sc => sc.CourseId);
            #endregion  
        }
    }
}