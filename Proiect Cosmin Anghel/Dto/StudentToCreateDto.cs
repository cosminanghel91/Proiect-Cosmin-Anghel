using System.ComponentModel.DataAnnotations;

namespace Proiect_Cosmin_Anghel.Dto
{
    public class StudentToCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Range(2, int.MaxValue)]
        public int Age { get; set; }
        public int? AddressId { get; set; }
    }
}
