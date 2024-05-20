using System;

namespace Innova.WebServiceV07.RO.Requests
{
	/// <summary>
	/// The Vehicle class is used to pass vehicle values in a request object that is passed to web methods.
	/// </summary>
	public class VehicleRequest : RequestBase
	{
		/// <summary>
		/// The default contructor for the Vehicle class.
		/// </summary>
		public VehicleRequest()
			: base()
		{

		}

		/// <summary>
		/// The <see cref="string"/> vehicle VIN. REQUIRED for US market, otherwise ignored if empty.
		/// </summary>
		public string VIN;

		/// <summary>
		/// The <see cref="string"/> vehicle class. Ignored if empty.
		/// </summary>
		public string VehicleClass;

		/// <summary>
		/// The <see cref="string"/> vehicle manufacturer. Ignored if empty.
		/// </summary>
		public string Manufacturer;

		/// <summary>
		/// The <see cref="string"/> vehicle make. Ignored if empty.
		/// </summary>
		public string Make;

		/// <summary>
		/// The <see cref="string"/> vehicle model. Ignored if empty.
		/// </summary>
		public string Model;

		/// <summary>
		/// The <see cref="string"/> vehicle trim. Ignored if empty.
		/// </summary>
		public string Trim;

		/// <summary>
		/// The <see cref="int"/> vehicle year. Ignored if null.
		/// </summary>
		public int? Year;

		/// <summary>
		/// The <see cref="string"/> vehicle engine type. Ignored if empty.
		/// </summary>
		public string EngineType;

		/// <summary>
		/// The <see cref="string"/> vehicle engine VIN code. Ignored if empty.
		/// </summary>
		public string EngineVinCode;

		/// <summary>
		/// The <see cref="string"/> vehicle transmission. Ignored if empty.
		/// </summary>
		public string Transmission;

		/// <summary>
		/// The <see cref="string"/> vehicle AAIA. Ignored if empty.
		/// </summary>
		public string AAIA;

		/// <summary>
		/// The <see cref="int"/> current vehicle mileage. Ignored if null.
		/// </summary>
		public int? CurrentMileage;
	}
}