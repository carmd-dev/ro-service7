using System;
using System.Data.SqlClient;

using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;


namespace CarMD.Vehicles
{
	/// <summary>
	/// Typed collection for the corresponding business object 
	/// (Remove the word "Collection" from VehicleTypeCodeSolutionCollection).
	/// Inherits from <see cref="BusinessObjectCollectionBase"/>.
	/// </summary>
	/// <example>
	/// To create the collection:
	/// 
	/// <code>VehicleTypeCodeSolutionCollection c = new VehicleTypeCodeSolutionCollection(Registry);</code>
	/// </example>
	/// <remarks>
	/// In order to create an instance of VehicleTypeCodeSolutionCollection, you need to have a <see cref="Registry"/>
	/// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
	/// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
	/// </remarks>
	[Serializable()]
	public class VehicleTypeCodeSolutionCollection : BusinessObjectCollectionBase
	{
		
		#region System Constructors
		/***********************************************************************************************
		 * 
		 * System Constructors
		 * 
		 * **********************************************************************************************/

		/// <summary>
		/// Constructor creates a new VehicleTypeCodeSolutionCollection object. 
		/// </summary>
		/// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
		public VehicleTypeCodeSolutionCollection(Registry registry) : base(registry)
		{
			businessObjectBaseType = typeof(VehicleTypeCodeSolution);
		}

		#endregion

		
		#region Relation Properties

		/// <summary>
		/// Gets the <see cref="BusinessObjectCollectionRelationDefinition"/> for loading the <see cref="VehicleTypeCodeSolution.VehicleTypeSolution"/> properties.
		/// </summary>
		public BusinessObjectCollectionRelationDefinition RelationVehicleTypeSolutions
		{
			get
			{
				SqlProcedureCommand call = new SqlProcedureCommand();
				call.ProcedureName = "VehicleTypeSolution_LoadByVehicleTypeCodeSolutionXmlGuidList";
				return new BusinessObjectCollectionRelationDefinition(typeof(VehicleTypeSolution), "VehicleTypeSolution.Id", "XmlGuidList", call, "VehicleTypeSolutionId", true);
			}
		}

		#endregion
		
		
		#region Indexer


		/// <summary>
		/// Indexer, used to return the <see cref="VehicleTypeCodeSolution"/> located at the index (<see cref="int"/>) position of the list.
		/// </summary>
		public VehicleTypeCodeSolution this[int index]
		{
			get
			{
				return (VehicleTypeCodeSolution)List[index];
			}
		}
		
		

		#endregion


		#region Default System Collection Methods
		/*****************************************************************************************
		 * 
		 * System Methods
		 * 
		 * **************************************************************************************/
		
		/// <summary>
		/// Adds an <see cref="VehicleTypeCodeSolution"/> object to the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeCodeSolution"/> to add.</param>
		public void Add(VehicleTypeCodeSolution value)
		{
			base.Add(value);
		}
		/// <summary>
		/// Removes an <see cref="VehicleTypeCodeSolution"/> object from the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeCodeSolution"/> to remove.</param>
		public void Remove(VehicleTypeCodeSolution value)
		{
			base.Remove(value);
		}

		/// <summary>
		/// Inserts an <see cref="VehicleTypeCodeSolution"/> object to the list.
		/// </summary>
		/// <param name="index"><see cref="int"/> index position inserting at</param>
		/// <param name="value"><see cref="VehicleTypeCodeSolution"/> to add</param>
		public void Insert(int index, VehicleTypeCodeSolution value)
		{
			base.Insert(index, value);
		}

		/// <summary>
		/// Creates the <see cref="VehicleTypeCodeSolution"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
		/// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
		/// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
		/// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
		protected override void CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
		{

			VehicleTypeCodeSolution obj = (VehicleTypeCodeSolution)Registry.CreateInstance(typeof(VehicleTypeCodeSolution), dr.GetGuid(idField));

			//if the base properties are set, then set the type of base
			if(isSetProperties)
			{
				obj.LoadPropertiesFromDataReader(dr, true);
			}
			
			this.Add(obj);
		}


		#endregion


	}
}
