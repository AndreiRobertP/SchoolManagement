using Microsoft.EntityFrameworkCore;
using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Models.BusinessLogic
{
    public class UserPermisions
    {
        public bool IsAdmin { get; set; } = false;
        public bool IsTeacher { get; set; } = false;
        public bool IsHomeroomTeacher { get; set; } = false;
        public bool IsStudent { get; set; } = false;
        public bool IsRegistered => IsAdmin || IsTeacher || IsStudent;
    }

    public class UserBLL
    {
        public UserPermisions GetUserTypeLogin(string username)
        {

            using (var context = new SchoolManagementContext())
            {
                var adminResponse = context.Admins.Where(a => a.Username == username && a.IsActive == true).ToArray<Admin>();

                var teacherResponse = context.Teachers.Where(t => t.Username == username && t.IsActive == true).Include(t => t.Homeroom).ToArray<Teacher>();

                var studentResponse = context.Students.Where(s => s.Username == username && s.IsActive == true).ToArray<Student>();

                return new UserPermisions
                {
                    IsAdmin = adminResponse.Length > 0,
                    IsTeacher = teacherResponse.Length > 0,
                    IsHomeroomTeacher = teacherResponse.Length > 0 && teacherResponse[0].Homeroom != null,
                    IsStudent = studentResponse.Length > 0
                };
            }
        }
    }
}
