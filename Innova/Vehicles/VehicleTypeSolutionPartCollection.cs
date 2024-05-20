using System;
using System.Data.SqlClient;

using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;


namespace CarMD.Vehicles
{
	/// <summary>
	/// Typed collection for the corresponding business object 
	/// (Remove the word "Collection" from VehicleTypeSolutionPartCollection).
	/// Inherits from <see cref="BusinessObjectCollectionBase"/>.
	/// </summary>
	/// <example>
	/// To create the collection:
	/// 
	/// <code>VehicleTypeSolutionPartCollection c = new VehicleTypeSolutionPartCollection(Registry);</code>
	/// </example>
	/// <remarks>
	/// In order to create an instance of VehicleTypeSolutionPartCollection, you need to have a <see cref="Registry"/>
	/// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
	/// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
	/// </remarks>
	[Serializable()]
	public class VehicleTypeSolutionPartCollection : BusinessObjectCollectionBase
	{


		//TODO Update the indexer, add, insert, remove methods, and the load methods with the specific type implementation

		#region System Constructors
		/***********************************************************************************************
		 * 
		 * System Constructors
		 * 
		 * **********************************************************************************************/

		/// <summary>
		/// Constructor creates a new VehicleTypeSolutionPartCollection object. 
		/// </summary>
		/// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
		public VehicleTypeSolutionPartCollection(Registry registry) : base(registry)
		{
			businessObjectBaseType = typeof(VehicleTypeSolutionPart);
		}

		#endregion

		
		#region Relation Properties


		#endregion
		
		
		#region Indexer


		/// <summary>
		/// Indexer, used to return the <see cref="VehicleTypeSolutionPart"/> located at the index (<see cref="int"/>) position of the list.
		/// </summary>
		public VehicleTypeSolutionPart this[int index]
		{
			get
			{
				return (VehicleTypeSolutionPart)List[index];
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
		/// Adds an <see cref="VehicleTypeSolutionPart"/> object to the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeSolutionPart"/> to add.</param>
		public void Add(VehicleTypeSolutionPart value)
		{
			base.Add(value);
		}
		/// <summary>
		/// Removes an <see cref="VehicleTypeSolutionPart"/> object from the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeSolutionPart"/> to remove.</param>
		public void Remove(VehicleTypeSolutionPart value)
		{
			base.Remove(value);
		}

		/// <summary>
		/// Inserts an <see cref="VehicleTypeSolutionPart"/> object to the list.
		/// </summary>
		/// <param name="index"><see cref="int"/> index position inserting at</param>
		/// <param name="value"><see cref="VehicleTypeSolutionPart"/> to add</param>
		public void Insert(int index, VehicleTypeSolutionPart value)
		{
			base.Insert(index, value);
		}

		/// <summary>
		/// Creates the <see cref="VehicleTypeSolutionPart"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
		/// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
		/// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
		/// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
		protected override void CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
		{

			VehicleTypeSolutionPart obj = (VehicleTypeSolutionPart)Registry.CreateInstance(typeof(VehicleTypeSolutionPart), dr.GetGuid(idField));

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
