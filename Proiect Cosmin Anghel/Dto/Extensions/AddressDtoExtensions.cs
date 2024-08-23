using Proiect_Cosmin_Anghel.Models;

namespace Proiect_Cosmin_Anghel.Dto.Extensions
{
    public static class AddressDtoExtensions
    {
        public static AddressToGetDto ToAddressToGet(this Address address)
        {
            if (address == null)
                return null;
            return new AddressToGetDto
            {
                City = address.City,
                Street = address.Street,
                No = address.No,
            };
        }
    }
}
