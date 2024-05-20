using System.Collections.Generic;

namespace Innova.WebServiceV07.RO.DataObjects
{
    public class DTCTypesInfo
    {
        public string DTC { get; set; }
        public string Title { get; set; }
        public string RepairStatus { get; set; }
        public List<string> Types { get; set; }
        public string Group { get; set; }
    }
}