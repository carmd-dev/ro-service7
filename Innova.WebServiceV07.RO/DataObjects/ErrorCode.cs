using System;

namespace CarScan.WebService.DataObjects
{
	/// <summary>
	/// The class that holds error code information from the tool.  Contains Primary Code (MIL ON, Freeze Frame), Stored Codes and Pending Codes
	/// </summary>
	public class ErrorCode
	{
		/// <summary>
		/// The <see cref="string"/> code (OBD code)
		/// </summary>
		public		string			Code = "";

		/// <summary>
		/// The <see cref="int"/> code type. The possibilities are: (0 - Primary Error Code, 1 - First Stored Cost, 2 - Additional Stored Code, 3 - First Pending Code, 4 - Additional Pending Code
		/// </summary>
		public		int				CodeType;

		
		
	}
}
