using System;
using System.Data.SqlClient;

using Metafuse3.NullableTypes;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;


namespace CarMD.Vehicles
{
	/// <summary>
	/// 
	/// Class used to hold the parts for a particular solution.
	/// 
	/// The VehicleTypeSolutionPart object handles the business logic and data access for the specialized business object, VehicleTypeSolutionPart.  
	/// The class inherits from <see cref="BusinessObjectBase"/> class. 
	/// </summary>
	/// <example>
	/// See the examples of how to construct the VehicleTypeSolutionPart object.
	/// 
	/// To create a new instance of a new of VehicleTypeSolutionPart.
	/// <code>VehicleTypeSolutionPart o = (VehicleTypeSolutionPart)Registry.CreateInstance(typeof(VehicleTypeSolutionPart));</code>
	/// 
	/// To create an new instance of an existing VehicleTypeSolutionPart.
	/// <code>VehicleTypeSolutionPart o = (VehicleTypeSolutionPart)Registry.CreateInstance(typeof(VehicleTypeSolutionPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
	/// </example>
	/// <remarks>
	/// In order to create an instance of VehicleTypeSolutionPart, you need to have a <see cref="Registry"/>
	/// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
	/// </remarks>
	[Serializable()]
	public class VehicleTypeSolutionPart : BusinessObjectBase
	{
		// data object variables
		private		VehicleTypeSolution	vehicleTypeSolution;
		private		string				partNumber = "";
		private		string				name = "";
		private		string				description = "";
		private		decimal				price;
		private		int					quantity = 0;
		

		#region System Constructors
		/***********************************************************************************************
		 * 
		 * System Constructors
		 * 
		 * **********************************************************************************************/
	
		/// <summary>
		/// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). VehicleTypeSolutionPart object.  
		/// In order to create a new VehicleTypeSolutionPart which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
		/// </summary>
		/// <example>
		/// <code>
		/// VehicleTypeSolutionPart o = (VehicleTypeSolutionPart)Registry.CreateInstance(typeof(VehicleTypeSolutionPart));
		/// </code>
		/// </example>
		protected internal VehicleTypeSolutionPart() : base()
		{
			IsObjectCreated = true;
			IsObjectLoaded = true;
			IsObjectDirty = true;		
		}
		/// <summary>
		/// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  VehicleTypeSolutionPart object.
		/// In order to create an existing VehicleTypeSolutionPart object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
		/// </summary>
		/// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
		/// <example>
		/// <code>
		/// VehicleTypeSolutionPart o = (VehicleTypeSolutionPart)Registry.CreateInstance(typeof(VehicleTypeSolutionPart), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
		/// </code>
		/// </example>
		protected internal VehicleTypeSolutionPart(Guid id) : base(id)
		{
			this.id = id;
		}

		#endregion


		#region System Properties DO NOT EDIT
		

		// private member variables used to handle the system properties.
		private bool isObjectDirty = false;
		private bool isObjectLoaded = false;
		private bool isObjectCreated = false;
		/*****************************************************************************************
		 * 
		 * System Properties: DO NOT EDIT
		 * 
		 * **************************************************************************************/

