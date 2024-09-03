using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proiect_Cosmin_Anghel.Data;
using Proiect_Cosmin_Anghel.Dto;
using Proiect_Cosmin_Anghel.Dto.Extensions;
using Proiect_Cosmin_Anghel.Services;

namespace Proiect_Cosmin_Anghel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentsRegistryDbContext ctx;
        private readonly StudentsService studentsService;

        public StudentsController(StudentsRegistryDbContext ctx, StudentsService studentsService)
        {
            this.ctx = ctx;
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
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult GetById(int id)
        {
            var student = studentsService.GetStudentById(id).ToStudentToGet();
            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound("student not found");
            }

        }

        [HttpGet("{studentId}/Address")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult<StudentToGetDto>> GetStudentAddress(int studentId)
        {
            var studentAddress = await studentsService.GetStudentAddressAsync(studentId);
            if (studentId == 0)
            {
                return NotFound("student id is invalid");
            }
            else
            if (studentAddress == null)
            {
                return NotFound("student address not found");
            }
            return Ok(studentAddress);
        }

        [HttpPut("Change student data")]
        public void UpdateStudent([FromHeader] int studentId, [FromBody] StudentToUpdate newStudentData) =>
                      studentsService.ChangeStudentData(studentId, newStudentData.ToEntity());


        [HttpDelete("studentById{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> DeleteStudentWithId(int id)
        {
            var result = await studentsService.DeleteStudentById(id);

            if (!result)
            {
                return NotFound("student not found");
            }

            return Ok($"student with id:  {id} has been deleted");
        }




        [HttpDelete("studentWithAddress{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult DeleteStudentWithAddress(int studentId, [FromQuery] bool deleteAddress = false)
        {
            var result = studentsService.DeleteStudentWithAddress(studentId, deleteAddress);

            if (!result)
            {
                return NotFound($"student id {studentId} not found");
            }

            return Ok($"student with id:  {studentId} was deleted with the address");

        }


        [HttpDelete("studentComplete{studentId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StudentToGetDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public IActionResult DeleteStudentComplete(int studentId, [FromQuery] bool deleteComplete = false)
        {
            var result = studentsService.DeleteStudentComplete(studentId, deleteComplete);

            if (!result)
            {
                return NotFound($"student id {studentId} not found");
            }

            return Ok($"student with id:  {studentId} has been deleted from all tables");

        }
    }
}
