using System;
using System.Data.SqlClient;

using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using Metafuse3.Web.UI;

namespace CarMD.Vehicles
{
	/// <summary>
	/// Class handles is a solution offered to fix errors conditions associated with a vehicle type.
	/// 
	/// The VehicleTypeSolution object handles the business logic and data access for the specialized business object, VehicleTypeSolution.  
	/// The class inherits from <see cref="BusinessObjectBase"/> class. 
	/// </summary>
	/// <example>
	/// See the examples of how to construct the VehicleTypeSolution object.
	/// 
	/// To create a new instance of a new of VehicleTypeSolution.
	/// <code>VehicleTypeSolution o = (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution));</code>
	/// 
	/// To create an new instance of an existing VehicleTypeSolution.
	/// <code>VehicleTypeSolution o = (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
	/// </example>
	/// <remarks>
	/// In order to create an instance of VehicleTypeSolution, you need to have a <see cref="Registry"/>
	/// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
	/// </remarks>
	[Serializable()]
	public class VehicleTypeSolution : BusinessObjectBase
	{
		// data object variables
		private		string		title = "";
		private		string		description = "";
		private		decimal		labor;
		private		decimal		additionalCost;
		private		bool		isActive;

		private		VehicleTypeSolutionPartCollection	parts;
		

		#region System Constructors
		/***********************************************************************************************
		 * 
		 * System Constructors
		 * 
		 * **********************************************************************************************/
	