		/// <summary>
		/// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been loaded from the database.
		/// The property is marked new so that each layer of the inheritence chain has it's own IsObjectLoaded property.
		/// Base layers may or may not be loaded.  The IsObjectLoaded propery is automatically set to true when the object is loaded from the database.
		/// The IsObjectLoaded property is used primarily for the internal Load methods to determine whether or not the object needs to load itself when a property is accessed or the Load method is invoked.
		/// </summary>
		public new bool IsObjectLoaded
		{
			get
			{
				return isObjectLoaded;
			}
			set
			{
				isObjectLoaded = value;
			}
		}
		/// <summary>
		/// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been updated and needs to be saved to the database.
		/// The property is marked new so that each layer of the inheritence chain has it's own IsObjectDirty property.
		/// Base layers may or may not be dirty.  The IsObjectDirty flag should set to true when a property is updated, and the object automatically sets the IsObjectDirty flag to false when the object is saved successfully.
		/// The IsObjectDirty property is used primarly for the internal Save methods to determine whether or not the object needs to save itself when the Save method is invoked.
		/// </summary>
		public new bool IsObjectDirty
		{
			get
			{
				return isObjectDirty;
			}
			set
			{
				isObjectDirty = value;
				
				if(!isObjectDirty)
				{
					isObjectCreated = false;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has just been created (is new) and will need to be saved to the database.
		/// The property is marked new so that each layer of the inheritence chain has it's own IsObjectCreated property.  
		/// The IsObjectCreated flag is automatically set to false when the object is saved.
		/// Base layers may or may not be created.  The IsObjectCreated flag is set to false when saved.
		/// </summary>
		public new bool IsObjectCreated
		{
			get
			{
				return isObjectCreated;
			}
			set
			{
				isObjectCreated = value;
			}
		}

	
		#endregion


		#region Object Properties
		/**************************************************************************************
		 * 
		 * Object Properties: Add Custom Fields
		 * 
		 * **************************************************************************************/

		/// <summary>
		/// Gets or sets the <see cref="VehicleTypeSolution"/> solution to which this part belongs.
		/// </summary>
		public VehicleTypeSolution VehicleTypeSolution
		{
			get
			{
				EnsureLoaded();
				return vehicleTypeSolution;
			}
			set
			{	
				EnsureLoaded();
				if(vehicleTypeSolution != value)
				{
					IsObjectDirty = true;
					vehicleTypeSolution = value;
				}
			}
		}


		/// <summary>
		/// Gets or sets the <see cref="string"/> part number of the vehicle part.
		/// </summary>
		public string PartNumber
		{
			get
			{
				EnsureLoaded();
				return partNumber;
			}
			set
			{	
				EnsureLoaded();
				if(partNumber != value)
				{
					IsObjectDirty = true;
					partNumber = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="string"/> name of the vehicle part.
		/// </summary>
		public string Name
		{
			get
			{
				EnsureLoaded();
				return name;
			}
			set
			{	
				EnsureLoaded();
				if(name != value)
				{
					IsObjectDirty = true;
					name = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="string"/> description of the vehicle part.
		/// </summary>
		public string Description
		{
			get
			{
				EnsureLoaded();
				return description;
			}
			set
			{	
				EnsureLoaded();
				if(description != value)
				{
					IsObjectDirty = true;
					description = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="decimal"/> price of the vehicle part.
		/// </summary>
		public decimal Price
		{
			get
			{
				EnsureLoaded();
				return price;
			}
			set
			{	
				EnsureLoaded();
				if(price != value)
				{
					IsObjectDirty = true;
					price = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="decimal"/> price of the vehicle part.
		/// </summary>
		public int Quantity
		{
			get
			{
				EnsureLoaded();
				return quantity;
			}
			set
			{	
				EnsureLoaded();
				if(quantity != value)
				{
					IsObjectDirty = true;
					quantity = value;
				}
			}
		}

		#endregion


		#region Object Properties (Related Objects)
		/*****************************************************************************************
		 * 
		 * Object Relationships: Add correlated Object Collections 
		 * 
		*******************************************************************************************/		

		#endregion


		#region Business Logic Methods

		/***********************************************************************************************
		 * 
		 * Custom Business Logic
		 * 
		 * **********************************************************************************************/
	
		
		
		/* Example of adding related vehicleTypeParts.
		// business logic add vehicleTypePart child.
		public void AddVehicleTypePartChild(VehicleTypePartChild vehicleTypePartChild)
		{
			vehicleTypePartChild.VehicleTypeSolutionPart = this;
			this.VehicleTypePartChilds.Add(vehicleTypePartChild);
		}
		// business logic remove vehicleTypePart child
		public void RemoveVehicleTypePartChild(VehicleTypePartChild vehicleTypePartChild)
		{
			this.VehicleTypePartChilds.Remove(vehicleTypePartChild);
		}

		// business logic add vehicleTypePart child.
		public void AddOther(Other other)
		{
			this.Others.Add(other);
		}
		// business logic remove vehicleTypePart child
		public void RemoveOther(Other other)
		{
			this.Others.Remove(other);
		}
		*/
		

		#endregion


		#region Required Object Methods (Load, Save, Delete, Etc)

		/***********************************************************************************************
		 * 
		 * Required Object Methods (Load, Save, Save Collections, Delete)  
		 * 
		 * **********************************************************************************************/
		// Edit Required Object Methods 
		
		#region System Load Calls (DO NOT EDIT)

		/// <summary>
		/// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false. 
		/// </summary>	
		public new void Load()
		{
			Load(null, null, false);
		}

		/// <summary>
		/// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.  
		/// </summary>
		/// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
		/// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
		/// <returns><see cref="SqlConnection"/> supplied or the one created internally.</returns>
		public new SqlConnection Load(SqlConnection connection, bool isLoadBase)
		{
			Load(connection, null, isLoadBase);

			return connection;
		}
 
		/// <summary>
		/// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.  
		/// </summary>
		/// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
		/// <param name="transaction"><see cref="SqlTransaction"/> currently being used (if any), otherwise if set to null the command will be executed outside the contect of a current transaction.</param>
		/// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
		/// <returns><see cref="SqlTransaction"/> currently being used (if any) to access the database.  If none, returns null.</returns>	
		public new SqlTransaction Load(SqlConnection connection, SqlTransaction transaction, bool isLoadBase)
		{
			EnsureValidId();

			if(isLoadBase)
			{
				//load the base vehicleTypePart if user selected it.
				transaction = base.Load(connection, transaction, isLoadBase);
			}



			if(!IsObjectLoaded)
			{

				SqlDataReaderWrapper dr;
				if(connection == null)
				{
					dr = new SqlDataReaderWrapper(ConnectionString);
				}
				else
				{
					dr = new SqlDataReaderWrapper(connection, false);
				}

				using(dr)
				{

		
					SetLoadProcedureCall(dr);
			
					if(transaction == null)
					{
						dr.Execute();
					}
					else
					{
						dr.Execute(transaction);	
					}

					if(dr.Read())
					{
						LoadPropertiesFromDataReader(dr, isLoadBase);
					}
					else
					{
			
						throw(new ApplicationException("Load Failed for type " + this.GetType().ToString() + " using Procedure: " + dr.ProcedureCall));
					}
				}
			}

			return transaction;

		}

		/// <summary>
		/// Loads all the properties of this object from <see cref="SqlDataReaderWrapper"/> supplied.
		/// This method calls the protected internal method <see cref="SetPropertiesFromDataReader"/>.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> recordset containing the fields required to set the properties of this object.</param>
		/// <param name="isSetBase"><see cref="bool"/> determines whether or not to load base layers (if any) of the object.  Set to true if the recordset contains the fields necessary to load the properties of base layers of this object.</param>
		public new void LoadPropertiesFromDataReader(SqlDataReaderWrapper dr, bool isSetBase)
		{
			if(isSetBase)
			{
				base.LoadPropertiesFromDataReader(dr, isSetBase);
			}
			
			if(!IsObjectLoaded)
			{
				SetPropertiesFromDataReader(dr);
			}
		
			IsObjectLoaded = true;
		}

		/// <summary>
		/// Method ensures the object is loaded.  This method is located in the get portion of the a property representing data in the database and is called there.  If the object's <see cref="IsObjectLoaded"/> property is false and the <see cref="IsObjectCreated"/> property is false, then the <see cref="Load"/> method is invoked.
		/// </summary>
		protected new void EnsureLoaded()
		{
			if(!IsObjectLoaded && !IsObjectCreated)
			{
				Load();
			}
		}


		#endregion


		/// <summary>
		/// Sets the base load procedure call and parameters to the supplied <see cref="SqlDataReaderWrapper"/>, to be executed.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> to set the procedure call and parameters to.</param>	
		protected new void SetLoadProcedureCall(SqlDataReaderWrapper dr)
		{
			dr.ProcedureName = "VehicleTypeSolutionPart_Load";
			dr.AddGuid("VehicleTypeSolutionPartId", Id);
		}

		/// <summary>
		/// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>	
		protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
		{
			vehicleTypeSolution	= (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution), dr.GetGuid("VehicleTypeSolutionId"));
			partNumber			= dr.GetString("PartNumber");
			name				= dr.GetString("Name");
			description			= dr.GetString("Description");
			price				= dr.GetDecimal("Price");
			quantity			= dr.GetInt32("Quantity");

			IsObjectLoaded = true;
		}
		
	
		/// <summary>
		/// Saves the current object, all base layers, and all related collections (calls <see cref="SaveCollections"/>).
		/// </summary>
		/// <param name="connection"><see cref="SqlConnection"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
		/// <param name="transaction"><see cref="SqlTransaction"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
		/// <returns><see cref="SqlTransaction"/> currently being used.</returns>	
		public override SqlTransaction Save(SqlConnection connection, SqlTransaction transaction)
		{

			// Call base save method of base class.
			transaction = base.Save(connection, transaction);

			//Custom save business logic here. Modify procedure name.
			if(IsObjectDirty)
			{
				transaction = EnsureDatabasePrepared(connection, transaction);

				using(SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
				{
					if(IsObjectCreated)
					{
						dr.ProcedureName = "VehicleTypeSolutionPart_Create";
					}
					else
					{
						dr.ProcedureName = "VehicleTypeSolutionPart_Save";
					}

					dr.AddGuid("VehicleTypeSolutionPartId", Id);
					dr.AddGuid("VehicleTypeSolutionId", vehicleTypeSolution.Id);
					dr.AddNVarChar("PartNumber", PartNumber);
					dr.AddNVarChar("Name", Name);
					dr.AddNText("Description", Description);
					dr.AddDecimal("Price", Price);
					dr.AddInt32("Quantity", Quantity);

					dr.Execute(transaction);
				}

				IsObjectDirty = false;
			}

			// call the save collections method
			transaction = SaveCollections(connection, transaction);
		
			return transaction;
		}

		
		/// <summary>
		/// Saves the related collections (normally set as properties) that the object needs to save to maintain integrity.
		/// </summary>
		/// <param name="connection"><see cref="SqlConnection"/> currently being used.</param>
		/// <param name="transaction"><see cref="SqlTransaction"/> currently being used.</param>
		/// <returns><see cref="SqlTransaction"/> currently being used.</returns>
		protected new SqlTransaction SaveCollections(SqlConnection connection, SqlTransaction transaction)
		{
			return transaction;
		}

	
		/// <summary>
		/// Deletes the object, base layers, and related collections necessary to delete the object. 
		/// </summary>
		/// <param name="connection"><see cref="SqlConnection"/> currently being used, if null a <see cref="SqlConnection"/> is created.</param>
		/// <param name="transaction"><see cref="SqlTransaction"/> currently being used, if null a <see cref="SqlTransaction"/> is created.</param>
		/// <returns><see cref="SqlTransaction"/> currently being used.</returns>
		public override SqlTransaction Delete(SqlConnection connection, SqlTransaction transaction)
		{
			EnsureValidId();

			transaction = EnsureDatabasePrepared(connection, transaction);

			// delete this current object
			using(SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
			{
				//delete the vehicleTypePart
				dr.ProcedureName = "VehicleTypeSolutionPart_Delete";
				dr.AddGuid("VehicleTypeSolutionPartId", Id);

				dr.ExecuteNonQuery(transaction);
			}
			
			transaction = base.Delete(connection, transaction);

			return transaction;
		}

		#endregion


	}
}
