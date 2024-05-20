namespace Innova.WebServiceV07.RO.DataObjects
{
    public class ArticleInfoPageSpanned
    {
        /// <summary>
        /// The <see cref="PageSpanInfo"/> object which contains the page span data for the search query
        /// </summary>
        public PageSpanInfo PageSpanInfo = null;

        /// <summary>
        /// The array of <see cref="ArticleInfo"/> objects returned by the page span search
        /// </summary>
        public ArticleInfo[] TSBInfos = null;
    }
}