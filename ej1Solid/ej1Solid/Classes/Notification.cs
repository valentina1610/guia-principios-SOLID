using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ej1Solid.Interfaces;

namespace ej1Solid.Classes
{
    class Notification : INotificationService
    {
        public void SendEmail(Order order)
        {
            try
            {
                using var smtp = new SmtpClient("smtp.example.com");
                var mail = new MailMessage("sales@example.com", order.CustomerEmail);
                mail.Subject = $"Order Confirmation #{order.Id}";
                mail.Body = $"Thanks! Your total is {order.Total:C2}";
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[LOG] Failed to send email: " + ex.Message);
            }
        }
    }
}
