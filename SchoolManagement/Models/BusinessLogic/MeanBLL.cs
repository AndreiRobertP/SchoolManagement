﻿using SchoolManagement.Models.DataAccess;
using SchoolManagement.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                if (GetMeanByShtAndStudentAndSemester(newMean.Sht, newMean.Student, newMean.Semester) != null)
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

                int average;
                //If we have thesis
                if (thesis.Count() == 1)
                {
                    var thesisValue = thesis.Single();
                    average = Grade.ComputeMeanWithThesis(thesisValue, nonThesisGrades.ToArray());
                }
                //If we don't have thesis
                else
                {
                    average = Grade.ComputeMeanWithoutThesis(nonThesisGrades.ToArray());
                }


                newMean.Value = average;
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

        public Mean? GetMeanByShtAndStudentAndSemester(Sht sht, Student student, int semester)
        {
            using (var context = new SchoolManagementContext())
            {
                var means = context.Means.Where(f => f.Sht.ShtId == sht.ShtId && f.Student.StudentId == student.StudentId && f.Semester == semester).Include(g => g.Student).Include(g => g.Sht).ToList();

                if (means.Count == 0)
                    return null;

                return means[0];
            }
        }

        public ObservableCollection<Mean> GetMeansByStudent(Student student)
        {
            ObservableCollection<Mean> collection = new ObservableCollection<Mean>();
            using (var context = new SchoolManagementContext())
            {
                var means = context.Means.Where(f => f.Student.StudentId == student.StudentId).Include(g => g.Student).Include(g => g.Sht).ToList();

                foreach (var mean in means)
                    collection.Add(mean);
            }
            return collection;
        }
    }
}
