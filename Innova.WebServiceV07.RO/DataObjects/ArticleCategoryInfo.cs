using Innova.Articles;
using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The class that contains data for an article category.
    /// </summary>
    public class ArticleCategoryInfo
    {
        /// <summary>
        /// Default constructor for the article category info class
        /// </summary>
        public ArticleCategoryInfo()
        {
        }

        /// <summary>
        /// The <see cref="Guid"/> ID of the category.
        /// </summary>
        public Guid ArticleCategoryId;

        /// <summary>
        /// The <see cref="string"/> ID of the parent category.
        /// </summary>
        public Guid? ParentArticleCategoryId;

        /// <summary>
        /// The <see cref="string"/> ID of the parent category.
        /// </summary>
        public string ParentArticleCategoryName;

        /// <summary>
        /// The <see cref="string"/> name of the category.
        /// </summary>
        public string Name;

        /// <summary>
        /// The <see cref="ArticleCategoryInfo"/> array of all immediate child categories.
        /// </summary>
        public ArticleCategoryInfo[] ChildArticleCategories;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Article"/> object to create the object from.</param>
        /// <returns><see cref="ArticleInfo"/> object created from the supplied SDK object.</returns>
        protected internal static ArticleCategoryInfo GetWebServiceObject(ArticleCategory sdkObject)
        {
            ArticleCategoryInfo wsObject = new ArticleCategoryInfo();

            wsObject.ArticleCategoryId = sdkObject.Id;
            wsObject.Name = sdkObject.Name;
            if (sdkObject.ArticleCategoryContainer != null)
            {
                wsObject.ParentArticleCategoryId = sdkObject.ArticleCategoryContainer.Id;
                wsObject.ParentArticleCategoryName = sdkObject.ArticleCategoryContainer.Name;
            }

            Articles.ArticleCategoryCollection cats = ArticleCategory.GetSubCategories(sdkObject.Registry, sdkObject, true);
            wsObject.ChildArticleCategories = new ArticleCategoryInfo[cats.Count];
            for (int i = 0; i < cats.Count; i++)
            {
                ArticleCategory ac = cats[i];
                wsObject.ChildArticleCategories[i] = GetWebServiceObject(ac);
            }

            return wsObject;
        }
    }
}