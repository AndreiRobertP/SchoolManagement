using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.EntityLayer;
using System.Configuration;
using System.Linq;
using Microsoft.Data.SqlClient;

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


        // ============
        // SHT DEPENDENT
        // ============

        public DbSet<Grade> Grades { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Mean> Means { get; set; }
        public DbSet<File> Files { get; set; }

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

        // Procedures

        public IEnumerable<Teacher> GetTeachersByUsername(string username)
        {
            var usernameParameter = new SqlParameter("@Username", username);
            var teachers = Teachers.FromSqlRaw("EXEC [dbo].[SelectTeacherByUsername] @Username", usernameParameter);
            return teachers;
        }

        public IEnumerable<Admin> GetAdminsByUsername(string username)
        {
            var usernameParameter = new SqlParameter("@Username", username);
            var admins = Admins.FromSqlRaw("EXEC [dbo].[SelectAdminsByUsername] @Username", usernameParameter);
            return admins;
        }

        public IEnumerable<Homeroom> GetHomeroomsByTeacherUsername(string teacherUsername)
        {
            var teacherUsernameParameter = new SqlParameter("@TeacherUsername", teacherUsername);
            var homerooms = Homerooms.FromSqlRaw("EXEC [dbo].[GetHomeroomByTeacherUsername] @TeacherUsername", teacherUsernameParameter);
            return homerooms;
        }

        public IEnumerable<Student> GetStudentsByUsername(string username)
        {
            var usernameParameter = new SqlParameter("@Username", username);
            var students = Students.FromSqlRaw("EXEC [dbo].[GetStudentsByUsername] @Username", usernameParameter).ToList();
            return students;
        }

        public void InsertTeacher(Teacher teacher)
        {
            var usernameParameter = new SqlParameter("@Username", teacher.Username);
            var nameParameter = new SqlParameter("@Name", teacher.Name);
            var isActiveParameter = new SqlParameter("@IsActive", teacher.IsActive);

            Database.ExecuteSqlRaw("EXEC [dbo].[InsertTeacher] @Username, @Name, @IsActive", usernameParameter, nameParameter, isActiveParameter);
        }

        public void UpdateTeacher(Teacher teacher)
        {
            var teacherIdParameter = new SqlParameter("@TeacherId", teacher.TeacherId);
            var usernameParameter = new SqlParameter("@Username", teacher.Username);
            var nameParameter = new SqlParameter("@Name", teacher.Name);
            var isActiveParameter = new SqlParameter("@IsActive", teacher.IsActive);

            Database.ExecuteSqlRaw("EXEC [dbo].[UpdateTeacher] @TeacherId, @Username, @Name, @IsActive", teacherIdParameter, usernameParameter, nameParameter, isActiveParameter);
        }

        public List<Specialization> GetSpecializations()
        {
            return Specializations
                .FromSqlRaw("EXEC [dbo].[GetSpecializations]")
                .ToList();
        }

        public IEnumerable<Specialization> GetSpecialisationsByName(string name)
        {
            var nameParameter = new SqlParameter("@Name", name);

            return Specializations
                .FromSqlRaw("EXEC [dbo].[GetSpecialisationByName] @Name", nameParameter);
        }

        public void InsertSpecialization(Specialization specialization)
        {
            var nameSpecializationParameter = new SqlParameter("@NameSpecialization", specialization.NameSpecialization);
            var isActiveParameter = new SqlParameter("@IsActive", specialization.IsActive);

            Database.ExecuteSqlRaw("EXEC [dbo].[InsertSpecialization] @NameSpecialization, @IsActive", nameSpecializationParameter, isActiveParameter);
        }

        public void UpdateSpecialization(Specialization specialization)
        {
            var specializationIdParameter = new SqlParameter("@SpecializationId", specialization.SpecializationId);
            var nameSpecializationParameter = new SqlParameter("@NameSpecialization", specialization.NameSpecialization);
            var isActiveParameter = new SqlParameter("@IsActive", specialization.IsActive);

            Database.ExecuteSqlRaw("EXEC [dbo].[UpdateSpecialization] @SpecializationId, @NameSpecialization, @IsActive", specializationIdParameter, nameSpecializationParameter, isActiveParameter);
        }
    }
}
