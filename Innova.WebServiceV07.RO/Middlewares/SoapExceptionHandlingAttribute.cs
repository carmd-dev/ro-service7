using System;
using System.Web.Services.Protocols;

namespace Innova.WebServiceV07.RO
{
    /// <summary>
    /// The SoapExceptionHandlingAttribute class is used to provide information when handling SOAP exceptions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SoapExceptionHandlingAttribute : SoapExtensionAttribute
    {
        /// <summary>
        /// Gets the <see cref="Type"/> extension type.
        /// </summary>
        public override Type ExtensionType
        {
            get
            {
                return typeof(SoapExceptionHandler);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="int"/> priority.
        /// </summary>
        public override int Priority
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
    }
}