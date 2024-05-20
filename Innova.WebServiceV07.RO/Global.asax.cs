using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.SessionState;

using Metafuse3.Data.SqlClient;
using Metafuse3.BusinessObjects;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    /// Summary description for Global.
    /// </summary>
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public static readonly Guid FLEET_TOOL_ID = new Guid("{ED8EB8DA-DFEE-4C42-941C-4F1E997A755E}");
        public static readonly Guid AUTOZONE_TOOL_ID = new Guid("{CE2D229A-E2FE-474E-9C91-6BA5DDC68477}");

        private static Registry registry = null;
        private static Registry registryReadOnly = null;

        /*
         *****************************************************************
         */

        public static Registry Registry
        {
            get
            {
                if (registry == null)
                {
                    registry = new Registry(ConfigurationManager.AppSettings["ConnectionString"], ConfigurationManager.AppSettings["SmtpServer"]);
                }
                return registry;
            }
        }

        public static Registry RegistryReadOnly
        {
            get
            {
                if (registryReadOnly == null)
                {
                    registryReadOnly = new Registry(ConfigurationManager.AppSettings["ConnectionStringReadOnly"], ConfigurationManager.AppSettings["SmtpServer"]);
                }
                return registryReadOnly;
            }
        }


        /// <summary>
        /// Gets the <see cref="string"/> base URL for downloadable content resources.
        /// </summary>
        public static string ResourcesBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ResourcesBaseUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> virtual path for article images.
        /// </summary>
        public static string ArticleImageFileVirtualPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleImageFileVirtualPath"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> virtual path for article documents.
        /// </summary>
        public static string ArticleDocumentFileVirtualPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleDocumentFileVirtualPath"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> virtual path for article media files.
        /// </summary>
        public static string ArticleMediaFileVirtualPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleMediaFileVirtualPath"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> virtual path for article Flash files.
        /// </summary>
        public static string ArticleFlashFileVirtualPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleFlashFileVirtualPath"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> base URL for downloading article video files.
        /// </summary>
        public static string ArticleVideoFileBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleVideoFileBaseUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> base URL for streaming article video files.
        /// </summary>
        public static string ArticleVideoStreamingBaseUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleVideoStreamingBaseUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> virtual path for article video thumbnail images.
        /// </summary>
        public static string ArticleVideoThumbnailVirtualPath
        {
            get
            {
                return ConfigurationManager.AppSettings["ArticleVideoThumbnailVirtualPath"];
            }
        }

        /// <summary>
        /// Gets the no SMTP Server to send email messages from.
        /// </summary>
        public static string SmtpServer
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpServer"];
            }
        }

        /// <summary>
        /// Gets the <see cref="int"/> number of miles an average vehicle is driven on a daily basis.
        /// </summary>
        public static int AverageMilesDrivenPerDay
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["AverageMilesDrivenPerDay"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag that determines whether or not transaction logging is turned on.
        /// </summary>
        public static bool EnableTransactionLogging
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableTransactionLogging"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> base URL for retreiving the TSB PDF files.
        /// </summary>
        public static string TsbRootUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["TsbRootUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> base URL for retreiving the DTC HTML files.
        /// </summary>
        public static string DtcInfoRootUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["DtcInfoRootUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> indicating if Polk data should be used over VinPower data, including which VIN decoder to use.
        /// </summary>
        public static bool UsePolkData
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["UsePolkData"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> base URL for retreiving the Polk vehicle images.
        /// </summary>
        public static string PolkVehicleImageRootUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["PolkVehicleImageRootUrl"];
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not error logging is turned on.
        /// </summary>
        public static bool IsLogError
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsLogError"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="bool"/> flag indicating whether or not error emailing is turned on.
        /// </summary>
        public static bool IsEmailError
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["IsEmailError"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> email address where any application errors are sent to (if enabled in the web.config).
        /// </summary>
        public static string ErrorEmailAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorEmailAddress"];
            }
        }

        public static string IDM_API_URL
        {
            get
            {
                return ConfigurationManager.AppSettings["IDM_API_URL"];
            }
        }

        public static string IDM_API_AUTH_TOKEN
        {
            get
            {
                return ConfigurationManager.AppSettings["IDM_API_AUTH_TOKEN"];
            }
        }

        public static int IDM_API_CONNECTION_TIMEOUT
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["IDM_API_CONNECTION_TIMEOUT"]);
            }
        }

        public static bool ForwardDataToIDMService
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["ForwardDataToIDMService"]);
            }
        }

        /// <summary>
        /// Gets the <see cref="string"/> Innova Payload Web Service URL.
        /// </summary>
        public static string InnovaWebServiceV7Url
        {
            get
            {
                return ConfigurationManager.AppSettings["InnovaWebServiceV7Url"];
            }
        }

        /// <summary>
        /// The virtual root of the application (if any).  For example "/ProjectInsight.WebApp";
        /// </summary>
        public static string Root
        {
            get
            {
                string root = HttpRuntime.AppDomainAppVirtualPath;

                if (root == "/")
                {
                    root = "";
                }

                return root;
            }
        }

        public static string ServiceReadOnly_NewPayloadFolder_When_SendRequest_ToRabbitMQ_Failed
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceReadOnly_NewPayloadFolder_When_SendRequest_ToRabbitMQ_Failed"];
            }
        }

        public static string RabbitMQ_QueueName_DiagnosticReportLogging
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_QueueName_DiagnosticReportLogging"];
            }
        }

        public static string OreillyReportFromBBFolderPath
        {
            get
            {
                return ConfigurationManager.AppSettings["OreillyReportFromBBFolderPath"];
            }
        }

        public static string RabbitMQ_QueueName_OreillyBlackBoxLog
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_QueueName_OreillyBlackBoxLog"];
            }
        }

        public static string RabbitMQ_QueueName_ServiceRO
        {
            get
            {
                return ConfigurationManager.AppSettings["RabbitMQ_QueueName_Default"];
            }
        }

        /// <summary>
        /// The default contructor for the <see cref="Global"/> class.
        /// </summary>
        public Global()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The method called when the application is started.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_Start(Object sender, EventArgs e)
        {
            Hashtable stateLaborRates = new Hashtable();

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(ConfigurationManager.AppSettings["ConnectionString"]))
            {
                dr.ProcedureName = "StateLaborRate_LoadAll";
                dr.Execute();

                while (dr.Read())
                {
                    stateLaborRates.Add(dr.GetString("StateCode"), dr.GetDecimal("DollarsPerHour"));
                }
            }

            this.Application.Lock();
            this.Application.Add("StateLaborRates", stateLaborRates);
            this.Application.UnLock();

            Metafuse3.BusinessObjects.Registry registry = new Metafuse3.BusinessObjects.Registry(ConfigurationManager.AppSettings["ConnectionString"], ConfigurationManager.AppSettings["SmtpServer"]);

            // Create an instance of the Polk VIN decoder so that the static vehicle data gets loaded from the db.
            Innova.Vehicles.PolkVinDecoder polkVinDecoder = new Vehicles.PolkVinDecoder(registry);
        }

        /// <summary>
        /// The method called when the session is started.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Session_Start(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The method called when the application is first requested.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            string root = HttpRuntime.AppDomainAppVirtualPath;

            if (root == "/")
            {
                root = "";
            }
        }

        /// <summary>
        /// The method called when the application's request is ended.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_EndRequest(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The method called when the application's request is authenticated.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The method called when the application encounters an error.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_Error(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The method called when the session is ended.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Session_End(Object sender, EventArgs e)
        {
        }

        /// <summary>
        /// The method called when the application is ended.
        /// </summary>
        /// <param name="sender">The <see cref="Object"/> sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> e.</param>
        protected void Application_End(Object sender, EventArgs e)
        {
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }

        #endregion Web Form Designer generated code
    }
}