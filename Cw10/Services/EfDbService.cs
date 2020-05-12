﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
