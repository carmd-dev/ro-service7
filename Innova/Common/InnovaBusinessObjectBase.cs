using Metafuse3.BusinessObjects;
using System;

namespace Innova
{
    /// <summary>
    /// This class is used for the base of Innova classes that require common properties, methods and logic.
    /// </summary>
    public class InnovaBusinessObjectBase : BusinessObjectBase
    {
        /// <summary>
        /// This default constructor creates a new <c>BusinessObjectBase</c> object.
        /// Creates a new Guid when object is first created.
        /// </summary>
        protected internal InnovaBusinessObjectBase() : base()
        {
        }

        /// <summary>
        /// This constructor creates a new <c>BusinessObjectBase</c> object with the supplied <c>id</c>.
        /// </summary>
        /// <param name="id"><c>Guid</c> representing the unique Id for this instance.</param>
        protected internal InnovaBusinessObjectBase(Guid id)
            : base(id)
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="RuntimeInfo"/> to be used.
        /// </summary>
        public RuntimeInfo RuntimeInfo
        {
            get
            {
                // First see if we have a RuntimeInfo object already in the Registry.
                RuntimeInfo runtimeInfo = this.Registry.CustomObject1 as RuntimeInfo;

                // If not, then create an object and set the language to English.
                if (runtimeInfo == null)
                {
                    runtimeInfo = new RuntimeInfo(this.Registry);
                    runtimeInfo.CurrentLanguage = Language.English;
                    this.Registry.CustomObject1 = runtimeInfo;
                }

                return runtimeInfo;
            }
        }
    }
}