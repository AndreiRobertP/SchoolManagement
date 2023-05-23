using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace SchoolManagement.Models.BusinessLogic
{
    public class UserLoginResponse
    {
        public bool IsAdmin => Admin != null;
        public bool IsTeacher => Teacher != null;
        public bool IsHomeroomTeacher => Homeroom != null;
        public bool IsStudent => Student != null;
        public bool IsRegistered => IsAdmin || IsTeacher || IsStudent;

        public Admin? Admin { get; set; } = null;
        public Teacher? Teacher { get; set; } = null;
        public Homeroom? Homeroom { get; set; } = null;
        public Student? Student { get; set; } = null;
    }

    public class UserBLL
    {
        public UserLoginResponse GetUserTypeLogin(string username)
        {

            using (var context = new SchoolManagementContext())
            {
                var adminResponse = context.GetAdminsByUsername(username).ToArray<Admin>();

                var teacherResponse = context.GetTeachersByUsername(username).ToArray<Teacher>();

                Homeroom[] homeroomResponse = Array.Empty<Homeroom>();
                if (teacherResponse.Length > 0)
                {
                    Teacher teacher = teacherResponse[0];
                    homeroomResponse = context.GetHomeroomsByTeacherUsername(teacher.Username).ToArray();
                }

                var studentResponse = context.Students.Where(s => s.Username == username && s.IsActive == true).Include(s=> s.Homeroom).ToArray<Student>();

                return new UserLoginResponse
                {
                    Admin = adminResponse.Length == 1 ? adminResponse[0] : null,
                    Teacher = teacherResponse.Length == 1? teacherResponse[0] : null,
                    Homeroom = homeroomResponse.Length >= 1? homeroomResponse[0] : null,
                    Student = studentResponse.Length == 1? studentResponse[0] : null
                };
            }
        }
    }
}
