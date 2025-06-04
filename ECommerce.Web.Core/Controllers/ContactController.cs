using Admin.DTO;
using Contact.DTO;
using Contact.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public List<ContactDTO> GetList(string status) => _contactService.GetList(status);

        [HttpPost]
        public async Task<ResultDTO> Create([FromBody] ContactCreateDTO input) => await _contactService.Create(input);

        [HttpPut]
        public async Task<ContactDTO> Update([FromHeader(Name = "Authorization")] string token, [FromBody] int id, [FromBody] string status = "COMPLETED")
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _contactService.Update(token, id, status);
        }
    }
}
