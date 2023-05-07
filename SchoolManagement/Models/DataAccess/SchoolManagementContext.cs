using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.DataAccess
{
    public class SchoolManagementContext : DbContext
    {
        // ============
        // USERS
        // ============

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }


        // ============
        // CATEGORIES
        // ============

        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Subject> Subjects { get; set; }


        // ============
        // HOMEROOM
        // ============

        public DbSet<Homeroom> Homerooms { get; set; }
        public DbSet<Sht> Shts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["database"].ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
