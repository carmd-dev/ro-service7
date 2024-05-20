using System;

using Metafuse3;
using Metafuse3.BusinessObjects;
using Metafuse3.Data;
using Metafuse3.Data.SqlClient;
using Metafuse3.NullableTypes;
using Metafuse3.Web.UI;

using CarMD.Vehicles;

namespace CarMD.Vehicles
{
	/// <summary>
	/// This class is used to hold data and objects that are returned from search results.
	/// </summary>
	public class VehicleTypeCodeAssignmentSearchResult
	{
		private	VehicleTypeCodeAssignment	vehicleTypeCodeAssignment;
		private	VehicleTypeCodeSolution		vehicleTypeCodeSolution;
		private string						secondaryCodes;
		
		/// <summary>
		/// Contructs a <see cref="VehicleTypeCodeAssignmentSearchResult"/> object.
		/// </summary>
		/// <param name="vehicleTypeCodeAssignment">The <see cref="VehicleTypeCodeAssignment"/> object.</param>
		/// <param name="vehicleTypeCodeSolution">The <see cref="VehicleTypeCodeSolution"/> object.</param>
		public VehicleTypeCodeAssignmentSearchResult(VehicleTypeCodeAssignment vehicleTypeCodeAssignment, VehicleTypeCodeSolution vehicleTypeCodeSolution)
		{
			this.vehicleTypeCodeAssignment	= vehicleTypeCodeAssignment;
			this.vehicleTypeCodeSolution	= vehicleTypeCodeSolution;
		}

		/// <summary>
		/// Gets or sets the <see cref="VehicleTypeCodeAssignment"/> object.
		/// </summary>
		public VehicleTypeCodeAssignment VehicleTypeCodeAssignment
		{
			get
			{
				return this.vehicleTypeCodeAssignment;
			}
			set
			{
				this.vehicleTypeCodeAssignment = value;
			}
		}

		/// <summary>
		/// Gets or sets the <see cref="VehicleTypeCodeSolution"/> object.
		/// </summary>
		public VehicleTypeCodeSolution VehicleTypeCodeSolution
		{
			get
			{
				return this.vehicleTypeCodeSolution;
			}
			set
			{
				this.vehicleTypeCodeSolution = value;
			}
		}

		/// <summary>
		/// Gets or sets a <see cref="string"/> of comma separated error codes
		/// </summary>
		public string SecondaryCodes
		{
			get
			{
				return this.secondaryCodes;
			}
			set
			{
				this.secondaryCodes = value;
			}
		}



		/// <summary>
		/// Searchs for a collection of Vehicle Type Code Assignment Search Result objects
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
		/// <returns>A <see cref="VehicleTypeCodeAssignmentSearchResultCollection"/> collection of matching <see cref="VehicleTypeCodeAssignmentSearchResult"/> objects.</returns>
		public static VehicleTypeCodeAssignmentSearchResultCollection Search(string query, string startCharacter, string orderBy, SortDirection sortDirection, int currentPage, int pageSize, int year, string manufacturerName, string make, string model, string primaryCode, string secondaryCodes, NullableBoolean includeActive, NullableBoolean includeNonActive)
		{
			Registry tempRegistry = new Registry();
			VehicleTypeCodeAssignmentSearchResultCollection vehicleTypeCodeAssignmentResults = new VehicleTypeCodeAssignmentSearchResultCollection();

			using(SqlDataReaderWrapper dr = new SqlDataReaderWrapper(Metafuse3.BusinessObjects.BusinessObjectBase.ConnectionString))
			{
				dr.ProcedureName = "VehicleTypeCodeAssignmentSearchResult_LoadBySearch";
				dr.AddNVarChar("Query", query);
				dr.AddNVarChar("StartCharacter", startCharacter);
				dr.AddNVarChar("OrderBy", orderBy);
				dr.AddInt32("SortDirection", (int)sortDirection);
				dr.AddInt32("CurrentPage", currentPage);
				dr.AddInt32("PageSize", pageSize);
				if(year >= 0)
				{
					dr.AddInt32("Year", year);
				}
				dr.AddNVarChar("ManufacturerName", manufacturerName);
				dr.AddNVarChar("Make", make);
				dr.AddNVarChar("Model", model);
				dr.AddNVarChar("PrimaryCode", primaryCode);
				if(secondaryCodes.Length > 0)
				{
					dr.AddNText("SecondaryCodesXml", Metafuse3.Web.UI.Formatting.DelimittedStringToXMLList(secondaryCodes, Environment.NewLine));
				}
				if(!includeActive.IsNull && !includeNonActive.IsNull)
				{
					if(includeActive.IsFalse && includeNonActive.IsFalse)
					{
						dr.AddTinyInt("IsActive", 2);
					}
					else if(includeActive.IsTrue && includeNonActive.IsFalse)
					{
						dr.AddTinyInt("IsActive", 1);
					}
					else if(includeActive.IsFalse && includeNonActive.IsTrue)
					{
						dr.AddTinyInt("IsActive", 0);
					}
				}
				else if(!includeActive.IsNull)
				{
					dr.AddBoolean("IsActive", includeActive.Value);
				}
				else
				{
					dr.AddBoolean("IsActive", !includeNonActive.Value);
				}

				dr.Execute();

				if(dr.Read())
				{
					vehicleTypeCodeAssignmentResults.SetPagination(new Pagination(dr.GetInt32("AbsolutePage"), dr.GetInt32("PageSize"), dr.GetInt32("RecordCount"), dr.GetInt32("MaxRecords")));
				}

				dr.NextResult();

				while(dr.Read())
				{
					VehicleTypeCodeAssignment vtca = (VehicleTypeCodeAssignment)tempRegistry.CreateInstance(typeof(VehicleTypeCodeAssignment), dr.GetGuid("VehicleTypeCodeAssignmentId"));
					vtca.LoadPropertiesFromDataReader(dr, false);
					vtca.VehicleType.LoadPropertiesFromDataReader(dr, false);
					
					VehicleTypeCodeSolution vtcs = (VehicleTypeCodeSolution)tempRegistry.CreateInstance(typeof(VehicleTypeCodeSolution), dr.GetGuid("VehicleTypeCodeSolutionId"));
					vtcs.LoadPropertiesFromDataReader(dr, false);
					vtcs.VehicleTypeSolution.LoadPropertiesFromDataReader(dr, false);
					
					VehicleTypeCodeAssignmentSearchResult vtcasr = new VehicleTypeCodeAssignmentSearchResult(vtca, vtcs);
					vtcasr.SecondaryCodes = dr.GetString("SecondaryCodes");

					vehicleTypeCodeAssignmentResults.Add(vtcasr);
				}
			}

			return vehicleTypeCodeAssignmentResults;
		}

	}
}
