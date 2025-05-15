using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Twilio.Types;
using Application.Share.Consts.DTO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Edge;



namespace Application.Share
{
    public class Function : IFunction
    {
        private readonly string? _secretkey;
        private readonly string? _smtPassword;
        private readonly string? _emailWebsite;
        private readonly string? _websiteName;
        private readonly string? _smtpServer;
        private readonly string? _emailImgur;
        private readonly string? _passwrdImgur;
        private readonly string? _userArgent;

        public Function(IConfiguration configuration)
        {
            _secretkey = configuration["SecretKey"];
            _smtPassword = configuration["EmailSettings:SmtpPassword"];
            _emailWebsite = configuration["EmailSettings:WebsiteEmail"];
            _websiteName = configuration["EmailSettings:WebsiteName"];
            _smtpServer = configuration["EmailSettings:SmtpServer"];
            _emailImgur = configuration["Imgur:email"];
            _passwrdImgur = configuration["Imgur:passwrd"];
            _userArgent = configuration["Imgur:user_argent"];
        }

        public string HashPassword(string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretkey)))
            {
                byte[] result = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(result);
            }
        }

        public bool VerifyPassword(string inputPassword, string storedHash)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput == storedHash;
        }

        public string Config_ID()
        {
            var randId = Guid.NewGuid().ToString("N");
            var numberId = string.Concat(randId.Where(char.IsDigit).Take(5));
            var charId = string.Concat(randId.Where(char.IsLetter).Take(3)).ToUpper();

            return $"CS_{charId}{numberId}";
        }

        public string Config_ID_Admin()
        {
            var randId = Guid.NewGuid().ToString("N");
            var numberId = string.Concat(randId.Where(char.IsDigit).Take(5));
            var charId = string.Concat(randId.Where(char.IsLetter).Take(3)).ToUpper();

            return $"AD_{charId}{numberId}";
        }

        public string Config_ID_Order()
        {
            var randId = Guid.NewGuid().ToString("N");
            var numberId = string.Concat(randId.Where(char.IsDigit).Take(5));
            var charId = string.Concat(randId.Where(char.IsLetter).Take(3)).ToUpper();

            return $"OR{charId}{numberId}";
        }

        public string GenerateToken(string userId, string userLastName, string userEmail = "gmail@.com", string userPhone = "098", string? role = "")
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretkey);

            var tokenDes = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Email, userEmail),
                    new Claim(ClaimTypes.Name, userLastName),
                    new Claim(ClaimTypes.MobilePhone, userPhone),
                    new Claim(ClaimTypes.Role, role ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandle.CreateToken(tokenDes);
            return tokenHandle.WriteToken(token);
        }

        public TokenDTO DeToken(string token)
        {
            var tokenHandle = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretkey);

            try
            {
                var tokenValidationParams = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandle.ValidateToken(token, tokenValidationParams, out SecurityToken validatedToken);
                var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                if(string.IsNullOrEmpty(userId)) 
                {
                    throw new Exception("Token không hợp lệ");
                }

                TokenDTO result = new TokenDTO
                {
                    UserId = userId,
                    Role = string.IsNullOrEmpty(role) ? "USER" : role
                };
                return result;
            }
            catch
            {
                Console.WriteLine(Console.Error);
                throw;
            }
        }
        public async Task<bool> SendMailOTP(string email, string userName, string otpCode)
        {
            try
            {
                var mes = new MimeMessage();
                mes.From.Add(new MailboxAddress(_websiteName, _emailWebsite));
                mes.To.Add(new MailboxAddress(userName, email));
                mes.Subject = "Mã xác nhận OTP dùng một lần";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<h3>Xin chào {userName},</h3>" +
                               $"<p>Mã OTP của bạn là: <strong>{otpCode}</strong></p>" +
                               "<p>Mã này có hiệu lực trong 5 phút. Vui lòng không chia sẻ với bất kỳ ai.</p>" +
                               $"<p>Trân trọng,<br/>{_websiteName}</p>"
                };
                mes.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, 587, SecureSocketOptions.StartTls);
                if (!client.IsConnected) return false;

                await client.AuthenticateAsync(_emailWebsite, _smtPassword);
                if (!client.IsAuthenticated) return false;

                await client.SendAsync(mes);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> SendSmsOTP(string phone, string userName, string otpCode)
        {
            try
            {
                
                string messageBody = $"Xin chào {userName}, mã OTP của bạn là: {otpCode}. Mã này có hiệu lực trong 5 phút.";

                if (!phone.StartsWith("+"))
                {
                    phone = phone.Trim();
                    if (phone.StartsWith("0"))
                    {
                        phone = "+84" + phone.Substring(1);
                    }
                    else
                    {
                        phone = "+84" + phone;
                    }
                }

                var message = await Twilio.Rest.Api.V2010.Account.MessageResource.CreateAsync(
                    body: messageBody,
                    from: new PhoneNumber("+19206565477"),
                    to: new PhoneNumber(phone)
                );

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi SMS: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> MailVerifyAccount(string email, string userName)
        {
            try
            {
                string otpCode = new Random().Next(100000, 999999).ToString();
                var mes = new MimeMessage();
                mes.From.Add(new MailboxAddress(_websiteName, _emailWebsite));
                mes.To.Add(new MailboxAddress(userName, email));
                mes.Subject = "Mã xác nhận OTP dùng một lần";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $"<h3>Xin chào {userName},</h3>" +
                               $"<p>Mã OTP của bạn là: <strong>{otpCode}</strong></p>" +
                               "<p>Mã này có hiệu lực trong 5 phút. Vui lòng không chia sẻ với bất kỳ ai.</p>" +
                               $"<p>Trân trọng,<br/>{_websiteName}</p>"
                };
                mes.Body = bodyBuilder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_smtpServer, 587, SecureSocketOptions.StartTls);
                if (!client.IsConnected) return false;

                await client.AuthenticateAsync(_emailWebsite, _smtPassword);
                if (!client.IsAuthenticated) return false;

                await client.SendAsync(mes);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi gửi email: {ex.Message}");
                return false;
            }
        }
        public string OpenImgur()
        {
            var options = new EdgeOptions();
            options.AddArgument(_userArgent);
            IWebDriver driver = new EdgeDriver(options);
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);

                driver.Navigate().GoToUrl("https://imgur.com/signin#%2Fuser%2FTranXuanHoang%2Fposts");

                wait.Until(d =>
                {
                    try
                    {
                        return d.Url.Contains("imgur.com") &&
                               ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete");
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                });

                driver.Navigate().Refresh();
                wait.Until(d => ((IJavaScriptExecutor)d).ExecuteScript("return document.readyState").Equals("complete"));

                //Email
                wait.Until(d =>
                {
                    try
                    {
                        var element = d.FindElement(By.Name("username"));
                        return element.Displayed && element.Enabled;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
                IWebElement emailField = driver.FindElement(By.Name("username"));
                emailField.SendKeys(_emailImgur);
                Thread.Sleep(new Random().Next(1000, 3000));

                //Passwrd
                wait.Until(d =>
                {
                    try
                    {
                        var element = d.FindElement(By.Name("password"));
                        return element.Displayed && element.Enabled;
                    }
                    catch(NoSuchElementException)
                    {
                        return false;
                    }
                });
                IWebElement passwrdField = driver.FindElement(By.Name("password"));
                passwrdField.SendKeys(_passwrdImgur);
                Thread.Sleep(new Random().Next(1000, 3000));

                wait.Until(d =>
                {
                    try
                    {
                        var element = d.FindElement(By.Id("Imgur"));
                        return element.Displayed && element.Enabled;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }

                });
                IWebElement nextButton = driver.FindElement(By.Id("Imgur"));
                nextButton.Click();
                Thread.Sleep(new Random().Next(1000, 3000));


                // Navigate Upload
                driver.Navigate().GoToUrl("https://imgur.com/upload");
                Thread.Sleep(new Random().Next(1000, 3000));

                wait.Until(d =>
                {
                    try
                    { 
                        var element = d.FindElement(By.ClassName("PopUpActions-filePicker"));
                        return (element.Displayed && element.Enabled);
                    }
                    catch(NoSuchElementException)
                    {
                        return false;
                    }
                });
                IWebElement upload = driver.FindElement(By.ClassName("PopUpActions-filePicker"));
                upload.Click();
                Thread.Sleep(new Random().Next(100, 300));

                wait.Until(d =>
                {
                    try
                    {
                        var element = driver.FindElement(By.ClassName("PostContent-imageWrapper-rounded"));
                        return element.Displayed && element.Enabled;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
                wait.Until(d => d.FindElements(By.TagName("img")).Any(img => img.GetAttribute("src").Contains("i.imgur.com")));
                IWebElement uploadedImage = driver.FindElements(By.TagName("img")).First(img => img.GetAttribute("src").Contains("i.imgur.com"));
                string imageUrl = uploadedImage.GetAttribute("src");
                string imageId = imageUrl.Split('/').Last();
                return imageId;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                // driver.Quit();
            }
        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
