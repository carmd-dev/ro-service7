namespace Innova.WebServiceV07.RO.Requests
{
    /// <summary>
    /// The PageSpanConfig class holds the inputs necessary for a request that sends back page spanned results.
    /// </summary>
    public class PageSpanConfig
    {
        /// <summary>
        /// The default contructor for the PageSpanConfig class.
        /// </summary>
        public PageSpanConfig()
        {
        }

        /// <summary>
        /// <param name="PageSize">The <see cref="int"/> number of results per page.</param>
        /// </summary>
        public int PageSize;

        /// <summary>
        /// <param name="CurrentPage">The <see cref="int"/> page of results being requested.</param>
        /// </summary>
        public int CurrentPage;
    }
}