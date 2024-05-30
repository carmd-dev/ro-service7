using Innova.Vehicles;
using Metafuse3.BusinessObjects;
using System;

namespace Innova.WebServiceV07.RO.Services
{
    public class VINDecodingService
    {
        public static (PolkVehicleYMME, string) DecodeVIN(Registry registry, string vin, bool validateVin)
        {
            PolkVehicleYMME polkVehicleYMME = null;
            string errorMessage = string.Empty;

            try
            {
                PolkVinDecoder polkVinDecoder = new PolkVinDecoder(registry);

                if (polkVinDecoder.IsVinAValidMaskPattern(vin))
                {
                    validateVin = false;
                }

                polkVehicleYMME = polkVinDecoder.DecodeVIN(vin, validateVin);

                if (polkVehicleYMME == null)
                {
                    errorMessage = "The VIN supplied is invalid, you cannot retreive a diagnostic report without a valid vin";
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"{ex}";
            }

            return (polkVehicleYMME, errorMessage);
        }
    }
}