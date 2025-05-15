using Admin.DTO;

namespace Admin.Intfs
{
    public interface IAdminService
    {
        public Task<object> Login(AdminLoginDTO input);
        public Task<object> Create_Account(string token, CreateAdminDTO input);
        public Task<ResultDTO> Delete_Account(string token, string id);
    }
}