		/// <summary>
		/// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). VehicleTypeSolution object.  
		/// In order to create a new VehicleTypeSolution which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
		/// </summary>
		/// <example>
		/// <code>
		/// VehicleTypeSolution o = (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution));
		/// </code>
		/// </example>
		protected internal VehicleTypeSolution() : base()
		{
			IsObjectCreated = true;
			IsObjectLoaded = true;
			IsObjectDirty = true;		
		}
		/// <summary>
		/// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  VehicleTypeSolution object.
		/// In order to create an existing VehicleTypeSolution object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
		/// </summary>
		/// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
		/// <example>
		/// <code>
		/// VehicleTypeSolution o = (VehicleTypeSolution)Registry.CreateInstance(typeof(VehicleTypeSolution), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
		/// </code>
		/// </example>
		protected internal VehicleTypeSolution(Guid id) : base(id)
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
		/// Gets or sets the <see cref="string"/> title of the vehicle type solution.
		/// </summary>
		public string Title
		{
			get
			{
				EnsureLoaded();
				return title;
			}
			set
			{	
				EnsureLoaded();
				if(title != value)
				{
					IsObjectDirty = true;
					title = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="string"/> description of the vehicle type solution.
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
		/// Gets or sets the <see cref="decimal"/> hours of labor required for the vehicle type solution.
		/// </summary>
		public decimal Labor
		{
			get
			{
				EnsureLoaded();
				return labor;
			}
			set
			{	
				EnsureLoaded();
				if(labor != value)
				{
					IsObjectDirty = true;
					labor = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="decimal"/> additional cost of the vehicle type solution.
		/// </summary>
		public decimal AdditionalCost
		{
			get
			{
				EnsureLoaded();
				return additionalCost;
			}
			set
			{	
				EnsureLoaded();
				if(additionalCost != value)
				{
					IsObjectDirty = true;
					additionalCost = value;
				}
			}
		}


		/// <summary>
		/// Gets or sets the <see cref="Boolean"/> active status of the vehicle type solution.
		/// </summary>
		public bool IsActive
		{
			get
			{
				EnsureLoaded();
				return isActive;
			}
			set
			{	
				EnsureLoaded();
				if(isActive != value)
				{
					IsObjectDirty = true;
					isActive = value;
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


		/// <summary>
		/// Gets the <see cref="VehicleTypeSolutionPartCollection"/> parts.
		/// </summary>
		public VehicleTypeSolutionPartCollection Parts
		{
			get
			{
				if(parts == null)
				{
					parts = new VehicleTypeSolutionPartCollection(Registry);

					//load if not a user created element
					if(!isObjectCreated)
					{
						SqlProcedureCommand call = new SqlProcedureCommand();
						EnsureValidId();

						call.ProcedureName = "VehicleTypeSolutionPart_LoadByVehicleTypeSolution";
						call.AddGuid("VehicleTypeSolutionId", Id);

						parts.Load(call, "VehicleTypeSolutionPartId", true, true);

					}
				}

				return parts;
			}
		}		
		

		#endregion


		#region Business Logic Methods
		
		/***********************************************************************************************
		 * 
		 * Custom Business Logic
		 * 
		 * **********************************************************************************************/
		
		/// <summary>
		/// Searchs for a collection of Vehicle Type
		/// </summary>
		/// <param name="query">A <see cref="string"/> of keywords to be searched for.</param>
		/// <param name="startCharacter">A <see cref="string"/> letter to be searched for as the first character in the title of the solution</param>
		/// <param name="orderBy">The <see cref="string"/> column to sort the results by.</param>
		/// <param name="sortDirection">The <see cref="SortDirection"/> direction in which to sort the results.</param>
		/// <param name="currentPage">The <see cref="int"/> desired page of results to return.</param>
		/// <param name="pageSize">The <see cref="int"/> number of results per page.</param>
		/// <param name="year">The <see cref="int"/> year of the vehicle. (To ignore, pass in -1)</param>
		/// <param name="manufacturerName">The <see cref="string"/> manufacturer of the vehicle. (To ignore, pass in String.Empty)</param>
		/// <param name="make">The <see cref="string"/> make of the vehicle. (To ignore, pass in String.Empty)</param>
		/// <param name="model">The <see cref="string"/> model of the vehicle. (To ignore, pass in String.Empty)</param>
		/// <param name="primaryCode">The <see cref="string"/> primary error code for the desired solutions. (To ignore, pass in String.Empty)</param>
		/// <param name="secondaryCodes">A <see cref="string"/> list of secondary error codes for the desired solutions. (The list should be dilimited with newline chacters. To ignore, pass in String.Empty)</param>
		/// <param name="includeActive">A <see cref="NullableBoolean"/> indicating wheather or not to include active solutions in the results.</param>
		/// <param name="includeNonActive">A <see cref="NullableBoolean"/> indicating wheather or not to include non-active solutions in the results.</param>
		/// <returns>A <see cref="VehicleTypeSolutionCollection"/> collection of matching <see cref="VehicleTypeSolution"/> objects.</returns>
		public static VehicleTypeSolutionCollection Search(string query, string startCharacter, string orderBy, SortDirection sortDirection, int currentPage, int pageSize, int year, string manufacturerName, string make, string model, string primaryCode, string secondaryCodes, NullableBoolean includeActive, NullableBoolean includeNonActive)
		{
			Registry tempRegistry = new Registry();

			VehicleTypeSolutionCollection vehicleTypeSolutions = new VehicleTypeSolutionCollection(tempRegistry);

			SqlProcedureCommand call = new SqlProcedureCommand();
	
			call.ProcedureName = "VehicleTypeSolution_LoadBySearch";
			call.AddNVarChar("Query", query);
			call.AddNVarChar("StartCharacter", startCharacter);
			call.AddNVarChar("OrderBy", orderBy);
			call.AddInt32("SortDirection", (int)sortDirection);
			call.AddInt32("CurrentPage", currentPage);
			call.AddInt32("PageSize", pageSize);
			if(year >= 0)
			{
				call.AddInt32("Year", year);
			}
			call.AddNVarChar("ManufacturerName", manufacturerName);
			call.AddNVarChar("Make", make);
			call.AddNVarChar("Model", model);
			call.AddNVarChar("PrimaryCode", primaryCode);
			if(secondaryCodes.Length > 0)
			{
				call.AddNText("SecondaryCodesXml", Metafuse3.Web.UI.Formatting.DelimittedStringToXMLList(secondaryCodes, Environment.NewLine));
			}
			if(!includeActive.IsNull && !includeNonActive.IsNull)
			{
				if(includeActive.IsFalse && includeNonActive.IsFalse)
				{
					call.AddTinyInt("IsActive", 2);
				}
				else if(includeActive.IsTrue && includeNonActive.IsFalse)
				{
					call.AddTinyInt("IsActive", 1);
				}
				else if(includeActive.IsFalse && includeNonActive.IsTrue)
				{
					call.AddTinyInt("IsActive", 0);
				}
			}
			else if(!includeActive.IsNull)
			{
				call.AddBoolean("IsActive", includeActive.Value);
			}
			else
			{
				call.AddBoolean("IsActive", !includeNonActive.Value);
			}
			
			vehicleTypeSolutions.Load(call, "VehicleTypeSolutionId", true, true, true);

			return vehicleTypeSolutions;
		}

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
				//load the base vehicleTypeSolution if user selected it.
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
			dr.ProcedureName = "VehicleTypeSolution_Load";
			dr.AddGuid("VehicleTypeSolutionId", Id);
		}

		/// <summary>
		/// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
		/// </summary>
		/// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>	
		protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
		{
			title			= dr.GetString("Title");
			description		= dr.GetString("Description");
			labor			= dr.GetDecimal("Labor");
			additionalCost	= dr.GetDecimal("AdditionalCost");
			isActive		= dr.GetBoolean("IsActive");

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
						dr.ProcedureName = "VehicleTypeSolution_Create";
					}
					else
					{
						dr.ProcedureName = "VehicleTypeSolution_Save";
					}

					dr.AddGuid("VehicleTypeSolutionId", Id);
					dr.AddNVarChar("Title", Title);
					dr.AddNText("Description", Description);
					dr.AddDecimal("Labor", Labor);
					dr.AddDecimal("AdditionalCost", AdditionalCost);
					dr.AddBoolean("IsActive", IsActive);

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
				//delete the vehicleTypeSolution
				dr.ProcedureName = "VehicleTypeSolution_Delete";
				dr.AddGuid("VehicleTypeSolutionId", Id);

				dr.ExecuteNonQuery(transaction);
			}
			
			transaction = base.Delete(connection, transaction);

			return transaction;
		}

		#endregion


	}
}
