namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// Object holds the page span statistics information.
    /// </summary>
    public class PageSpanInfo
    {
        /// <summary>
        /// The <see cref="int"/> current page number
        /// </summary>
        public int CurrentPageNumber;

        /// <summary>
        /// Gets the <see cref="int"/> next page number.
        /// </summary>
        public int MaxRecords;

        /// <summary>
        /// Gets the <see cref="int"/> next page number.
        /// </summary>
        public int NextPageNumber;

        /// <summary>
        /// Gets the <see cref="int"/> next page record count, the number of records on the next page.
        /// </summary>
        public int NextPageRecordCount;

        /// <summary>
        /// Gets the <see cref="int"/> total page count.
        /// </summary>
        public int PageCount;

        /// <summary>
        /// Gets the <see cref="int"/> page size.
        /// </summary>
        public int PageSize;

        /// <summary>
        /// Gets the <see cref="int"/> previous page number. If <see cref="CurrentPageNumber"/> is 0, this will be 0.
        /// </summary>
        /// <remarks>
        /// If <see cref="CurrentPageNumber"/> is 0, this will be 0.
        /// </remarks>
        public int PreviousPageNumber;

        /// <summary>
        /// Gets the <see cref="int"/> number of records on the previous page. If <see cref="CurrentPageNumber"/> is 1 or 0, this will be 0.
        /// </summary>
        /// <remarks>
        /// If <see cref="CurrentPageNumber"/> is 1 or 0, this will be 0.
        /// </remarks>
        public int PreviousPageRecordCount;

        /// <summary>
        /// Gets the <see cref="int"/> total record count. This number will always be a whole number. If there are no records, this will be 0.
        /// </summary>
        /// <remarks>
        /// This number will always be a whole number.
        /// If there are no records, this will be 0.
        /// </remarks>
        public int RecordCount;

        /// <summary>
        /// Gets the <see cref="int"/> record end. RecordEnd indicates the ones-based number for the current page's last
        /// record relative to the entire data set.  If <see cref="RecordCount"/> is 0, this will be 0.
        /// </summary>
        /// <remarks>
        /// If <see cref="RecordCount"/> is 0, this will be 0.
        /// </remarks>
        public int RecordEnd;

        /// <summary>
        /// Gets the <see cref="int"/> record start. RecordStart indicates the ones-based number for the current page's first
        /// record relative to the entire data set. If <see cref="RecordCount"/> is 0, this will be 0.
        /// </summary>
        /// <remarks>
        /// If <see cref="RecordCount"/> is 0, this will be 0.
        /// </remarks>
        public int RecordStart;
    }
}