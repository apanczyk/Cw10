﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
  using Cw10.DTOs;
  using Cw10.Models;

namespace Cw10.Services
{
    public interface IEfDbService
    {
        public IEnumerable<Student> GetStudents();
        public Student ModifyStudent(string id, string name, string surname);
        public Student DeleteStudent(string id);
        public EnrollmentResponse EnrollStudent(EnrollStudentRequest request);
        void PromoteStudents(PromoteStudentRequest request);
    }
}
