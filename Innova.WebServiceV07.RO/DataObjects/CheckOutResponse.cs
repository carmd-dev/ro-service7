using System;

namespace Innova.WebService.DataObjects
{
	/// <summary>
	/// The check out response object is returned from the check out method and indicates whether the server accepted the checkout request successfully.
	/// </summary>
	public class CheckOutReturn
	{

		/// <summary>
		/// Default constructor for the checkout response object.
		/// </summary>
		public CheckOutReturn()
		{
		}

		/// <summary>
		/// The <see cref="bool"/> flag indicating whether or not the checkout request was successfully executed.
		/// </summary>
		public	bool	IsValid = false;


		/// <summary>
		/// The array of <see cref="ValidationFailure"/> objects holding validation failure codes and descriptions.
		/// </summary>
		public ValidationFailure[] ValidationFailures = null;
	}
}
