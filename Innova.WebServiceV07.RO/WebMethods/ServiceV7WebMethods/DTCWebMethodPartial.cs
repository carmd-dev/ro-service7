using Innova.InnovaDLLParsers;
using Innova.WebServiceV07.RO.DataObjects;
using System.Collections.Generic;
using System.Web.Services;

namespace Innova.WebServiceV07.RO
{
    public partial class ServiceV7
    {
        /// <summary>
        /// Decode rawstring v02, get dtcs and types
        /// </summary>
        /// <param name="key"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        [WebMethod(Description = "Get DTC Type Info")]
        public List<DTCTypesInfo> GetDTCTypeInfo(WebServiceKey key, string payload)
        {
            List<DTCTypesInfo> dtcTypes = new List<DTCTypesInfo>();

            if (!this.ValidateKey(key))
            {
                return dtcTypes;
            }

            var vehicleExInfo = InnovaRawPayloadParser.ParseVehicleData(payload, Language.English);

            if (vehicleExInfo != null)
            {
                foreach (var dtc in vehicleExInfo.AllDtcs)
                {
                    dtcTypes.Add(new DTCTypesInfo
                    {
                        DTC = dtc.Code,
                        Title = dtc.Definition,
                        RepairStatus = dtc.RepairStatus ?? string.Empty,
                        Types = dtc.Types,
                        Group = dtc.Group//#ABSStatus
                    });
                }
            }
            return dtcTypes;
        }
    }
}