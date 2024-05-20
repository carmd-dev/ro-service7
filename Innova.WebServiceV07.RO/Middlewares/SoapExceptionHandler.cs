using System;
using System.IO;
using System.Net.Mail;
using System.Web;
using System.Web.Services.Protocols;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    ///
    /// </summary>
    public class SoapExceptionHandler : System.Web.Services.Protocols.SoapExtension
    {
        private Stream _OldStream;
        private Stream _NewStream;

        /// <summary>
        ///
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="initializer"></param>
        public override void Initialize(object initializer)
        {
        }

        /// <summary>
        /// This method saves the provided stream and returns a new stream.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to saved.</param>
        /// <returns>A new <see cref="Stream"/>.</returns>
        public override Stream ChainStream(Stream stream)
        {
            this._OldStream = stream;
            this._NewStream = new MemoryStream();
            return _NewStream;
        }

        /// <summary>
        /// This method copies one stream to another.
        /// </summary>
        /// <param name="fromStream">The source <see cref="Stream"/>.</param>
        /// <param name="toStream">The destination <see cref="Stream"/>.</param>
        private void Copy(Stream fromStream, Stream toStream)
        {
            StreamReader sr = new StreamReader(fromStream);
            StreamWriter sw = new StreamWriter(toStream);
            sw.Write(sr.ReadToEnd());
            sw.Flush();
        }

        /// <summary>
        /// Processes the SOAP message.
        /// </summary>
        /// <param name="message">The <see cref="SoapMessage"/> to be processed.</param>
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeDeserialize:
                    this.Copy(_OldStream, _NewStream);
                    _NewStream.Position = 0;
                    break;

                case SoapMessageStage.AfterSerialize:
                    Exception ex = null;
                    if (message.Exception != null && message.Exception.InnerException != null)
                    {
                        ex = message.Exception.InnerException;
                    }
                    if (ex != null)
                    {
                        this._NewStream.Position = 0;
                        TextReader tr = new StreamReader(this._NewStream);
                        string messageStreamText = tr.ReadToEnd();
                        // overwrite the stream with the detail node from the exception
                        string strDetailNode = "";
                        if (message.Exception.Detail != null)
                        {
                            strDetailNode = message.Exception.Detail.OuterXml;
                            messageStreamText = messageStreamText.Replace("<detail />", strDetailNode);
                        }

                        this._NewStream = new MemoryStream();
                        TextWriter tw = new StreamWriter(this._NewStream);
                        tw.Write(messageStreamText);
                        tw.Flush();

                        /*****************************************************************************
						*
						* Perform Error Tracking Routines
						*
						* ***************************************************************************/
                        if (Global.IsLogError || Global.IsEmailError)
                        {
                            Metafuse3.Web.Errors.Error error = new Metafuse3.Web.Errors.Error(ex);

                            if (Global.IsLogError)
                            {
                                error.LogError();
                            }

                            if (Global.IsEmailError)
                            {
                                string errorMessage = "";

                                errorMessage += error.GetErrorMessageDetails();
                                errorMessage += Environment.NewLine;
                                errorMessage += "Assembly: " + System.Reflection.Assembly.GetExecutingAssembly().FullName + Environment.NewLine + Environment.NewLine;
                                errorMessage += ".Net Version: " + System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion + Environment.NewLine + Environment.NewLine;
                                errorMessage += "SOAP Exception Detail: " + strDetailNode + Environment.NewLine + Environment.NewLine;
                                errorMessage += "SoapMessage: " + messageStreamText;

                                error.EmailError(Global.ErrorEmailAddress, Global.ErrorEmailAddress, Global.SmtpServer, errorMessage);

                                //Added on 2019-09-01 12:15 AM: Send emails out
                                MailMessage msg = new MailMessage();
                                msg.To.Add(new MailAddress(Global.ErrorEmailAddress));
                                msg.From = new MailAddress(Global.ErrorEmailAddress);
                                msg.Subject = "Form: " + HttpContext.Current.Request.Path + " - Error: "; //Subject
                                msg.Body = errorMessage; //Body

                                var emailSender = new EmailCommunications.EmailHelper();
                                emailSender.SendMail(msg, true);
                            }
                        }
                    }

                    this._NewStream.Position = 0;
                    this.Copy(this._NewStream, this._OldStream);

                    break;
            }
        }
    }
}