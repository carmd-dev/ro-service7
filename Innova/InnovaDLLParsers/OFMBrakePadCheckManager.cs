using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Innova.InnovaDLLParsers
{
    /// <summary>
    /// OFMBrakePadCheckManager
    /// </summary>
    public static class OFMBrakePadCheckManager
    {
        private static readonly List<OFMBrakePadCheckData> ofmDatas;

        static OFMBrakePadCheckManager()
        {
            ofmDatas = new List<OFMBrakePadCheckData>
            {
                new OFMBrakePadCheckData
                {
                    Manufacturer = "BMW",
                    Makes = new List<string>{"BMW"},
                    PIDs = new List<string>{ "Rear Brake Pad Check", "Front Brake Pad Check" },
                    FromIntValue = 50, Values = new List<string>(), Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "BMW",
                    Makes = new List<string>{"BMW"},
                    PIDs = new List<string>{ "Rear Brake Pad Check", "Front Brake Pad Check" },
                    UnderIntValue = 50, Values = new List<string>(), Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "BMW",
                    Makes = new List<string>{"BMW"},
                    PIDs = new List<string>{ "Rear Brake Pad Check", "Front Brake Pad Check" },
                    Values = new List<string>{ "Level Reached", "1. Level Reached", "2. Level Reached", "Not Available"},
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "BMW",
                    Makes = new List<string>{"BMW"},
                    PIDs = new List<string>{ "Rear Brake Pad Check", "Front Brake Pad Check" },
                    Values = new List<string>{"OK"}, Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "FORD",
                    Makes = new List<string>{"Ford", "Lincoln", "Mercury" },
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"ON", "LOW"},
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "FORD",
                    Makes = new List<string>{"Ford", "Lincoln", "Mercury" },
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"OK", "OFF"},
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Jaguar Land Rover",
                    Makes = new List<string>{"Jaguar", "Land Rover"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"ON"},
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Jaguar Land Rover",
                    Makes = new List<string>{"Jaguar", "Land Rover"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"OFF"},
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Toyota",
                    Makes = new List<string>{"Toyota", "Lexus", "Scion"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"ON"},
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Toyota",
                    Makes = new List<string>{"Toyota", "Lexus", "Scion"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{"OFF"},
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Mercedes-Benz",
                    Makes = new List<string>{"Mercedes-Benz"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Present", "On", "Activated"},
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Mercedes-Benz",
                    Makes = new List<string>{"Mercedes-Benz"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Not Present", "OFF", "NOT SET"},
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Mercedes-Benz",
                    Makes = new List<string>{"Mercedes-Benz"},
                    PIDs = new List<string>{ "Rear Brake Pad Check","Front Brake Pad Check" },
                    Values = new List<string>{ "Not OK" },
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Mercedes-Benz",
                    Makes = new List<string>{"Mercedes-Benz"},
                    PIDs = new List<string>{ "Rear Brake Pad Check", "Front Brake Pad Check" },
                    Values = new List<string>{ "OK" },
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Volkswagen",
                    Makes = new List<string>{"Volkswagen", "Audi"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Active/ Activated", "Active", "Activated", "Actived", "Brake lining 0" },
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Volkswagen",
                    Makes = new List<string>{"Volkswagen", "Audi"},
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Not activate/ Not activated", "Not activate", "Not activated", "Not Actived", "Brake lining 1" },
                    Message = "Good Condition"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Chrysler",
                    Makes = new List<string>{ "chrysler", "dodge", "jeep", "fiat", "plymouth" },
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Brake Pads Worn" },
                    Message = "Inspect"
                },
                new OFMBrakePadCheckData
                {
                    Manufacturer = "Chrysler",
                    Makes = new List<string>{ "chrysler", "dodge", "jeep", "fiat", "plymouth" },
                    PIDs = new List<string>{ "Brake Pad Check" },
                    Values = new List<string>{ "Brake Pads Not Worn" },
                    Message = "Good Condition"
                }
            };
        }

        /// <summary>
        /// Get message
        /// </summary>
        /// <param name="make"></param>
        /// <param name="PID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMessage(string make, string PID, string value)
        {
            if (string.IsNullOrWhiteSpace(make) || string.IsNullOrWhiteSpace(PID) || string.IsNullOrWhiteSpace(value))
                return string.Empty;

            make = make.Trim();
            PID = PID.Trim();
            value = value.Trim();

            if (string.Equals("BMW", make, StringComparison.OrdinalIgnoreCase))
            {
                if (value.Contains("%"))
                {
                    int.TryParse(Regex.Replace(value, @"\D+", string.Empty), out var val);
                    return val >= 50 ? "Good Condition" : "Inspect";
                }
            }

            var ignoreCaseComparer = new IgnoreCaseStringComparer();
            var ofmsByMake = ofmDatas.Where(o =>
                o.Makes.Contains(make, ignoreCaseComparer) && o.PIDs.Contains(PID, ignoreCaseComparer));

            if (ofmsByMake == null || !ofmsByMake.Any()) return string.Empty;

            var ofmByValue = ofmsByMake.FirstOrDefault(o => o.Values.Contains(value, ignoreCaseComparer));

            return ofmByValue != null ? ofmByValue.Message : string.Empty;
        }
    }

    /// <summary>
    ///
    /// </summary>
    public class OFMBrakePadCheckData
    {
        public string Manufacturer { get; set; }
        public List<string> Makes { get; set; }
        public List<string> PIDs { get; set; }
        public int FromIntValue { get; set; }
        public int UnderIntValue { get; set; }
        public List<string> Values { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    ///
    /// </summary>
    public class IgnoreCaseStringComparer : IEqualityComparer<string>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }
    }
}