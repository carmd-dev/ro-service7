using System;

using Metafuse3.Data;

namespace CarMD.Vehicles
{
	/// <summary>
	/// Summary description for VehicleTypeCodeAssignmentSearchResultCollection.
	/// </summary>
	public class VehicleTypeCodeAssignmentSearchResultCollection : Metafuse3.BusinessObjects.SmartCollection
	{		
		/// <summary>
		/// The default constructor.
		/// </summary>
		public VehicleTypeCodeAssignmentSearchResultCollection(): base()
		{

		}

		#region Indexer

		/// <summary>
		/// Indexer for this collection.
		/// </summary>
		public new VehicleTypeCodeAssignmentSearchResult this[int index]
		{
			get
			{
				return (VehicleTypeCodeAssignmentSearchResult)List[index];
			}
		}

		#endregion

		/// <summary>
		/// Sets the pagination
		/// </summary>
		/// <param name="pagination">The <see cref="Pagination"/> object to use.</param>
		public void SetPagination(Pagination pagination)
		{
			this.pagination = pagination;
		}

		/// <summary>
		/// Adds a <see cref="VehicleTypeCodeAssignmentSearchResult"/> object to the collection.
		/// </summary>
		/// <param name="vehicleTypeCodeAssignmentSearchResult">The <see cref="VehicleTypeCodeAssignmentSearchResult"/> object to add.</param>
		public void Add(VehicleTypeCodeAssignmentSearchResult vehicleTypeCodeAssignmentSearchResult)
		{
			base.Add(vehicleTypeCodeAssignmentSearchResult);
		}
	}
}
