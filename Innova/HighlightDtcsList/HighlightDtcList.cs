using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Innova.HighlightDtcsList
{
    //#IWD-12
    public class HighlightDtcList
    {
        private static List<string> sortedDtcList;

        public static string GetHighestPriorityDtc(List<string> input)
        {
            if (input == null)
                return string.Empty;

            //just find fixes for P code
            input = input.Where(dtc => dtc.StartsWith("P") || dtc.StartsWith("p")).ToList();

            if (!input.Any())
                return string.Empty;

            if (sortedDtcList == null)
            {
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");
                var filePath = Path.Combine(path, @"HighlightDtcsList\HighlightDTCList.INI");

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"HighlightDTCList.INI file not found in {path}");

                var allLines = File.ReadAllLines(filePath).ToList();

                sortedDtcList = new List<string>();
                foreach (var line in allLines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    sortedDtcList.Add(line.Trim());
                }
            }

            //Check in DTC List first
            string foundDtc = null;
            for (var i = 0; i < sortedDtcList.Count; i++)
            {
                foundDtc = input.FirstOrDefault(ip => string.Equals(ip, sortedDtcList[i], StringComparison.OrdinalIgnoreCase));
                if (foundDtc != null)
                {
                    break;
                }
            }

            if (foundDtc != null)
                return foundDtc;

            //Then lowest P code
            foundDtc = input.OrderBy(dtc => dtc).FirstOrDefault();

            return foundDtc;
        }
    }
}

