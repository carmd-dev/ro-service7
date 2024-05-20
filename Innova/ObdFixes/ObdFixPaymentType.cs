using System.ComponentModel;

namespace Innova.ObdFixes
{
    public enum ObdFixPaymentType
    {
        /// <summary>
        /// OBD1 Codes
        /// </summary>
        [Description("OBD1 Code")]
        OBD1Code = 0,

        /// <summary>
        /// OBD2 B Codes
        /// </summary>
        [Description("OBD2 B Code")]
        OBD2BCode = 1,

        /// <summary>
        /// OBD2 C Codes
        /// </summary>
        [Description("OBD2 C Code")]
        OBD2CCode = 2,

        /// <summary>
        /// OBD2 P Codes
        /// </summary>
        [Description("OBD2 P Code")]
        OBD2PCode = 3,

        /// <summary>
        /// OBD2 U Codes
        /// </summary>
        [Description("OBD2 U Code")]
        OBD2UCode = 4,

        /// <summary>
        /// Tools Required
        /// </summary>
        [Description("Tools Required")]
        ToolsRequired = 5,

        /// <summary>
        /// Symptom
        /// </summary>
        [Description("Symptom Diagnostic")]
        Symptom = 6,

        /// <summary>
        /// Repair Tools
        /// </summary>
        [Description("Repair Tools")]
        RepairTools = 7
    }
}