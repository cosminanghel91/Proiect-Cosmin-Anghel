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
    public class AddressesController : ControllerBase
    {
        private readonly StudentsService studentsService;
        private readonly AddressesService addressesService;
        private readonly MarksService marksService;
        private readonly StudentsRegistryDbContext ctx;

        public AddressesController(StudentsService studentsService, AddressesService addressesService, MarksService marksService, StudentsRegistryDbContext ctx)
        {
            this.studentsService = studentsService;
            this.addressesService = addressesService;
            this.marksService = marksService;
            this.ctx = ctx;
        }

        [HttpPost]
        public AddressToGetDto AddAddres([FromBody] AddressToCreateDto addressToCreate) =>
                addressesService.AddAddress(
                    addressToCreate.City, addressToCreate.Street, addressToCreate.No
                    ).ToAddressToGet();
        //[HttpPost("add-student-to-address/{addressId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    }
}
