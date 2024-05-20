using System;

namespace Innova.WebService.DataObjects
{
	/// <summary>
	/// Holds the information related to getting the URL for the repair procedure.
	/// </summary>
	public class RepairProcedure
	{

		/// <summary>
		/// The <see cref="CarScanSession"/> that is associated to this repair procedure.
		/// </summary>
		public CarScanSession			CarScanSession;

		/// <summary>
		/// The <see cref="string"/> repair procedure URL.
		/// </summary>
		public string					RepairProcedureURL;

		/// <summary>
		/// Default constructor for the repair procedure
		/// </summary>
		public RepairProcedure()
		{
			
		}




	}
}
