using MimeKit;

namespace BookHive.Helpers
{
    public class  Email
    {
        private IConfiguration _configuration;
        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool SendEmail(string from, string to, string subject, string content)
        {
            try
            {
                var sendermail = _configuration["Gmail:email"];

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("", sendermail));
                message.To.Add(new MailboxAddress("", to));              
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = content;
                message.Body = bodyBuilder.ToMessageBody();               
                var password = _configuration["Gmail:password"];


                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);                 
                    client.Authenticate(sendermail, password);
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
