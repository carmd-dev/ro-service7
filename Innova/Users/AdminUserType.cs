using System.ComponentModel;

namespace Innova.Users
{
    /// <summary>
    /// Summary description for AdminUserType.
    /// </summary>
    public enum AdminUserType
    {
        /// <summary>
        /// CarMD Admin.
        /// </summary>
        [Description("CarMD Admin")]
        CarMDAdmin = 0,

        /// <summary>
        /// Tech Admin.
        /// </summary>
        [Description("Tech Admin")]
        TechAdmin = 1,

        /// <summary>
        /// CarMD and Tech Admin.
        /// </summary>
        [Description("CarMD & Tech Admin")]
        CarMDAndTechAdmin = 2,

        /// <summary>
        /// Validation Tech.
        /// </summary>
        [Description("Validation Tech")]
        ValidationTech = 3,

        /// <summary>
        /// Validation Admin.
        /// </summary>
        [Description("Validation Admin")]
        ValidationAdmin = 4
    }
}