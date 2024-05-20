using System;
using System.Xml.Serialization;
using Innova.WebService.DataObjects.E;
using CS = Innova.CarScan;

namespace Innova.WebService.DataObjects
{
	/// <summary>
	/// The CarScan session class handles the authorization of the credit card and general session availability.  
	/// </summary>
	public class CarScanSession
	{
	
		/// <summary>
		/// Default constructor for the car scan session class.
		/// </summary>
		public CarScanSession()
		{
			this.ValidationFailures = new ValidationFailure[0];
		}

		/// <summary>
		/// The <see cref="bool"/> flag indicating whether or not the session is valid. (Default true unless validation messages are added).
		/// </summary>
		public bool				IsValid = true;

		/// <summary>
		/// The array of <see cref="ValidationFailure"/> objects holding validation failure codes and descriptions.
		/// </summary>
		public ValidationFailure[] ValidationFailures = null;

		/// <summary>
		/// The <see cref="string"/> CarScan session id.  The Id is required for subsequent calls to the service and must match records in the database to proceed with method calls.
		/// </summary>
		public	string			CarScanSessionId;


		/// <summary>
		/// The <see cref="string"/> first name of the user associated with the session.
		/// </summary>
		public	string			FirstName = "";

		/// <summary>
		/// The <see cref="string"/> last name of the user associated with the session.
		/// </summary>
		public string			LastName = "";


		/// <summary>
		/// The <see cref="string"/> zip code the session was submitted with.
		/// </summary>
		public string			ZipCode = "";

		//added in 2.0.5
		public string			CCNumber = "";
		public string			CCType = "";
		public string			CCExpMonth = "";
		public string			CCExpYear = "";
		

		/// <summary>
		/// The <see cref="string"/> date time in universal time (US Date Time Format) the session was initially created.  For information.
		/// </summary>
		public string			CreatedDateTimeUTCString = "";

		/// <summary>
		/// Gets the <see cref="VehicleInfo"/> object (if any and if the payload is sent up during check-in).
		/// </summary>
		public VehicleInfo		VehicleInfoFoundDuringCheckIn = null;


		[XmlIgnore()]
		protected internal CS.CarScanSession	CSCarScanSession;

		[XmlIgnore()]
		protected internal Guid CarScanSessionObjectId;

		/// <summary>
		/// Adds validation failure to the existing validation failures
		/// </summary>
		/// <param name="code"><see cref="string"/> code to add.</param>
		protected internal void AddValidationFailure(string code, string description)
		{
			

			ValidationFailure[] previous = this.ValidationFailures;

			ValidationFailures = new ValidationFailure[previous.Length + 1];

			for(int i = 0; i < previous.Length; i++)
			{
				ValidationFailures[i] = previous[i];
			}

			ValidationFailure vf = new ValidationFailure();
			vf.Code = code;
			vf.Description = description;
		
			//add the newest failure to the end of the list
			ValidationFailures[ValidationFailures.Length - 1] = vf;

			this.IsValid = false;

	
		}


		/*
		/// <summary>
		/// Gets a new <see cref="CarScanSession"/> object based on the supplied <see cref="CS.CarScanSession"/> object.
		/// </summary>
		/// <returns>A <see cref="CarScanSession"/> object.</returns>
		protected internal static CarScanSession GetWebServiceObject(CS.CarScanSession csCarScanSession)
		{
			CarScanSession wsCarScanSession = new CarScanSession();
			wsCarScanSession.CarScanSessionId = K.K2;

			wsCarScanSession.FirstName = csCarScanSession.FirstName;
			wsCarScanSession.LastName = csCarScanSession.LastName;
			wsCarScanSession.CreatedDateTimeUTCString = csCarScanSession.CreatedDateTimeUTC.ToString();
			
			return wsCarScanSession;
		}
		*/

	}
}
