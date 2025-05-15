using Address.DTO;
using System.Globalization;

namespace Address.Intfs
{
    public interface IAddressService
    {
        public List<AddressDTO> GetAddresses(string token);
        public Task<object> CreateAddress(string token, AddressDTO input);
        public Task<object> UpdateAddress(string token, AddressDTO input);
    }
}
