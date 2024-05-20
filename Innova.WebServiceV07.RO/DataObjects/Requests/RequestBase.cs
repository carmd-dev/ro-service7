using Innova.WebServiceV07.RO.DataObjects;

namespace Innova.WebServiceV07.RO.Requests
{
    /// <summary>
    /// The base class for requests made to the web service. This class is abstract and must be inherited.
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// The default contructor for the RequestBase class.
        /// </summary>
        public RequestBase()
        {
        }

        /// <summary>
        /// The <see cref="WebServiceKey"/> currently in use.
        /// </summary>
        public WebServiceKey WebServiceKey;

        /// <summary>
        /// The <see cref="string"/> first name of the person making the request.
        /// </summary>
        public string RequesterFirstName;

        /// <summary>
        /// The <see cref="string"/> last name of the person making the request.
        /// </summary>
        public string RequesterLastName;

        /// <summary>
        /// The <see cref="string"/> email address of the person making the request.
        /// </summary>
        public string RequesterEmail;

        /// <summary>
        /// The <see cref="string"/> representation of the ID of the person making the request.
        /// </summary>
        public string RequesterUserId;

        /// <summary>
        /// The <see cref="string"/> phone number of the person making the request.
        /// </summary>
        public string RequesterPhoneNumber;

        /// <summary>
        /// The <see cref="string"/> region (territory or state) of the person making the request. This should be the 2 character code for the state. If invalid, then CA is used.
        /// </summary>
        public string RequesterRegion;

        /// <summary>
        /// A <see cref="PageSpanConfig"/> object containing requirements for page-spanned results.
        /// </summary>
        public PageSpanConfig PageSpanConfig;
    }
}