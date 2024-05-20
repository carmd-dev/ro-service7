using System;
using System.Configuration;
using System.Net.Mail;

namespace Innova.EmailCommunications
{
    /// <summary>
    /// The EmailHelper subject handles the business logic and data access for communicating with an SMTP server and sending emails.
    /// </summary>
    public class EmailHelper
    {
        private string smtpServer = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpServerNumTwo"]) ? ConfigurationManager.AppSettings["SmtpServerNumTwo"] : "";
        private string smtpAuthServer = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpServerNumTwo"]) ? ConfigurationManager.AppSettings["SmtpServerNumTwo"] : "";
        private bool smtpLoginRequired = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpLoginRequired"]) ? bool.Parse(ConfigurationManager.AppSettings["SmtpLoginRequired"]) : false;
        private bool smtpEnableSsl = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpEnableSsl"]) ? bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]) : false;
        private bool smtpUseDefaultCredentials = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"]) ? bool.Parse(ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"]) : false;
        private string smtpUsername = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpUsername"]) ? ConfigurationManager.AppSettings["SmtpUsername"] : string.Empty;
        private string smtpPassword = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpPassword"]) ? ConfigurationManager.AppSettings["SmtpPassword"] : string.Empty;
        private string smtpDomain = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["SmtpDomain"]) ? ConfigurationManager.AppSettings["SmtpDomain"] : string.Empty;
        private int smtpPort = Int32.Parse(ConfigurationManager.AppSettings["SmtpPort"], 0);

        /// <summary>
        /// The pre-defined class method.
        /// </summary>
        public EmailHelper()
        { }

        /// <summary>
        /// Sends email
        /// </summary>
        /// <param name="msg">A <see cref="MailMessage"/> Mail Message to be sent out</param>
        /// <param name="isAuthenticationRequired">A <see cref="bool"/> True/False value to request or not to an authentication SMTP server</param>
        public void SendMail(MailMessage msg, bool isAuthenticationRequired = false)
        {
            if (isAuthenticationRequired)
                SendMailWithSmtpAuthentication(msg);
            else
                SendMailWithoutSmtpAuthentication(msg);
        }

        /// <summary>
        /// Sends email using SMTP Authentication.
        /// </summary>
        /// <param name="msg"></param>
        public void SendMailWithSmtpAuthentication(MailMessage msg)
        {
            SmtpClient smtpClient = new SmtpClient(smtpAuthServer);
            if (smtpPort != 0) smtpClient.Port = smtpPort;
            if (smtpEnableSsl)
            {
                smtpClient.EnableSsl = true;

                //Added on 2019-08-31 4 PM: Solves the SSL certificate issue "The remote certificate is invalid according to the validation procedure."
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            }
            if (smtpLoginRequired)
            {
                if (!string.IsNullOrEmpty(smtpDomain)) //If included a domain value
                    smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword, smtpDomain);
                else
                    smtpClient.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
            }
            if (smtpUseDefaultCredentials) smtpClient.UseDefaultCredentials = true;

            smtpClient.Send(msg);
        }

        /// <summary>
        /// Sends email without SMTP Authentication
        /// </summary>
        /// <param name="msg"></param>
        public void SendMailWithoutSmtpAuthentication(MailMessage msg)
        {
            if (msg.To.Count > 0)
            {
                foreach (var email in msg.To)
                {
                    MailMessage mailObj = new MailMessage(
                        msg.From.Address //From email address
                        , email.Address //To email address
                        , msg.Subject //Subject
                        , msg.Body //Body
                    );

                    SmtpClient smtpClient = new SmtpClient(smtpServer);
                    if (smtpPort != 0) smtpClient.Port = smtpPort;
                    if (smtpEnableSsl)
                    {
                        smtpClient.EnableSsl = true;

                        //Added on 2019-08-31 4 PM: Solves the SSL certificate issue "The remote certificate is invalid according to the validation procedure."
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                    }

                    smtpClient.Send(mailObj);
                }
            }
        }
    }
}