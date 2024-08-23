using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Models;

namespace Proiect_Cosmin_Anghel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly StudentsRegistryDbContext ctx;
        public SubjectsController(StudentsRegistryDbContext ctx)
        {
            this.ctx = ctx;
        }

        [HttpPost]
        public Subject AddSubject([FromBody] SubjectToCreateDto subjectToCreate)
        {
            if (ctx.Subjects.Any(s => s.Name == subjectToCreate.Name))
            {
                return null;
            }
            var subject = new Subject
            {
                Name = subjectToCreate.Name,
            };
            ctx.Subjects.Add(subject);
            ctx.SaveChanges();
            return subject;
        }
    }
}
