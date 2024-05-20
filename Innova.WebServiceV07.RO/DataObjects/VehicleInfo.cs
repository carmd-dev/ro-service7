using Innova.Vehicles;
using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The vehicle information class holds vehicle information for the vehicle that was VIN decoded for and for the diagnostic reports.
    /// </summary>
    public class VehicleInfo
    {
        /// <summary>
        /// The default constructor for the vehicle info class.
        /// </summary>
        public VehicleInfo()
        {
        }

        /// <summary>
        /// The <see cref="Guid"/> ID of the vehicle, used in certain conditions (like scheduled maintenance, recalls and TSB alert system).
        /// </summary>
        public Guid? VehicleId;

        /// <summary>
        /// The <see cref="bool"/> flag indicating that the vehicle was decoded properly and is valid.  Default true unless set to false.
        /// </summary>
        public bool IsValid = false;

        /// <summary>
        /// The array of <see cref="ValidationFailure"/> objects holding validation failure codes and descriptions.
        /// </summary>
        public ValidationFailure[] ValidationFailures = null;

        /// <summary>
        /// The <see cref="string"/> VIN that was decoded or attempted to be decoded.
        /// </summary>
        public string VIN = "";

        /// <summary>
        /// The <see cref="string"/> common Manufacturer.
        /// </summary>
        public string ManufacturerName = "";

        /// <summary>
        /// The <see cref="string"/> decoded year.
        /// </summary>
        public string Year = "";

        /// <summary>
        /// The <see cref="string"/> decoded make.
        /// </summary>
        public string Make = "";

        /// <summary>
        /// The <see cref="string"/> decoded model.
        /// </summary>
        public string Model = "";

        /// <summary>
        /// The <see cref="string"/> decoded engine type.
        /// </summary>
        public string EngineType = "";

        /// <summary>
        /// The <see cref="string"/> decoded transmission type (Future feature of VINPower)
        /// </summary>
        public string Transmission = "";

        public decimal FuelMPGCombined = 0;

        public decimal FuelMPGCity = 0;

        public decimal FuelMPGHighway = 0;

        /// <summary>
        /// The <see cref="string"/> decoded Manufacturer.
        /// </summary>
        public string ManufacturerNameAlt = "";

        /// <summary>
        /// The <see cref="string"/> decoded engine VIN code.
        /// </summary>
        public string EngineVINCode = "";

        /// <summary>
        /// The <see cref="string"/> decoded body type.
        /// </summary>
        public string BodyType = "";

        /// <summary>
        /// The <see cref="string"/> decoded trim level.
        /// </summary>
        public string TrimLevel = "";

        /// <summary>
        /// The <see cref="string"/> decoded series.
        /// </summary>
        public string Series = "";

        /// <summary>
        /// The <see cref="string"/> AAIA number for the vehicle
        /// </summary>
        public string AAIA = "";

        /// <summary>
        /// The <see cref="int"/> mileage value assigned to the vehicle.
        /// </summary>
        public int? Mileage = null;

        /// <summary>
        /// The universal <see cref="DateTime"/> the mileage was last recorded.
        /// </summary>
        public DateTime? MileageLastRecordedDateTimeUTC = null;

        /// <summary>
        /// The <see cref="bool"/> flag indicating the vehicle is signed up for scheduled maintenance alerts
        /// </summary>
        public bool SendScheduledMaintenanceAlerts = false;

        /// <summary>
        /// The <see cref="bool"/> flag indicating the vehicle is signed up to for TSB alerts
        /// </summary>
        public bool SendNewTSBAlerts = false;

        /// <summary>
        /// The <see cref="bool"/> flag indicating the vehicle is signed up for sent recall alerts
        /// </summary>
        public bool SendNewRecallAlerts = false;

        /// <summary>
        /// The array of <see cref="RecallInfo"/> objects which have changed since the last invocation, or all of the recall currently in the system for the users vehicle
        /// </summary>
        public RecallInfo[] NewRecallsToAlert = null;

        /// <summary>
        /// The array of <see cref="TSBInfo"/> objects which have changed since the last invocation of the update method.
        /// </summary>
        public TSBInfo[] NewTSBsToAlert = null;

        /// <summary>
        /// The array of <see cref="ScheduleMaintenanceServiceInfo"/> objects which contain the services for the vehicle.
        /// </summary>
        public ScheduleMaintenanceServiceInfo[] ScheduleMaintenanceServices = null;

        /// <summary>
        /// The <see cref="string"/> URL to the Polk model image file.
        /// </summary>
        public string ModelImageFileUrl;

        /// <summary>
        /// Base Vehicle ID
        /// </summary>
        public int ACESId { get; set; }

        /// <summary>
        /// Gets the <see cref="VehicleInfo"/> result from the Polk vehicle.
        /// </summary>
        /// <param name="v"><see cref="VehicleInfo"/> object to populate</param>
        /// <param name="polkVehicle">The <see cref="PolkVehicle"/> object.</param>
        /// <param name="vin">The <see cref="string"/> VIN used to decode the Polk vehicle.</param>
        /// <returns><see cref="VehicleInfo"/> vehicle info object populated from the PolkVehicle object supplied.</returns>
        protected internal static void PopulateVehicleInfoFromPolkVehicle(VehicleInfo v, PolkVehicleYMME polkVehicle, string vin)
        {
            v.IsValid = (polkVehicle != null);

            if (polkVehicle != null)
            {
                v.VIN = vin;
                v.ManufacturerName = polkVehicle.Manufacturer;
                v.Make = polkVehicle.Make;
                v.Year = polkVehicle.Year.ToString();
                v.Model = polkVehicle.Model;
                v.Transmission = polkVehicle.Transmission;
                v.EngineType = polkVehicle.EngineType;

                v.FuelMPGCombined = polkVehicle.FuelMPGCombined.GetValueOrDefault(0);
                v.FuelMPGCity = polkVehicle.FuelMPGCity.GetValueOrDefault(0);
                v.FuelMPGHighway = polkVehicle.FuelMPGHighway.GetValueOrDefault(0);

                v.TrimLevel = polkVehicle.Trim;
                v.EngineVINCode = polkVehicle.EngineVinCode;
                v.AAIA = polkVehicle.AAIA;
                v.ModelImageFileUrl = polkVehicle.GetModelImageUrl(Global.PolkVehicleImageRootUrl);
                v.ACESId = polkVehicle.ACESBaseVehicleID ?? 0;
            }
        }

        /// <summary>
        /// Populates the vehicle from the <see cref="Innova.Vehicles.Vehicle"/> object.
        /// </summary>
        /// <param name="v"><see cref="VehicleInfo"/> object to populate</param>
        /// <param name="innovaVehicle"><see cref="Innova.Vehicles.Vehicle"/> innova vehicle</param>
        /// <param name="usePolkData">A <see cref="bool"/> to indicate if Polk YMME values should be used to populate the object.</param>
        protected internal static void PopulateVehicleInfoFromInnovaVehicle(VehicleInfo v, Innova.Vehicles.Vehicle innovaVehicle, bool usePolkData)
        {
            v.VehicleId = innovaVehicle.Id;
            v.IsValid = true;
            v.VIN = innovaVehicle.Vin;

            if (usePolkData && innovaVehicle.PolkVehicleYMME != null)
            {
                v.ManufacturerName = innovaVehicle.PolkVehicleYMME.Manufacturer;
                v.Make = innovaVehicle.PolkVehicleYMME.Make;
                v.Year = innovaVehicle.PolkVehicleYMME.Year.ToString();
                v.Model = innovaVehicle.PolkVehicleYMME.Model;
                v.Transmission = innovaVehicle.PolkVehicleYMME.Transmission;
                v.EngineType = innovaVehicle.PolkVehicleYMME.EngineType;

                v.FuelMPGCombined = innovaVehicle.PolkVehicleYMME.FuelMPGCombined.GetValueOrDefault(0);
                v.FuelMPGCity = innovaVehicle.PolkVehicleYMME.FuelMPGCity.GetValueOrDefault(0);
                v.FuelMPGHighway = innovaVehicle.PolkVehicleYMME.FuelMPGHighway.GetValueOrDefault(0);

                v.TrimLevel = innovaVehicle.PolkVehicleYMME.Trim;
                v.EngineVINCode = innovaVehicle.PolkVehicleYMME.EngineVinCode;

                v.AAIA = innovaVehicle.PolkVehicleYMME.AAIA;
            }
            else
            {
                v.ManufacturerName = innovaVehicle.ManufacturerName;
                v.Make = innovaVehicle.Make;
                v.Year = innovaVehicle.Year.ToString();
                v.Model = innovaVehicle.Model;
                v.Transmission = innovaVehicle.TransmissionControlType;
                v.EngineType = innovaVehicle.EngineType;

                v.ManufacturerNameAlt = innovaVehicle.ManufacturerNameAlt;
                v.TrimLevel = innovaVehicle.TrimLevel;
                v.EngineVINCode = innovaVehicle.EngineVINCode;
                v.BodyType = innovaVehicle.BodyCode;
                v.AAIA = innovaVehicle.AAIA;
            }

            if (!innovaVehicle.Mileage.IsNull)
            {
                v.Mileage = innovaVehicle.Mileage.Value;
            }
            if (!innovaVehicle.MileageLastRecordedDateTimeUTC.IsNull)
            {
                v.MileageLastRecordedDateTimeUTC = innovaVehicle.MileageLastRecordedDateTimeUTC.Value;
            }
            v.SendNewRecallAlerts = innovaVehicle.SendNewRecallAlerts;
            v.SendNewTSBAlerts = innovaVehicle.SendNewTSBAlerts;
            v.SendScheduledMaintenanceAlerts = innovaVehicle.SendScheduledMaintenanceAlerts;
        }
    }
}