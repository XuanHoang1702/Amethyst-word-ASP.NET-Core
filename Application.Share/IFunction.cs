using Application.Share.Consts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Share
{
    public interface IFunction
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string inputPassword, string storedHash);
        public string Config_ID();
        public string Config_ID_Admin();
        public string GenerateToken(string userId, string userLastName, string userEmail = "gmail@.com", string userPhone = "098", string? role = "");
        public TokenDTO DeToken(string token);
        public Task<bool> SendMailOTP(string email, string userName, string otpCode);
        public Task<bool> SendSmsOTP(string phone, string userName, string otpCode);
        public string OpenImgur();
        public string GenerateRefreshToken();
        public string Config_ID_Order();
        public Task<bool> MailVerifyAccount(string email, string userName);
    }
} 
