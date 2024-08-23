using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Dto.Extensions;
using Proiect_Cosmin_Anghel.Services;

namespace Proiect_Cosmin_Anghel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsService studentsService;

        public StudentsController(StudentsService studentsService)
        {
            this.studentsService = studentsService;
        }

        [HttpPost]
        public StudentToGetDto AddStudent([FromBody] StudentToCreateDto studentToCreate) =>
        studentsService.AddStudent(
             studentToCreate.LastName,
             studentToCreate.FirstName,
             studentToCreate.Age,
             studentToCreate.AddressId
             ).ToStudentToGet();


        [HttpGet]
        public IEnumerable<StudentToGetDto> GetAll() =>
             studentsService.GetAll().Select(s => s.ToStudentToGet()).ToList();

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        public IActionResult GetById(int id)
        {
            return Ok(studentsService.GetStudentById(id).ToStudentToGet());
        }

        [HttpGet("{studentId}/Address")]
        public async Task<ActionResult<StudentToGetDto>> GetStudentAddress(int studentId)
        {
            var studentAddress = await studentsService.GetStudentAddressAsync(studentId);

            if (studentAddress == null)
            {
                return NotFound();
            }

            return Ok(studentAddress);
        }

        [HttpPut("Change student data")]
        public void UpdateStudent([FromHeader] int studentId, [FromBody] StudentToUpdate newStudentData) =>
                     studentsService.ChangeStudentData(studentId, newStudentData.ToEntity());


        [HttpDelete("studentById{id}")]
        public async Task<IActionResult> DeleteStudentWithId(int id)
        {
            var result = await studentsService.DeleteStudentById(id);

            if (!result)
            {
                return NotFound(new { Message = "Studentul nu a fost găsit." });
            }

            return NoContent(); // 204 No Content, ștergerea a fost realizată cu succes
        }

        [HttpDelete("studentWithAddress{studentId}")]
        public IActionResult DeleteStudentWithAddress(int studentId, [FromQuery] bool deleteAddress = false)
        {
            var result = studentsService.DeleteStudentWithAddress(studentId, deleteAddress);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpDelete("studentComplete{studentId}")]
        public IActionResult DeleteStudentComplete(int studentId, [FromQuery] bool deleteComplete = false)
        {
            var result = studentsService.DeleteStudentComplete(studentId, deleteComplete);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();

        }
    }
}
