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
            var mailMessage = new MailMessage {From = new MailAddress("abc@test.com")};
            mailMessage.To.Add(new MailAddress("latifur.maruf@hotmail.com"));

            mailMessage.Subject = "MD LATIFUR RAHMAN";

            mailMessage.Body = "This is a test";

            var mailClient=new SmtpClient("localhost"); 
            
            mailClient.Credentials = new NetworkCredential("abc@test.com", "maruf123");
            mailClient.UseDefaultCredentials = false;
            mailClient.DeliveryMethod=SmtpDeliveryMethod.Network;
            //mailClient.EnableSsl = true; 

            mailClient.Send(mailMessage);
        }
        
    }
}
