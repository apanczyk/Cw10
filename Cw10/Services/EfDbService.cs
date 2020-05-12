﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  using Cw10.DTOs;
  using Cw10.DTOs;
  using Cw10.Models;

 namespace Cw10.Services
{
    public class EfDbService : IEfDbService
    {
        public IEnumerable<Student> GetStudents()
        {
            var db = new s18706Context();
            return db.Student.ToList();
        }

        public Student ModifyStudent(string id, string name, string surname)
        {
            var db = new s18706Context();
            var student = db.Student.SingleOrDefault(x => x.IndexNumber == id);
            
            if(name != null && student != null)
                student.FirstName = name;
            if(surname != null && student != null)
                student.LastName = surname;
            
            db.SaveChanges();
            return student;
        }

        public Student DeleteStudent(string id)
        {
            var db = new s18706Context();
            var student = db.Student.SingleOrDefault(x => x.IndexNumber == id);
            
            if(student != null)
                db.Remove(student);
            
            db.SaveChanges();
            return student;
        }

        public EnrollmentResponse EnrollStudent(EnrollStudentRequest request)
        {
            var db = new s18706Context();
            
            using(var dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    var idStudies = db.Studies.SingleOrDefault(x=>x.Name==request.Studies);
                    if (idStudies == null)
                        throw new Exception();
                    
                    if (db.Enrollment.Where(x => x.Semester == 1)
                        .SingleOrDefault(x => x.IdStudy == idStudies.IdStudy) == null)
                    {
                        db.Enrollment.Add(new Enrollment
                        {
                            IdEnrollment = db.Enrollment.Max(x=>x.IdEnrollment)+1,
                            Semester = 1,
                            IdStudy = idStudies.IdStudy,
                            StartDate = DateTime.Now
                        });
                    }

                    db.Student.Add(new Student
                    {
                        IndexNumber = request.IndexNumber,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        BirthDate = request.Birthdate,
                        IdEnrollment = db.Enrollment.Where(x=>x.Semester==1)
                            .Where(x=>x.IdStudy==idStudies.IdStudy)
                            .Select(x=>x.IdEnrollment).First()
                    });

                    db.SaveChanges();
                    dbTran.Commit();
                    return db.Enrollment.Where(x => x.Semester == 1)
                        .Where(x => x.IdStudy == idStudies.IdStudy).Select(x=> new EnrollmentResponse
                        {
                            IdEnrollment = x.IdEnrollment,
                            IdStudy = x.IdStudy,
                            Semester = x.Semester,
                            StartDate = x.StartDate
                        }).First();
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    return null;
                }
            }
        }

        public void PromoteStudents(PromoteStudentRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
