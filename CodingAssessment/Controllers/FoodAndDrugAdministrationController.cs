using CodingAssessment.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace CodingAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FoodAndDrugAdministrationController : ControllerBase
    {
        [Route("SendEmail")]
        [HttpGet]
        public IActionResult Getlistoffoodenforcementandsendemail()
        {
            string url = "https://api.fda.gov/food/enforcement.json?limit=10";

            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            Food myDeserializedClass = JsonConvert.DeserializeObject<Food>(response.Content.ReadAsStringAsync().Result);

            SendEmailAsync(myDeserializedClass);

            return Ok();
        }

        [NonAction]
        public async Task SendEmailAsync(Food emailObject)
        {
            var fromAddress = new MailAddress("your-email@gmail.com", "Your Name");
            var toAddress = new MailAddress("recipient@example.com", "Recipient Name");
            const string fromPassword = "your-password";
            const string subject = "Subject of the Email";
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(emailObject);

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };

            await smtpClient.SendMailAsync(message);
            Console.WriteLine("Email sent successfully.");
        }

        [Route("Foodenforcement")]
        [HttpGet]
        public IActionResult Getlistoffoodenforcement()
        {
            string url = "https://api.fda.gov/food/enforcement.json?limit=10";

            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            Food myDeserializedClass = JsonConvert.DeserializeObject<Food>(response.Content.ReadAsStringAsync().Result);

            return Ok(myDeserializedClass);
        }
    }
}