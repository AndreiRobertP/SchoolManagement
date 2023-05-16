using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Models.BusinessLogic
{
    public class MeanBLL
    {
        public ObservableCollection<Mean> GetMeans()
        {
            ObservableCollection<Mean> collection = new ObservableCollection<Mean>();
            using (var context = new SchoolManagementContext())
            {
                var Means = context.Means.Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var Mean in Means)
                    collection.Add(Mean);
            }
            return collection;
        }

        public void ComputeAndAddMean(Mean newMean)
        {
            using (var context = new SchoolManagementContext())
            {
                //Make sure mean isn't already closed
                if (GetMeansByShtAndStudentAndSemester(newMean.Sht, newMean.Student, newMean.Semester).Any())
                {
                    throw new Exception("Media este deja incheiata");
                }

                Sht sht = context.Shts.Where(s => s.ShtId == newMean.Sht.ShtId && s.IsActive).Single();
                Student student = context.Students.Where(s => s.StudentId == newMean.Student.StudentId && s.IsActive).Single();

                newMean.Sht = sht;
                newMean.Student = student;

                var grades = context.Grades.Where(g =>
                    g.IsActive && g.Sht == sht && g.Semester == newMean.Semester && g.Student == student);

                //Make sure there are more than 3 grades
                if (grades.Count() < 3)
                {
                    throw new Exception("Note insuficiente");
                }

                var thesis = grades.Where(g => g.IsActive && g.IsThesis);

                //Make sure there is only one thesis/semester/subject if this Sht accepts a Thesis
                if (sht.HasThesis)
                {
                    if (thesis.Count() != 1)
                    {
                        throw new Exception("Aceasta materie necesita teza");
                    }
                }

                //Separate non-thesis grades and compute avg
                var nonThesisGrades = grades.Where(g => !g.IsThesis);
                double average = nonThesisGrades.Average(g => g.Value);

                //If we have thesis
                if (thesis.Count() == 1)
                {
                    var thesisValue = thesis.Single().Value;
                    average = average * 0.75 + thesisValue * 0.25;
                }

                average = ((int)average * 100) / 100d;

                newMean.Value = (float)average;
                context.Means.Add(newMean);
                context.SaveChanges();
            }
        }

        public void RemoveMean(Mean deleteMean)
        {
            using (var context = new SchoolManagementContext())
            {
                context.Remove(deleteMean);
                context.SaveChanges();
            }
        }

        public ObservableCollection<Mean> GetMeansByShtAndStudentAndSemester(Sht sht, Student student, int semester)
        {
            ObservableCollection<Mean> collection = new ObservableCollection<Mean>();
            using (var context = new SchoolManagementContext())
            {
                var Means = context.Means.Where(f => f.Sht.ShtId == sht.ShtId && f.Student.StudentId == student.StudentId && f.Semester == semester).Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var Mean in Means)
                    collection.Add(Mean);
            }
            return collection;
        }
    }
}
