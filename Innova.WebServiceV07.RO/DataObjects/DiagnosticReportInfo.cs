using System;


namespace Innova.WebService.DataObjects
{
	/// <summary>
	/// The diagnostic report contains the diagnostic report for an input vehicle (Fixes, Error Codes, Monitors etc)
	/// </summary>
	public class DiagnosticReportInfo
	{

		/// <summary>
		/// Default constructor for the diagnostic report class.
		/// </summary>
		public DiagnosticReportInfo()
		{
		}


		/// <summary>
		/// The <see cref="CarScanSession"/> associated with the diagnostic report.  Determines if there were any errors retrieving the diagnostic report including user session problems.
		/// </summary>
		public		CarScanSession	CarScanSession;
		/// <summary>
		/// The <see cref="VehicleInfo"/> the report is for.
		/// </summary>
		public		VehicleInfo		Vehicle;
		/// <summary>
		/// The array of <see cref="ErrorCodeInfo"/>
		/// </summary>
		public		ErrorCodeInfo[]		Errors;

		

		/// <summary>
		/// The array of <see cref="FreezeFrameInfo"/> freeze frame data.
		/// </summary>
		public		FreezeFrameInfo[]	FreezeFrame;
		/// <summary>
		/// The array of <see cref="MonitorInfo"/> monitor data.
		/// </summary>
		public		MonitorInfo[]	Monitors;
		
		/// <summary>
		/// The <see cref="string"/> tool LED status, Off, Green, Yellow, Red, Error
		/// </summary>
		public		string		ToolLEDStatusDesc = "";

		/// <summary>
		/// The array of <see cref="FixInfo"/> objects associated with the diagnostic report (could be null)
		/// </summary>
		public		FixInfo[]		Fixes = null;



		/* 4.0.4 New Fields */

		/// <summary>
		/// The array of <see cref="VehiclePartInfo"/> object associated with the vehicle on the diagnostic report. (could be null if none)
		/// </summary>
		public		VehiclePartInfo[] VehicleParts = null;

		/// <summary>
		/// The <see cref="string"/> diagnostic report number, a user friendly alpha numeric report number.
		/// </summary>
		public		string			DiagnosticReportNumber = "";
		/// <summary>
		/// The <see cref="string"/> CarScan Kiosk number, a user friendly kiosk number used on input.
		/// </summary>
		public		string			CarScanKioskNumber = "";


		/// <summary>
		/// The <see cref="int"/> number of technical support bulletins for the vehicle.
		/// </summary>
		public		int				TechnicalSupportBulletinCount = 0;
		/// <summary>
		/// The <see cref="int"/> number of recall bulletins available.
		/// </summary>
		public		int				RecallBulletinCount = 0;

		/// <summary>
		/// The <see cref="decimal"/> total amount charged on the credit card
		/// </summary>
		public		decimal			TotalCharged = 0;
		/// <summary>
		/// The <see cref="string"/> string credit card type.
		/// </summary>
		public		string			CCType = "Visa";
		/// <summary>
		/// The <see cref="string"/> last 4 digits of the card processed to place the order.
		/// </summary>
		public		string			CCLast4 = "1234";
		/// <summary>
		/// The <see cref="string"/> authorization code. 
		/// </summary>
		public		string			AuthorizationCode = "AUTHCODE";
		/// <summary>
		/// The <see cref="string"/> date time the authorization occurred in a string format (US String)
		/// </summary>
		public		string			ProcessedDateTimeUTCString = DateTime.UtcNow.ToString();


		/* 4.1 Fields */

		/// <summary>
		/// The <see cref="bool"/> flag indicating whether or not the primary fix has a repair procedure or not.
		/// </summary>
		public		bool			HasRepairProcedureForPrimaryFix;


		
		
		
	}
}
