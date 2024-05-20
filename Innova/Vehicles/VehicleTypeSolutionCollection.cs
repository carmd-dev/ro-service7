using System;
using System.Data.SqlClient;

using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;


namespace CarMD.Vehicles
{
	/// <summary>
	/// Typed collection for the corresponding business object 
	/// (Remove the word "Collection" from VehicleTypeSolutionCollection).
	/// Inherits from <see cref="BusinessObjectCollectionBase"/>.
	/// </summary>
	/// <example>
	/// To create the collection:
	/// 
	/// <code>VehicleTypeSolutionCollection c = new VehicleTypeSolutionCollection(Registry);</code>
	/// </example>
	/// <remarks>
	/// In order to create an instance of VehicleTypeSolutionCollection, you need to have a <see cref="Registry"/>
	/// object created, and have set the static member <see cref="BusinessObjectCollectionBase.ConnectionString"/> to the
	/// <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
	/// </remarks>
	[Serializable()]
	public class VehicleTypeSolutionCollection : BusinessObjectCollectionBase
	{


		
		#region System Constructors
		/***********************************************************************************************
		 * 
		 * System Constructors
		 * 
		 * **********************************************************************************************/

		/// <summary>
		/// Constructor creates a new VehicleTypeSolutionCollection object. 
		/// </summary>
		/// <param name="registry"><see cref="Registry"/> currently being used to store <see cref="BusinessObjectBase"/> objects.</param>
		public VehicleTypeSolutionCollection(Registry registry) : base(registry)
		{
			businessObjectBaseType = typeof(VehicleTypeSolution);
		}

		#endregion

		
		#region Relation Properties


		#endregion
		
		
		#region Indexer


		/// <summary>
		/// Indexer, used to return the <see cref="VehicleTypeSolution"/> located at the index (<see cref="int"/>) position of the list.
		/// </summary>
		public VehicleTypeSolution this[int index]
		{
			get
			{
				return (VehicleTypeSolution)List[index];
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
		/// Adds an <see cref="VehicleTypeSolution"/> object to the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeSolution"/> to add.</param>
		public void Add(VehicleTypeSolution value)
		{
			base.Add(value);
		}
		/// <summary>
		/// Removes an <see cref="VehicleTypeSolution"/> object from the list.
		/// </summary>
		/// <param name="value"><see cref="VehicleTypeSolution"/> to remove.</param>
		public void Remove(VehicleTypeSolution value)
		{
			base.Remove(value);
		}

		/// <summary>
		/// Inserts an <see cref="VehicleTypeSolution"/> object to the list.
		/// </summary>
		/// <param name="index"><see cref="int"/> index position inserting at</param>
		/// <param name="value"><see cref="VehicleTypeSolution"/> to add</param>
		public void Insert(int index, VehicleTypeSolution value)
		{
			base.Insert(index, value);
		}

		/// <summary>
		/// Creates the <see cref="VehicleTypeSolution"/> object from the specified recordset supplied and optionally sets the properties of the object, then adds it to the list.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> used.</param>
		/// <param name="idField"><see cref="string"/> The field name of the Guid that represents the objectId / primary key for the object.</param>
		/// <param name="isSetProperties"><see cref="bool"/> flag indicating whether or not to set the properties of the object.</param>
		/// <param name="isSetPropertiesOfBase"><see cref="bool"/> flag indicating whether or not to set the properties of bases of the object (if any).</param>
		protected override void CreateBusinessObjectAndAddToListFromRecordset(SqlDataReaderWrapper dr, string idField, bool isSetProperties, bool isSetPropertiesOfBase)
		{

			VehicleTypeSolution obj = (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution), dr.GetGuid(idField));

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
