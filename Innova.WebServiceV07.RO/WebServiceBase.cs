using Innova.Users;
using Innova.WebServiceV07.RO.DataModels.RabbitMQModels;
using Innova.WebServiceV07.RO.DataObjects;
using Innova.WebServiceV07.RO.Helpers;
using Innova.WebServiceV07.RO.RabbitMQPublishers;
using Metafuse3.BusinessObjects;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Configuration;
using System.Linq;
using System.Web.Services.Protocols;
using System.Xml;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    /// Summary description for WebServiceBase.
    /// </summary>
    public class WebServiceBase : System.Web.Services.WebService
    {
        private Registry registry;
        private Registry registryReadOnly;

        /// <summary>
        /// Default constructor for the web services base class
        /// </summary>
        public WebServiceBase()
        {
            //create a registry for use on web services
            this.registry = new Registry(ConfigurationManager.AppSettings["ConnectionString"], ConfigurationManager.AppSettings["SmtpServer"]);
            this.registryReadOnly = new Registry(ConfigurationManager.AppSettings["ConnectionStringReadOnly"], ConfigurationManager.AppSettings["SmtpServer"]);
        }

        /// <summary>
        /// Gets the <see cref="Registry"/> currently being used by the web service
        /// </summary>
        protected Registry Registry
        {
            get
            {
                return registry;
            }
        }

        //RO_
        /// <summary>
        /// Gets the ReadOnly <see cref="Registry"/> currently being used by the web service
        /// </summary>
        protected Registry RegistryReadOnly
        {
            get
            {
                return registryReadOnly;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="WebServiceKey"/> currently in use (set when Validate key) is invoked.
        /// </summary>
        protected WebServiceKey WebServiceKey { get; set; }

        #region External System Validation

        /// <summary>
        /// Validates the supplied key to ensure that the key is valid.
        /// </summary>
        /// <param name="key"><see cref="WebServiceKey"/> web service key</param>
        /// <param name="method"><see cref="string"/> method to log.</param>
        /// <returns><see cref="bool"/> flag indicating whether or not the key is valid</returns>
        protected bool ValidateKeyAndLogTransaction(WebServiceKey key, string method)
        {
            if (this.ValidateKey(key))
            {
                if (Global.EnableTransactionLogging)
                {
                    //go ahead and loGetOBDFixMasterTechsg the transaction of the access now....
                    using (Metafuse3.Data.SqlClient.SqlDataReaderWrapper dr = new Metafuse3.Data.SqlClient.SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
                    {
                        dr.ProcedureName = "ExternalSystemTransaction_Create";
                        dr.AddGuid("ExternalSystemId", this.WebServiceKey.ExternalSystemId.Value);
                        dr.AddNVarChar("MethodInvoked", method);

                        dr.AddNVarChar("MarketString", key.MarketString);
                        dr.AddNVarChar("LanguageString", key.LanguageString);
                        dr.AddInt32("Currency", key.Currency ?? 0);
                        dr.AddNVarChar("Region", key.Region);

                        dr.ExecuteNonQuery();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the supplied key.
        /// </summary>
        /// <param name="key"><see cref="WebServiceKey"/> web service key</param>
        /// <returns><see cref="bool"/> flag indicating whether or not the key is valid</returns>
        protected bool ValidateKey(WebServiceKey key)
        {
            this.WebServiceKey = (WebServiceKey)key;

            //if we still have a valid key, then the site is active
            if (this.Context.Cache.Get(this.WebServiceKey.Key) != null)
            {
                Guid? id = (Guid)this.Context.Cache.Get(this.WebServiceKey.Key);

                if (id.HasValue && id.Value == Guid.Empty)
                {
                    id = null;
                }

                this.WebServiceKey.ExternalSystemId = id;
            }
            else
            {
                //first time in, let's add it so we can see if the site is valid
                ExternalSystem externalSystem = ExternalSystem.GetActiveExternalSystemFromKey(this.Registry, this.WebServiceKey.Key);

                Guid? id = null;

                if (externalSystem != null && externalSystem.IsActive)
                {
                    id = externalSystem.Id;

                    this.Context.Cache.Insert(this.WebServiceKey.Key, id.Value, null, DateTime.Now.AddHours(4), System.Web.Caching.Cache.NoSlidingExpiration);

                    if (id == Guid.Empty)
                    {
                        id = null;
                    }
                }
                else
                {
                    id = Guid.Empty;
                    this.Context.Cache.Insert(this.WebServiceKey.Key, id, null, DateTime.Now.AddHours(4), System.Web.Caching.Cache.NoSlidingExpiration);
                    id = null;
                }

                this.WebServiceKey.ExternalSystemId = id;
            }

            //ok we have an external system and we have a key, so we can go ahead and initialize the runtime information here.
            if (this.WebServiceKey != null && this.WebServiceKey.ExternalSystemId.HasValue)
            {
                //create an instance of the web service key just to get a runtime info
                ExternalSystem ex = (ExternalSystem)this.Registry.CreateInstance(typeof(ExternalSystem), this.WebServiceKey.ExternalSystemId.Value);

                //set the labor rate from the key
                ex.RuntimeInfo.SetCurrentStateLaborRate(this.WebServiceKey.Region);

                // 2012-02-03 RG - There is a bug in the RunTimeInfo.SetCurrentLanguage method that is expecting the value "ex-MX"
                // for Spanish instead of the correct value "es-MX". That being the case we are going to switch a correct value to the
                // incorrect value in order to allow Spanish values to be requested.
                string tempLangString = this.WebServiceKey.LanguageString;
                if (tempLangString.ToLower() == "es-mx")
                {
                    tempLangString = "ex-MX";
                }

                //if there is a value then the language will be set
                ex.RuntimeInfo.SetCurrentLanguage(tempLangString);

                //if there is a value then the currency will be set
                ex.RuntimeInfo.SetCurrentCurrency(this.WebServiceKey.Currency);

                //if there is a value then the market will be set
                ex.RuntimeInfo.SetCurrentMarket(this.WebServiceKey.MarketString);

                //set the key runtime info to the currently used runtime info
                key.RuntimeInfo = ex.RuntimeInfo;

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion External System Validation

        #region Helper Methods

        //Nam moved this method from OBDfixWSBase 2017-10-19
        /// <summary>
        /// Check if userId is valid or not
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        internal static bool IsUserIdValid(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) return true; //skip checking if empty

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"00000000-0000-0000-0000-*");
            System.Text.RegularExpressions.Match match = regex.Match(userId);

            return !match.Success;
        }

        //Nam moved this method from OBDfixWSBase 2017-10-19

        #endregion Helper Methods

        #region Exception Handling

        /// <summary>
        /// Raises a SoapException based on the Exception that is passed in.
        /// </summary>
        /// <param name="exception">The <see cref="Exception"/> to re-throw as a <see cref="SoapException"/>.</param>
        protected void RaiseSoapException(Exception exception)
        {
            // See if we got a SoapException
            SoapException soapException = exception as SoapException;

            // If not, create one
            if (soapException == null)
            {
                XmlDocument doc = new XmlDocument();
                XmlNode detail = doc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);
                // This is the detail part of the exception
                detail.InnerText = exception.Message;

                soapException = new SoapException("Server was unable to process request", SoapException.ServerFaultCode, Context.Request.Url.AbsoluteUri, detail, exception);
            }

            // Now throw the SoapException
            throw soapException;
        }

        #endregion Exception Handling

        #region RabbitMQ

        protected void SendRequestToRabbitMQ(object requestModel, string logId, string queueName = "")
        {
            if (requestModel != null)
            {
                System.Threading.Thread.Sleep(500);

                var baseModel = (BaseRabbitMQRequestModel)requestModel;

                string serviceName = baseModel.ServiceName;
                string methodName = baseModel.MethodName;

                if (!string.IsNullOrEmpty(serviceName) && !string.IsNullOrEmpty(methodName))
                {
                    IRabbitMQHandler rabbitMQHandler = new RabbitMQHandler();
                    (bool isOk, Exception exception) = rabbitMQHandler.SendRequest(requestModel, queueName);

                    if (!isOk)
                    {
                        #region Log error message

                        //Log Error Message
                        Logger.Write($"Send Request To RabbitMQ Failed => logId: {logId} => methodName: {methodName} => queueName: {queueName} => Exception: {exception}");

                        #endregion Log error message

                        #region Save payload to file

                        var payloadInfo = baseModel.PayloadInfo;

                        if (payloadInfo.Any())
                        {
                            string externalSystemName = baseModel.ExternalSystemName;

                            string faliedFileName = $"{externalSystemName}_Payload_" + DateTime.Now.ToString("yyyyMMdd HHmmss.fffffff") + "_" + Guid.NewGuid() + ".txt";

                            Logger.Write($"Send Request To RabbitMQ Failed => SavePayloadToFile => logId: {logId} => externalSystemName: {externalSystemName} => faliedFileName: {faliedFileName}");

                            PayloadHelper.SavePayloadToFile(externalSystemName, Global.ServiceReadOnly_NewPayloadFolder_When_SendRequest_ToRabbitMQ_Failed, faliedFileName, payloadInfo);
                        }

                        #endregion Save payload to file
                    }
                    else
                    {
                        Logger.Write($"Send Request To RabbitMQ successfully => logId: {logId} => methodName: {methodName} => queueName: {queueName}");
                    }
                }
            }
        }

        /// <summary>
        /// Helper methods to keep all reports send from OR BB
        /// </summary>
        /// <param name="requestModel"></param>
        protected void SaveToLocalFolder(object requestModel, string newPayloadFolder)
        {
            try
            {
                if (requestModel != null)
                {
                    var baseModel = (BaseRabbitMQRequestModel)requestModel;

                    var payloadInfo = baseModel.PayloadInfo;

                    if (payloadInfo.Any())
                    {
                        string externalSystemName = baseModel.ExternalSystemName;

                        string reportFilename = $"{externalSystemName.Replace("'", "").Replace(" ", "")}_Payload_" + DateTime.Now.ToString("yyyyMMdd HHmmss.fffffff") + "_" + Guid.NewGuid() + ".txt";

                        PayloadHelper.SavePayloadToFileByDay(externalSystemName, newPayloadFolder, reportFilename, payloadInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write($"RO DiagnosticReportLoggingV2 => SaveToLocalFolder => Exception: {ex}");
            }
        }

        protected string GetExternalSystemName()
        {
            //Get ExternalSystem By WS Key
            return ExternalSystem.GetActiveExternalSystemFromKey(this.RegistryReadOnly, this.WebServiceKey.Key).Name;
        }

        #endregion RabbitMQ
    }
}