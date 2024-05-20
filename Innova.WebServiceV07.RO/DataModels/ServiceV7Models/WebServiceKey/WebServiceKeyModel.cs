namespace Innova.WebServiceV07.RO.DataModels.ServiceV7Models.WebServiceKey
{
    public class WebServiceKeyModel
    {
        /// <summary>
        /// Gets or sets the <see cref="string"/> web service key.
        /// </summary>
        public string Key = "";

        /// <summary>
        /// Gets or sets the <see cref="string"/> language.  Valid choices are en-US and es-MX.   If unset en-US is used.
        /// </summary>
        public string LanguageString = "";

        /// <summary>
        /// Gets or sets the <see cref="string"/> region to use for the call.   If unset, CA is used.
        /// </summary>
        public string Region = "";

        /// <summary>
        /// Gets or sets the <see cref="int"/> currency to for the call.  If unset, 0 is used (USD)  0 = USD, 1 = Canadian, 2 = Euros,  3 = Chinese Yuan,  4 = Nuevo Peso MXN (Mexico)
        /// </summary>
        public int? Currency;

        /// <summary>
        /// Gets or sets the <see cref="string"/> 2-letter market abbreviation.  Valid values are CA = Canada, CN = China, MX = Mexico, US = United States. If unset, US is used.
        /// </summary>
        public string MarketString = "";
    }
}