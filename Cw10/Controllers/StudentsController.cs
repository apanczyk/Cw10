using System.Collections.Generic;
using System.Linq;
using Cw10.Models;
using Cw10.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        
        private readonly IEfDbService _context;

        public StudentsController(IEfDbService context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(_context.GetPeople());
        }

    }
}