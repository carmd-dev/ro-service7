using System;

namespace CarScan.WebService.DataObjects
{
	/// <summary>
	/// The fix part class holds the information for each part that is associated with a fix on the diagnostic report.
	/// </summary>
	public class FixPart
	{
		/// <summary>
		/// The <see cref="int"/> quantity of fix parts.
		/// </summary>
		public		int			Quantity = 0;

		/// <summary>
		/// The <see cref="string"/> manufacturer name of the part.
		/// </summary>
		public		string		ManufacturerName = "";

		/// <summary>
		/// The <see cref="string"/> part number.
		/// </summary>
		public		string		PartNumber = "";

		/// <summary>
		/// The <see cref="string"/> name of the part.
		/// </summary>
		public		string		Name = "";

		/// <summary>
		/// The <see cref="string"/> description of the part. (Future Only)
		/// </summary>
		public		string		Description = "";

		/// <summary>
		/// The <see cref="decimal"/> price of the part (in USD).
		/// </summary>
		public		decimal		Price = 0;
		
	}
}
