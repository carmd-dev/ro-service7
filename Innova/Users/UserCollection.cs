using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Innova.Users
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from UserCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>UserCollection c = new UserCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of UserCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class UserCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new UserCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public UserCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(User);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="User.ExternalSystem"/> properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationExternalSystems
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "ExternalSystem_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(ExternalSystem), "ExternalSystem.Id", "XmlGuidList", call, "ExternalSystemId", true, true);
            }
        }

        #endregion Relation Properties

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="User"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public User this[int index]
        {
            get
            {
                return (User)List[index];
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
        /// Adds an <see cref="User"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="User"/> to add.</param>
        public void Add(User value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="User"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="User"/> to remove.</param>
        public void Remove(User value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="User"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="User"/> to add</param>
        public void Insert(int index, User value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="User"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            User obj = (User)Registry.CreateInstance(typeof(User), dr.GetGuid(idField));

            //if the base properties are set, then set the type of base
            if (isSetProperties)
            {
                obj.LoadPropertiesFromDataReader(dr, true);
            }

            this.Add(obj);

            return obj;
        }

        #endregion Default System Collection Methods

        #region Load Methods

        /// <summary>
        /// Loads the collection with Master Tech users that have been assigned to the specified make.
        /// </summary>
        public void LoadByMasterTechsAssignedToMake(string make)
        {
            SqlProcedureCommand call = new SqlProcedureCommand();
            call.ProcedureName = "User_LoadByIsMasterTechAndMake";
            call.AddNVarChar("Make", make);

            this.Load(call, "UserId", true, true);
        }

        /// <summary>
        /// Gets a <see cref="string"/> comma separated list of values for the property specified from item in this collection.
        /// </summary>
        /// <param name="propertyName"><see cref="string"/> property name to search for.</param>
        /// <returns>A <see cref="string"/> comma separated list of values for the property specified from item in this collection.</returns>
        public string GetCommaSeperatedListOfProperty(string propertyName)
        {
            return GetCommaSeperatedListOfProperty(propertyName, true);
        }

        /// <summary>
        /// Gets a <see cref="string"/> comma separated list of values for the property specified from item in this collection.
        /// </summary>
        /// <param name="propertyName"><see cref="string"/> property name to search for.</param>
        /// <param name="padWithSpace">A <see cref="bool"/> that indicates if the list should be padded with a space after each comma.</param>
        /// <returns>A <see cref="string"/> comma separated list of values for the property specified from item in this collection.</returns>
        public string GetCommaSeperatedListOfProperty(string propertyName, bool padWithSpace)
        {
            StringBuilder commaSeparatedList = new StringBuilder();
            this.AddValuesOfPropertyToStringBuilder(this.Registry, this, propertyName, commaSeparatedList, padWithSpace);

            return commaSeparatedList.ToString();
        }

        /// <summary>
        /// Adds <see cref="string"/> values of the property specified to a <see cref="StringBuilder"/> for members of the list.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> in use to test the object existence.</param>
        /// <param name="collection"><see cref="ICollection"/> collection to examine.</param>
        /// <param name="propertyName"><see cref="string"/> property name to search for.</param>
        /// <param name="propertyValuesList"><see cref="StringBuilder"/> to apply the values to.</param>
        /// <param name="padWithSpace">A <see cref="bool"/> that indicates if the list should be padded with a space after each comma.</param>
        private void AddValuesOfPropertyToStringBuilder(Registry registry, ICollection collection, string propertyName, StringBuilder propertyValuesList, bool padWithSpace)
        {
            if (propertyValuesList == null)
            {
                propertyValuesList = new StringBuilder();
            }

            //split the property name into a string array to loop through them.
            string[] propertyNames = propertyName.Split(".".ToCharArray());

            //if no property names exit
            if (propertyNames.Length == 0)
            {
                return;
            }

            PropertyInfo propertyType;
            object propertyValue;

            foreach (object o in collection)
            {
                propertyValue = o;
                //loop through supplied properties to get to the last property
                for (int i = 0; i < propertyNames.Length; i++)
                {
                    propertyType = propertyValue.GetType().GetProperty(propertyNames[i]);

                    //make sure we get a type, otherwise throw error
                    if (propertyType == null)
                    {
                        throw new ApplicationException("Unable to locate the property \"" + propertyNames[i] + "\" in the list");
                    }

                    propertyValue = propertyType.GetGetMethod().Invoke(propertyValue, BindingFlags.GetProperty, null, new object[0], null);

                    //if the property is null break out of the loop
                    if (propertyValue == null)
                    {
                        break;
                    }
                    else
                    {
                        //if the property is a collection then we need to recursively call the method to retrieve the values of the items in the collection
                        if (propertyValue is ICollection)
                        {
                            string childPropertyName = "";
                            for (int ii = i + 1; ii < propertyNames.Length; ii++)
                            {
                                if (childPropertyName.Length > 0)
                                {
                                    childPropertyName += ".";
                                }
                                childPropertyName += propertyNames[ii];
                            }

                            this.AddValuesOfPropertyToStringBuilder(registry, (ICollection)propertyValue, childPropertyName, propertyValuesList, padWithSpace);
                            break;
                        }
                    }
                }

                //if the property value is a guid then add it to the list
                if (propertyValue != null)
                {
                    if (propertyValuesList.Length > 0)
                    {
                        propertyValuesList.Append(",");

                        if (padWithSpace)
                        {
                            propertyValuesList.Append(" ");
                        }
                    }
                    propertyValuesList.Append(propertyValue.ToString());
                }
            }
        }

        #endregion Load Methods
    }
}