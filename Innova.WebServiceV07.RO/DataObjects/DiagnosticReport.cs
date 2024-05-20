using System;

namespace CarScan.WebService.DataObjects
{
	/// <summary>
	/// The diagnostic report contains the diagnostic report for an input vehicle (Fixes, Error Codes, Monitors etc)
	/// </summary>
	public class DiagnosticReport
	{
		/// <summary>
		/// The <see cref="CarScanSession"/> associated with the diagnostic report.  Determines if there were any errors retrieving the diagnostic report including user session problems.
		/// </summary>
		public		CarScanSession	CarScanSession;
		/// <summary>
		/// The <see cref="VehicleInfo"/> the report is for.
		/// </summary>
		public		VehicleInfo		Vehicle;
		/// <summary>
		/// The array of <see cref="string"/> freeze frame data.
		/// </summary>
		public		string[]	FreezeFrame;
		/// <summary>
		/// The array of <see cref="string"/> monitor data.
		/// </summary>
		public		string[]	Monitors;
		/// <summary>
		/// The <see cref="string"/> emissions status
		/// </summary>
		public		string		EmissionsStatus = "";


		/// <summary>
		/// The array of <see cref="Fix"/> objects associated with the diagnostic report (could be null)
		/// </summary>
		public		Fix[]		Fixes = null;


		
		/// <summary>
		/// Creates a <see cref="DiagnosticReport"/> based on the input from the tool.
		/// </summary>
		/// <param name="rawData">array of <see cref="byte"/> data from the tool</param>
		/// <param name="vin"><see cref="string"/> VIN to decode.</param>
		/// <param name="transmission"><see cref="string"/> transmission.</param>
		/// <returns><see cref="DiagnosticReport"/> to return to the user.</returns>
		protected internal static DiagnosticReport CreateDiagnosticReport(byte rawData, string vin, string transmission)
		{
			return null;
		}
		
	}
}
