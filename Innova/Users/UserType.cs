using System.ComponentModel;

namespace Innova.Users
{
    /// <summary>
    /// Summary description for UserType.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// External System User.
        /// </summary>
        [Description("External System User")]
        ExternalSystemUser = 0,

        /// <summary>
        /// OBDFix Technician User.
        /// </summary>
        [Description("OBDFix Technician User")]
        OBDFixTechnicianUser = 1,

        /// <summary>
        /// OBDFix Consultant.
        /// </summary>
        [Description("OBDFix Consultant")]
        OBDFixConsultant = 2,

        /// <summary>
        /// CanOBD2 User.
        /// </summary>
        [Description("CanOBD2 User")]
        CanOBD2User = 3
    }
}