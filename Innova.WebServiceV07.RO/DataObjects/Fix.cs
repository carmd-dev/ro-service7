using System;

namespace CarScan.WebService.DataObjects
{
	/// <summary>
	/// A fix that is associated with the primary error code on the diagnostic report.
	/// </summary>
	public class Fix
	{

		/// <summary>
		/// The <see cref="string"/> name of the fix.
		/// </summary>
		public		string			Name = "";
		/// <summary>
		/// The <see cref="string"/> description of the fix. (FUTURE ONLY).
		/// </summary>
		public		string			Description = "";

		/// <summary>
		/// The <see cref="decimal"/> labor hours required for the fix.
		/// </summary>
		public		decimal			LaborHours = 0;
		/// <summary>
		/// The <see cref="decimal"/> labor rate for the fix (for your state).
		/// </summary>
		public		decimal			LaborRate = 0;
		/// <summary>
		/// The <see cref="decimal"/> calculated labor cost.
		/// </summary>
		public		decimal			LaborCost = 0;
		/// <summary>
		/// The <see cref="decimal"/> calculated parts cost.
		/// </summary>
		public		decimal			PartsCost = 0;
		/// <summary>
		/// The <see cref="decimal"/> additional cost (if any).
		/// </summary>
		public		decimal			AdditionalCost = 0;
		/// <summary>
		/// The	<see cref="decimal"/> total cost.
		/// </summary>
		public		decimal			TotalCost;
		/// <summary>
		/// The array of <see cref="FixPart"/> objects required for this fix.  Could be null.
		/// </summary>
		public		FixPart[]		FixParts;

		
	}
}
