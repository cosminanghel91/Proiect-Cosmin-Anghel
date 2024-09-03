using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Models;
using Proiect_Cosmin_Anghel.Services;

namespace Proiect_Cosmin_Anghel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly StudentsRegistryDbContext ctx;
        private readonly SubjectsService subjectsService;
        public SubjectsController(StudentsRegistryDbContext ctx, SubjectsService subjectsService)
        {
            this.ctx = ctx;
            this.subjectsService = subjectsService;
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

        [HttpGet("names")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SubjectToGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult GetSubjectNames()
        {
            var subjectNames = subjectsService.GetAllSubjectNames();
            return Ok(subjectNames);
        }
    }
}
