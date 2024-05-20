using Innova.Fixes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections;

namespace Innova.ScheduleMaintenance
{
    /// <summary>
    /// Typed collection for the corresponding business object
    /// (Remove the word "Collection" from ScheduleMaintenancePlanDetailCollection).
    /// Inherits from <see cref="BusinessObjectCollectionBase"/>.
    /// </summary>
    /// <example>
    /// To create the collection:
    ///
    /// <code>ScheduleMaintenancePlanDetailCollection c = new ScheduleMaintenancePlanDetailCollection(Registry);</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of ScheduleMaintenancePlanDetailCollection, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
    /// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class ScheduleMaintenancePlanDetailCollection : BusinessObjectCollectionBase
    {
        #region System Constructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new ScheduleMaintenancePlanDetailCollection object.
        /// </summary>
        /// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
        public ScheduleMaintenancePlanDetailCollection(Registry registry) : base(registry)
        {
            businessObjectBaseType = typeof(ScheduleMaintenancePlanDetail);
        }

        #endregion System Constructors

        #region Relation Properties

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the ScheduleMaintenancePlanDetail.ScheduleMaintenanceService properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationScheduleMaintenanceServices
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "ScheduleMaintenanceService_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(ScheduleMaintenanceService), "ScheduleMaintenanceService.Id", "XmlGuidList", call, "ScheduleMaintenanceServiceId", true, true);
            }
        }

        /// <summary>
        /// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the ScheduleMaintenancePlanDetail.ScheduleMaintenanceService.FixName properties.
        /// </summary>
        public BusinessObjectCollectionRelationDefinition RelationScheduleMaintenanceServiceFixNames
        {
            get
            {
                SqlProcedureCommand call = new SqlProcedureCommand();
                call.ProcedureName = "ScheduleMaintenanceService_LoadByXmlGuidList";
                return new BusinessObjectCollectionRelationDefinition(typeof(FixName), "ScheduleMaintenanceService.FixName.Id", "XmlGuidList", call, "FixNameId", true, true);
            }
        }

        #endregion Relation Properties

        /// <summary>
        /// Relationship populates the fixes that match the supplied inputs associated to the services in the application
        /// </summary>
        /// <param name="manufacturer"></param>
        /// <param name="make"></param>
        /// <param name="model"></param>
        /// <param name="year"></param>
        /// <param name="trimLevel"></param>
        /// <param name="engineType"></param>
        /// <param name="engineVINCode"></param>
        /// <param name="transmission"></param>
        public void RelationPopulateFixes(string make, string model, int? year, string trimLevel, string engineType, string engineVINCode, string transmission)
        {
            //make sure the services are loaded
            this.LoadRelation(this.RelationScheduleMaintenanceServices);
            this.LoadRelation(this.RelationScheduleMaintenanceServiceFixNames);

            Hashtable scheduleMaintenanceServiceFixes = new Hashtable();

            foreach (ScheduleMaintenancePlanDetail detail in this.List)
            {
                if (!scheduleMaintenanceServiceFixes.ContainsKey(detail.ScheduleMaintenanceService.FixName))
                {
                    scheduleMaintenanceServiceFixes.Add(detail.ScheduleMaintenanceService.FixName, detail.ScheduleMaintenanceService);
                }
            }

            string fixNameXmlGuidList = this.GetXmlGuidListFromProperty("ScheduleMaintenanceService.FixName.Id");

            FixCollection fixes = new FixCollection(this.Registry);

            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(this.Registry.ConnectionStringDefault))
            {
                dr.ProcedureName = "Fix_LoadByVehicleInfoAndFixNameXmlGuidList";
                dr.AddNVarChar("FixNameXmlGuidList", fixNameXmlGuidList);
                dr.AddNVarChar("Make", make);
                dr.AddNVarChar("Model", model);
                if (year.HasValue)
                {
                    dr.AddInt32("Year", year);
                }
                dr.AddNVarChar("TrimLevel", trimLevel);
                dr.AddNVarChar("EngineType", engineType);
                dr.AddNVarChar("EngineVINCode", engineVINCode);
                dr.AddNVarChar("Transmission", transmission);
                dr.AddBoolean("IncludeRepairFixes", true);

                dr.Execute();

                while (dr.Read())
                {
                    //get the fix name, and set the fix service of the fix name, but only the first one, that is the best fit
                    FixName fixName = (FixName)this.Registry.CreateInstance(typeof(FixName), dr.GetGuid("FixNameId"));

                    if (fixName.FixService == null)
                    {
                        Fix fix = (Fix)this.Registry.CreateInstance(typeof(Fix), dr.GetGuid("FixId"));
                        fix.LoadPropertiesFromDataReader(dr, true);
                        fixName.FixService = fix;

                        //it shouldn't be in the list, but just in case
                        if (fixes.FindByProperty("Id", fix.Id) == null)
                        {
                            fixes.Add(fix);
                        }
                    }

                    ScheduleMaintenancePlanDetail detail = null;
                    FixName existingFixName = null;

                    detail = (ScheduleMaintenancePlanDetail)this.FindByProperty("ScheduleMaintenanceService.FixName.Description", fixName.Description);
                    if (detail != null)
                    {
                        existingFixName = detail.ScheduleMaintenanceService.FixName;
                    }
                    if (existingFixName != null)
                    {
                        if (scheduleMaintenanceServiceFixes.ContainsKey(existingFixName))
                        {
                            ScheduleMaintenanceService service = (ScheduleMaintenanceService)scheduleMaintenanceServiceFixes[existingFixName];
                            if (service.FixName.FixService == null)
                            {
                                service.FixName = fixName;
                            }
                        }
                    }
                }
            }

            //load the parts on the fixes
            fixes.LoadRelation(fixes.RelationFixParts);
        }

        #region Indexer

        /// <summary>
        /// Indexer, used to return the <see cref="ScheduleMaintenancePlanDetail"/> located at the index (<see cref="int"/>) position of the list.
        /// </summary>
        public ScheduleMaintenancePlanDetail this[int index]
        {
            get
            {
                return (ScheduleMaintenancePlanDetail)List[index];
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
        /// Adds an <see cref="ScheduleMaintenancePlanDetail"/> object to the list.
        /// </summary>
        /// <param name="value"><see cref="ScheduleMaintenancePlanDetail"/> to add.</param>
        public void Add(ScheduleMaintenancePlanDetail value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes an <see cref="ScheduleMaintenancePlanDetail"/> object from the list.
        /// </summary>
        /// <param name="value"><see cref="ScheduleMaintenancePlanDetail"/> to remove.</param>
        public void Remove(ScheduleMaintenancePlanDetail value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts an <see cref="ScheduleMaintenancePlanDetail"/> object to the list.
        /// </summary>
        /// <param name="index"><see cref="int"/> index position inserting at</param>
        /// <param name="value"><see cref="ScheduleMaintenancePlanDetail"/> to add</param>
        public void Insert(int index, ScheduleMaintenancePlanDetail value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Creates the <see cref="ScheduleMaintenancePlanDetail"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
        /// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
        /// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
        /// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
        /// <returns><see cref="BusinessObjectBase"/> created</returns>
        protected override BusinessObjectBase CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
        {
            ScheduleMaintenancePlanDetail obj = (ScheduleMaintenancePlanDetail)Registry.CreateInstance(typeof(ScheduleMaintenancePlanDetail), dr.GetGuid(idField));

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