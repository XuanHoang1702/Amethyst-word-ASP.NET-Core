using Address.DTO;
using Address.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService) { _addressService = addressService; }

        [HttpGet]
        public List<AddressDTO> GetById([FromHeader(Name = "Authorization")] string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }
            return _addressService.GetAddresses(token);
        }

        [HttpPost]
        public Task<object> Create([FromHeader(Name = "Authorization")] string token, [FromBody] AddressDTO input)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }

            return _addressService.CreateAddress(token, input);
        }

        [HttpPut]
        public Task<object> Update([FromHeader(Name = "Authorization")] string token, [FromBody] AddressDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return _addressService.UpdateAddress(token, input);
        }

    }
}
