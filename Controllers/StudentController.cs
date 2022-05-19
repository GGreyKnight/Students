using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Students.Entities;

namespace Students.Controllers
{
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly StudentDbContext _dbContext;

        public StudentController(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAll()
        {
            var students = _dbContext
                .Students
                .Include(s => s.Results)
                .ToList();

            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> Get([FromRoute] int id)
        {
            var student = _dbContext
                .Students
                .Include(s => s.Results)
                .FirstOrDefault(s => s.Id == id);

            if (student is null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public ActionResult CreateStudent([FromBody] Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();

            return Created($"/student/{student.Id}", null);
        }

        [HttpDelete("{id}")]
        public bool DeleteStudent([FromRoute] int id)
        {
            var student = _dbContext
                .Students
                .FirstOrDefault(s => s.Id == id);

            if (student is null) return false;

            _dbContext.Students.Remove(student);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
