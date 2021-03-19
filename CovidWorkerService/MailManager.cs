using CovidWorkerService.Model;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidWorkerService
{
    public class MailManager
    {
        private readonly string mailServer, login, password;
        private readonly int port;
        private readonly bool ssl;
        public MailManager(string mailServer, int port, bool ssl, string login, string password)
        {
            this.mailServer = mailServer;
            this.port = port;
            this.ssl = ssl;
            this.login = login;
            this.password = password;
        }

        public async System.Threading.Tasks.Task<bool> SendVaccineAvailability(Location location, User user)
        {
            try
            {
                using (var smtpClient = new SmtpClient())
                {
                    using (var client = new ImapClient())
                    {
                        client.Connect(mailServer, port, ssl);

                        // Note: since we don't have an OAuth2 token, disable
                        // the XOAUTH2 authentication mechanism.
                        client.AuthenticationMechanisms.Remove("XOAUTH2");

                        client.Authenticate(login, password);

                        smtpClient.Connect("smtp.gmail.com", 587);
                        smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
                        smtpClient.Authenticate(login, password);

                        var inbox = client.Inbox;
                        var reply = new MimeMessage();
                        reply.To.Add(MailboxAddress.Parse(user.Email));
                        reply.From.Add(MailboxAddress.Parse("maddenpickems@gmail.com"));

                        if(user.PhoneNumber != null)
                        {
                            reply.To.Add(MailboxAddress.Parse(user.PhoneNumber));
                        }


                        reply.Subject = "Vaccine Availability";

                        string text = String.Format("Vaccine availability at {0} in {1}, {2} {3}.", location.name, location.address.city, location.address.state, location.address.zip);
                        text += "<br />";
                        text += string.Format("https://www.hy-vee.com/my-pharmacy/covid-vaccine-consent");

                        var bodyBuilder = new BodyBuilder();
                        bodyBuilder.HtmlBody = string.Format("<span style='font-size:12.0pt;font-family:\"Courier New\" '>{0}</span>", text);


                        reply.Body = bodyBuilder.ToMessageBody();
                        smtpClient.Send(reply);

                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }

}
