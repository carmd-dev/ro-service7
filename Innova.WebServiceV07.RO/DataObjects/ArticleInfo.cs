using System;

using Innova.Articles;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class that contains data for an article.
    /// </summary>
    public class ArticleInfo
    {
        /// <summary>
        /// Default constructor for the article info class
        /// </summary>
        public ArticleInfo()
        {
        }

        /// <summary>
        /// The <see cref="Guid"/> ID of the article.
        /// </summary>
        public Guid ArticleId;

        /// <summary>
        /// The <see cref="string"/> name of the user who created the article.
        /// </summary>
        public string AdminUserNameCreated;

        /// <summary>
        /// The <see cref="string"/> name of the user who last updated the article.
        /// </summary>
        public string AdminUserNameUpdated;

        /// <summary>
        /// The <see cref="Guid"/> ID of the primary category for the article.
        /// </summary>
        public Guid PrimaryArticleCategoryId;

        /// <summary>
        /// The <see cref="string"/> name of the primary category for the article.
        /// </summary>
        public string PrimaryArticleCategoryName;

        /// <summary>
        /// The <see cref="string"/> name article type.
        /// </summary>
        public string ArticleTypeName;

        /// <summary>
        /// The <see cref="string"/> author of the article.
        /// </summary>
        public string Author;

        /// <summary>
        /// The <see cref="string"/> body of the article.
        /// </summary>
        public string Body;

        /// <summary>
        /// The <see cref="string"/> representation of the date the article was created in UTC.
        /// </summary>
        public string CreatedDateTimeUTCString;

        /// <summary>
        /// The <see cref="string"/> representation of the publication date of the article.
        /// </summary>
        public string DateString;

        /// <summary>
        /// The <see cref="string"/> representation of the date the article should expire.
        /// </summary>
        public string EndDateString;

        /// <summary>
        /// The <see cref="bool"/> indicating if the article is active.
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// The <see cref="bool"/> indicating if the article is free.
        /// </summary>
        public bool IsFree;

        /// <summary>
        /// The <see cref="string"/> keywords of the article.
        /// </summary>
        public string Keywords;

        /// <summary>
        /// The <see cref="string"/> representation of the date the article should be available for viewing.
        /// </summary>
        public string StartDateString;

        /// <summary>
        /// The <see cref="string"/> summary of the article.
        /// </summary>
        public string Summary;

        /// <summary>
        /// The <see cref="string"/> title of the article.
        /// </summary>
        public string Title;

        /// <summary>
        /// The <see cref="string"/> representation of the date the article was last updated in UTC.
        /// </summary>
        public string UpdatedDateTimeUTCString;

        /// <summary>
        /// The <see cref="int"/> duration of the video in seconds.
        /// </summary>
        public int VideoDurationSeconds;

        /// <summary>
        /// The <see cref="int"/> height of the video.
        /// </summary>
        public int VideoHeight;

        /// <summary>
        /// The <see cref="string"/> URL to the video thumbnail image.
        /// </summary>
        public string VideoThumbnailUrl;

        /// <summary>
        /// The <see cref="string"/> download URL to the video file associated to this article.
        /// </summary>
        public string VideoDownloadUrl;

        /// <summary>
        /// The <see cref="string"/> streaming URL to the video file associated to this article.
        /// </summary>
        public string VideoStreamingUrl;

        /// <summary>
        /// The <see cref="int"/> width of the video.
        /// </summary>
        public int VideoWidth;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Article"/> object to create the object from.</param>
        /// <returns><see cref="ArticleInfo"/> object created from the supplied SDK object.</returns>
        protected internal static ArticleInfo GetWebServiceObject(Article sdkObject)
        {
            ArticleInfo wsObject = new ArticleInfo();

            // Fully qualify any hyperlinked resource URLs
            string articleBody = sdkObject.Body;
            articleBody = articleBody.Replace(Global.ArticleImageFileVirtualPath, Global.ResourcesBaseUrl + Global.ArticleImageFileVirtualPath);
            articleBody = articleBody.Replace(Global.ArticleDocumentFileVirtualPath, Global.ResourcesBaseUrl + Global.ArticleDocumentFileVirtualPath);
            articleBody = articleBody.Replace(Global.ArticleMediaFileVirtualPath, Global.ResourcesBaseUrl + Global.ArticleMediaFileVirtualPath);
            articleBody = articleBody.Replace(Global.ArticleFlashFileVirtualPath, Global.ResourcesBaseUrl + Global.ArticleFlashFileVirtualPath);

            wsObject.ArticleId = sdkObject.Id;
            wsObject.AdminUserNameCreated = sdkObject.AdminUserCreated.Name;
            wsObject.AdminUserNameUpdated = sdkObject.AdminUserUpdated.Name;
            wsObject.PrimaryArticleCategoryId = sdkObject.ArticleCategoryPrimary.Id;
            wsObject.PrimaryArticleCategoryName = sdkObject.ArticleCategoryPrimary.Name;
            wsObject.ArticleTypeName = sdkObject.ArticleType.ToString();
            wsObject.Author = sdkObject.Author;
            wsObject.Body = articleBody;
            wsObject.CreatedDateTimeUTCString = sdkObject.CreatedDateTimeUTC.ToString();
            wsObject.DateString = sdkObject.Date.ToString();
            wsObject.EndDateString = sdkObject.EndDate.ToString();
            wsObject.IsActive = sdkObject.IsActive;
            wsObject.IsFree = sdkObject.IsFree;
            wsObject.Keywords = sdkObject.Keywords;
            wsObject.StartDateString = sdkObject.StartDate.ToString();
            wsObject.Summary = sdkObject.Summary;
            wsObject.Title = sdkObject.Title;
            wsObject.UpdatedDateTimeUTCString = sdkObject.UpdatedDateTimeUTC.ToString();
            wsObject.VideoDurationSeconds = sdkObject.VideoDurationSeconds;
            wsObject.VideoHeight = sdkObject.VideoHeight;
            wsObject.VideoThumbnailUrl = Global.ResourcesBaseUrl + Global.ArticleVideoThumbnailVirtualPath + sdkObject.VideoThumbnailUrl;
            wsObject.VideoDownloadUrl = sdkObject.GetFullyQualifiedVideoURL(Global.ArticleVideoFileBaseUrl);
            wsObject.VideoStreamingUrl = sdkObject.GetFullyQualifiedVideoURL(Global.ArticleVideoStreamingBaseUrl);
            wsObject.VideoWidth = sdkObject.VideoWidth;

            return wsObject;
        }
    }
}