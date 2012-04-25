using System.Net;
using System.Net.Mail;
using NUnit.Framework;

namespace FrameWorkTests {
    [TestFixture]
    public class Test1
    {        
        [SetUp]
        public void Init()
        {
            
        }

        [TearDown]
        public void EndCase()
        {
            
        }
        
        
        [Test]
        public void SendMail()
        {
            var mailMessage = new MailMessage {From = new MailAddress("marufbd@gmail.com")};
            mailMessage.To.Add(new MailAddress("marufbd@gmail.com"));

            mailMessage.Subject = "MD LATIFUR RAHMAN";

            mailMessage.Body = "This is a test";

            var mailClient=new SmtpClient("smtp.gmail.com", 587); 
            
            mailClient.Credentials = new NetworkCredential("marufbd@gmail.com", "freakyt!me");
            mailClient.UseDefaultCredentials = false;
            mailClient.DeliveryMethod=SmtpDeliveryMethod.Network;
            mailClient.EnableSsl = true; 

            mailClient.Send(mailMessage);
        }
        
    }
}
