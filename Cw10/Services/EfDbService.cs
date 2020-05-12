﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Cw10.Models;

 namespace Cw10.Services
{
    public class EfDbService : IEfDbService
    {
        public IEnumerable<Student> GetPeople()
        {
            var db = new s18706Context();
            return db.Student.ToList();
        }
    }
}
