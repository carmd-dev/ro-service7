using System;

namespace Innova.WebServiceV07.RO.DataObjects
{
    /// <summary>
    /// The Schedule maintenance service information objects
    /// </summary>
    public class ScheduleMaintenanceServiceInfo
    {
        /// <summary>
        /// The <see cref="int"/> current mileage interval for the service
        /// </summary>
        public int Mileage = 0;

        /// <summary>
        /// The <see cref="string"/> name of the service.
        /// </summary>
        public string Name = "";

        /// <summary>
        /// DEPRECATED
        /// </summary>
        public string Category = "";

        /// <summary>
        /// The <see cref="FixInfo"/> object containing the specific service and parts and labor for this scheduled maintenance
        /// </summary>
        public FixInfo ServiceInfo;

        /// <summary>
        /// Method updates the web service object from the supplied SDK object
        /// </summary>
        /// <param name="sdkObject"><see cref="Innova.ScheduleMaintenance.ScheduleMaintenancePlanDetail"/> object to create the object from.</param>
        /// <returns><see cref="ScheduleMaintenanceServiceInfo"/> object created from the supplied SDK object.</returns>
        protected internal static ScheduleMaintenanceServiceInfo GetWebServiceObject(Innova.ScheduleMaintenance.ScheduleMaintenancePlanDetail sdkObject)
        {
            ScheduleMaintenanceServiceInfo wsObject = new ScheduleMaintenanceServiceInfo();

            wsObject.Name = sdkObject.ScheduleMaintenanceService.FixName.Description_Translated;
            wsObject.Category = "";//DEPRECATED
            //Added on 2018-11-6 2:15 PM by Nam Lu - INNOVA Dev Team: Detects and assigns the value for category
            var smName = sdkObject.ScheduleMaintenanceService.FixName.Description.ToLower(); //en
            wsObject.Category = "1"; //General category 1
            if (smName.StartsWith("inspect", StringComparison.OrdinalIgnoreCase))
            {
                wsObject.Category = "2"; //Inspect category 2
            }
            else if (smName.StartsWith("check", StringComparison.OrdinalIgnoreCase))
            {
                wsObject.Category = "3"; //Check category 3
            }
            else if (smName.StartsWith("change", StringComparison.OrdinalIgnoreCase))
            {
                wsObject.Category = "4"; //Change category 4
            }
            else if (smName.StartsWith("replace", StringComparison.OrdinalIgnoreCase))
            {
                wsObject.Category = "5"; //Replace category 5
            }
            //Added on 2018-11-6 2:15 PM by Nam Lu - INNOVA Dev Team: Detects and assigns the value for category
            //there should be a value here
            if (sdkObject.NextServiceMileageInterval.HasValue)
            {
                wsObject.Mileage = sdkObject.NextServiceMileageInterval.Value;
            }

            //if there is a service for this
            if (sdkObject.ScheduleMaintenanceService.FixName.FixService != null)
            {
                wsObject.ServiceInfo = FixInfo.GetWebServiceObject(sdkObject.ScheduleMaintenanceService.FixName.FixService);
            }

            return wsObject;
        }
    }
}