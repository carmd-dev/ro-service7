namespace Innova.Vehicles
{
    public class PolkVehicle
    {
        private string vinPatternMask = "";
        private string manufacturer = "";
        private string make = "";
        private string model = "";
        private int modelYear;
        private string engineType = "";
        private string engineVINCode = "";
        private string trimLevel = "";
        private string aaia = "";

        /// <summary>
        /// Constructor for the PolkVehicle object.
        /// </summary>
        /// <param name="vinPatternMask">The <see cref="string"/> VIN pattern mask.</param>
        /// <param name="manufacturer">The <see cref="string"/> manufacturer.</param>
        /// <param name="make">The <see cref="string"/> make.</param>
        /// <param name="model">The <see cref="string"/> model.</param>
        /// <param name="modelYear">The <see cref="string"/> year.</param>
        /// <param name="engineType">The <see cref="string"/> engine.</param>
        /// <param name="engineVINCode">The <see cref="string"/> engine VIN code.</param>
        /// <param name="trimLevel">The <see cref="string"/> trim level.</param>
        /// <param name="aaia">The <see cref="string"/>AAIA number.</param>
        public PolkVehicle(string vinPatternMask, string manufacturer, string make, string model, int modelYear, string engineType, string engineVINCode, string trimLevel, string aaia)
        {
            this.vinPatternMask = vinPatternMask;
            this.manufacturer = manufacturer;
            this.make = make;
            this.model = model;
            this.modelYear = modelYear;
            this.engineType = engineType;
            this.engineVINCode = engineVINCode;
            this.trimLevel = trimLevel;
            this.aaia = aaia;
        }

        /// <summary>
        /// The <see cref="string"/> VIN pattern Mask.
        /// </summary>
        public string VINPatternMask
        {
            get
            {
                return this.vinPatternMask;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle manufacturer.
        /// </summary>
        public string Manufacturer
        {
            get
            {
                return this.manufacturer;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle make.
        /// </summary>
        public string Make
        {
            get
            {
                return this.make;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle model.
        /// </summary>
        public string Model
        {
            get
            {
                return this.model;
            }
        }

        /// <summary>
        /// The <see cref="int"/> vehicle year.
        /// </summary>
        public int ModelYear
        {
            get
            {
                return this.modelYear;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle engine type.
        /// </summary>
        public string EngineType
        {
            get
            {
                return this.engineType;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle engine VIN code.
        /// </summary>
        public string EngineVINCode
        {
            get
            {
                return this.engineVINCode;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle trim level.
        /// </summary>
        public string TrimLevel
        {
            get
            {
                return this.trimLevel;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle AAIA number.
        /// </summary>
        public string AAIA
        {
            get
            {
                return this.aaia;
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle manufacturer.
        /// </summary>
        public string ManufacturerLower
        {
            get
            {
                return this.Manufacturer.ToLower();
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle make.
        /// </summary>
        public string MakeLower
        {
            get
            {
                return this.Make.ToLower();
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle model.
        /// </summary>
        public string ModelLower
        {
            get
            {
                return this.Model.ToLower();
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle engine type.
        /// </summary>
        public string EngineTypeLower
        {
            get
            {
                return this.EngineType.ToLower();
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle engine type.
        /// </summary>
        public string EngineVINCodeLower
        {
            get
            {
                return this.EngineType.ToLower();
            }
        }

        /// <summary>
        /// The <see cref="string"/> vehicle trim level.
        /// </summary>
        public string TrimLevelLower
        {
            get
            {
                return this.TrimLevel.ToLower();
            }
        }
    }
}