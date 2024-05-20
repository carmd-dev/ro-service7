using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;

namespace Innova.Articles
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from ArticleCategoryCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>ArticleCategoryCollection c = new ArticleCategoryCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ArticleCategoryCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class ArticleCategoryCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new ArticleCategoryCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public ArticleCategoryCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(ArticleCategory);
        }

        #endregion System Constructors



        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="ArticleCategory"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public ArticleCategory this[int index]
        {
            get
            {
                return (ArticleCategory)List[index];
            }
        }

        #endregion Indexer

        #region Default System Collection Methods

        /*****************************************************************************************
		 *
		 * System Methods
		 *
		 * **************************************************************************************/

        /// <summary>
        /// Adds an <see cref="ArticleCategory"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="ArticleCategory"/> to add.</param>
        public void Add(ArticleCategory value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="ArticleCategory"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="ArticleCategory"/> to remove.</param>
        public void Remove(ArticleCategory value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="ArticleCategory"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="ArticleCategory"/> to add</param>
        public void Insert(int index, ArticleCategory value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="ArticleCategory"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            ArticleCategory obj = (ArticleCategory)Registry.CreateInstance(typeof(ArticleCategory), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, isSetPropertiesOfBase);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods
    }
}