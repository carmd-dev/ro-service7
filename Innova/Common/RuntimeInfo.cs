using Innova.Markets;
using Innova.Users;
using Metafuse3.BusinessObjects;
using Metafuse3.NullableTypes;
using System;
using System.Web;
using System.Web.Caching;

namespace Innova
{
    /// <summary>
    /// The RuntimeInfo class contains properties used for displaying info according to user-specific settings.
    /// </summary>
    public class RuntimeInfo
    {
        private Registry registry;
        private string currentRegion = null;
        private Currency? currentCurrency = null;
        private StateLaborRate currentStateLaborRate;
        private CurrencyExchangeRate currentCurrencyExchangeRate;
        private StateLaborRateCollection rates = null;

        //##Security
        /// <summary>
        /// The <see cref="string"/> encryption passphrase.
        /// </summary>
        public static string EncryptionPassphrase = "g7yoqIKVjTs19!&al5wN#hJsCggecd56NubJBqDs@71OM1@S!RcvoszfP2%7a00!76&kLIe5*Rst7af7";

        /// <summary>
        /// The <see cref="string"/> hash salt.
        /// </summary>
        public static string PassphraseSalt = "&al5w";

        //##Security

        /// <summary>
        /// Default constructor for the RuntimeInfo class
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        public RuntimeInfo(Registry registry)
        {
            this.registry = registry;
            this.CurrentMarket = Market.US;
            this.CurrentLanguage = Language.English;
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> current region.
        /// NOTE: This should be set before it is accessed, otherwise it will default to CurrentUser.Region, then CurrentAdminUser.State, then California (CA).
        /// </summary>
        public string CurrentRegion
        {
            get
            {
                if (String.IsNullOrEmpty(this.currentRegion))
                {
                    // Use California as the default
                    this.currentRegion = "CA";

                    if (this.CurrentUser != null && !String.IsNullOrEmpty(this.CurrentUser.Region))
                    {
                        // Use the current user's region
                        this.currentRegion = this.CurrentUser.Region;
                    }
                    else if (this.CurrentAdminUser != null && !String.IsNullOrEmpty(this.CurrentAdminUser.State))
                    {
                        // Use the current admin user's region
                        this.currentRegion = this.CurrentAdminUser.State;
                    }
                }

                return this.currentRegion;
            }
            set
            {
                if (this.currentRegion != value)
                {
                    // Since we are changing the region clear the current labor rate
                    this.currentStateLaborRate = null;

                    // Not set the region
                    this.currentRegion = value;
                }
            }
        }

        /// <summary>
        /// Get or sets the current <see cref="Market"/> to use
        /// </summary>
        public Market CurrentMarket { get; set; }

        /// <summary>
        /// Get or sets the current <see cref="Language"/> to use
        /// </summary>
        public Language CurrentLanguage { get; set; }

        /// <summary>
        /// Get or sets the current <see cref="Currency"/> to use
        /// NOTE: This should be set before it is accessed, otherwise it will default to CurrentStateLaborRate.Currency, which in.
        /// </summary>
        public Currency CurrentCurrency
        {
            get
            {
                if (!this.currentCurrency.HasValue)
                {
                    this.currentCurrency = this.CurrentStateLaborRate.Currency;
                }

                return this.currentCurrency.Value;
            }
            set
            {
                this.currentCurrency = value;
            }
        }

        /// <summary>
        /// Gets or sets the currently logged in <see cref="User"/>.
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Gets or sets the currently logged in <see cref="AdminUser"/>.
        /// </summary>
        public AdminUser CurrentAdminUser { get; set; }

        /// <summary>
        /// Gets a <see cref="StateLaborRateCollection"/> of all labor rates in the system.
        /// </summary>
        private StateLaborRateCollection StateLaborRates
        {
            get
            {
                if (this.rates == null)
                {
                    // If we're in a web app check to see if the rates collection exists in the cache
                    if (HttpContext.Current != null && HttpContext.Current.Cache["StateLaborRates"] != null)
                    {
                        this.rates = (StateLaborRateCollection)HttpContext.Current.Cache["StateLaborRates"];
                    }
                }

                // If rates are still null, then load them.
                if (this.rates == null)
                {
                    this.rates = StateLaborRate.GetAll(this.registry);

                    // If we are in a web app then store the collection in the cache.
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Cache.Add("StateLaborRates", this.rates, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    }
                }

                return this.rates;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="StateLaborRate"/> to be used for labor cost calculations.
        /// </summary>
        public StateLaborRate CurrentStateLaborRate
        {
            get
            {
                // If we don't have a rate yet then get it.
                if (this.currentStateLaborRate == null)
                {
                    // Get the rate for the current user's or admin user's region
                    this.currentStateLaborRate = (StateLaborRate)this.StateLaborRates.FindByProperty("StateCode", this.CurrentRegion);

                    // If no rate was found then default to CA rate.
                    if (this.currentStateLaborRate == null)
                    {
                        this.currentStateLaborRate = (StateLaborRate)this.StateLaborRates.FindByProperty("StateCode", "CA");
                    }
                }

                return this.currentStateLaborRate;
            }
            set
            {
                this.currentStateLaborRate = value;
            }
        }

        private CurrencyExchangeRateCollection CurrencyExchangeRates
        {
            get
            {
                CurrencyExchangeRateCollection rates = null;

                // If we're in a web app check to see if the rates collection exists in the cache
                if (HttpContext.Current != null && HttpContext.Current.Cache["CurrencyExchangeRates"] != null)
                {
                    rates = (CurrencyExchangeRateCollection)HttpContext.Current.Cache["CurrencyExchangeRates"];
                }

                // If rates are still null, then load them.
                if (rates == null)
                {
                    rates = CurrencyExchangeRate.GetMostRecentRates(this.registry);

                    // If we are in a web app then store the collection in the cache.
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.Cache.Add("CurrencyExchangeRates", rates, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                    }
                }

                return rates;
            }
        }

        /// <summary>
        /// Gets or sets the current <see cref="CurrentCurrencyExchangeRate"/>.
        /// </summary>
        public CurrencyExchangeRate CurrentCurrencyExchangeRate
        {
            get
            {
                // If we don't have a rate yet then get it.
                if (this.currentCurrencyExchangeRate == null)
                {
                    // Get the rate for the current currency
                    this.currentCurrencyExchangeRate = (CurrencyExchangeRate)this.CurrencyExchangeRates.FindByProperty("CurrencyISOCode", this.CurrentCurrency.ToString());

                    // If no rate was found then default to USD rate.
                    if (this.currentCurrencyExchangeRate == null)
                    {
                        this.currentCurrencyExchangeRate = (CurrencyExchangeRate)this.CurrencyExchangeRates.FindByProperty("CurrencyISOCode", "USD");
                    }
                }

                return this.currentCurrencyExchangeRate;
            }
            set
            {
                this.currentCurrencyExchangeRate = value;
            }
        }

        /// <summary>
        /// Gets the <see cref="decimal"/> state labor rate in US dollars.
        /// </summary>
        public decimal CurrentStateLaborRateInUSD
        {
            get
            {
                if (this.CurrentStateLaborRate.Currency == Currency.USD)
                {
                    return this.CurrentStateLaborRate.DollarsPerHour;
                }
                else
                {
                    return this.ConvertCurrency(this.CurrentStateLaborRate.Currency, this.CurrentStateLaborRate.DollarsPerHour, Currency.USD);
                }
            }
        }

        /// <summary>
        /// Gets the labor rate for the 2-letter region code specified.
        /// </summary>
        /// <param name="regionCode">The <see cref="string"/> 2-letter region code.</param>
        /// <returns>The <see cref="decimal"/> labor rate for the 2-letter region code specified.</returns>
        public decimal GetStateLaborRate(string regionCode)
        {
            decimal rateValue = 0m;

            StateLaborRate rate = (StateLaborRate)this.StateLaborRates.FindByProperty("StateCode", regionCode);

            if (rate != null)
            {
                rateValue = rate.DollarsPerHour;
            }

            return rateValue;
        }

        /// <summary>
        /// Sets the labor rate for the 2-letter region code specified.
        /// </summary>
        /// <param name="regionCode">The <see cref="string"/> 2-letter region code.</param>
        /// <returns>A <see cref="bool"/> indicating if the state labor was found and set for the region code provided.</returns>
        public bool SetCurrentStateLaborRate(string regionCode)
        {
            bool success = false;

            if (!String.IsNullOrEmpty(regionCode))
            {
                StateLaborRate rate = (StateLaborRate)this.StateLaborRates.FindByProperty("StateCode", regionCode);

                if (rate != null)
                {
                    this.currentStateLaborRate = rate;
                    success = true;
                }
            }

            return success;
        }

        /// <summary>
        /// Sets the current language from the supplied language string
        /// </summary>
        /// <param name="languageString"><see cref="string"/> language string</param>
        /// <returns><see cref="bool"/> flag indicating whether or not the method was successful in matching the supplied string</returns>
        public bool SetCurrentLanguage(string languageString)
        {
            if (!String.IsNullOrEmpty(languageString))
            {
                languageString = languageString.Trim();//#Ticket43-44-45

                if (languageString.ToLower() == "en-us")
                {
                    this.CurrentLanguage = Language.English;
                    return true;
                }
                // 2012-02-03 RG - The incorrect value "ex-mx" was initially in the code so to ensure backwards compatibility
                // the comparison will allow "es-mx" OR "ex-mx"
                else if (languageString.ToLower() == "es-mx" || languageString.ToLower() == "ex-mx")
                {
                    this.CurrentLanguage = Language.SpanishMX;
                    return true;
                }
                //#Ticket43-44-45
                else if (languageString.ToLower() == "fr-fr")
                {
                    this.CurrentLanguage = Language.French;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the current currency from the supplied currency integer
        /// </summary>
        /// <param name="integerValue"><see cref="int"/> enum value of the currency type</param>
        /// <returns><see cref="bool"/> flag indicating whether or not the method was successful in matching the supplied integer</returns>
        public bool SetCurrentCurrency(int? integerValue)
        {
            if (integerValue.HasValue && integerValue.Value >= 0 && integerValue.Value <= 4)
            {
                this.CurrentCurrency = (Currency)integerValue.Value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the current market from the supplied market string
        /// </summary>
        /// <param name="marketString">The <see cref="string"/> market string.</param>
        /// <returns>A <see cref="bool"/> flag indicating whether or not the method was successful in matching the supplied string.</returns>
        public bool SetCurrentMarket(string marketString)
        {
            Market? market = null;
            bool success = false;

            if (!String.IsNullOrEmpty(marketString))
            {
                try
                {
                    market = (Market)Enum.Parse(typeof(Market), marketString);
                }
                catch
                {
                    // DO NOTHING
                }
            }

            if (market.HasValue)
            {
                this.CurrentMarket = market.Value;
                success = true;
            }

            return success;
        }

        /// <summary>
        /// Gets a boolean value that indicates if a translated value exits for the CurrentLanguage specified.
        /// </summary>
        /// <param name="value_en">The value in English.</param>
        /// <param name="value_es">The value in Spanish.</param>
        /// <param name="value_fr">The value in French.</param>
        /// <param name="value_zh">The value in Mandarin Chinese.</param>
        /// <returns>A <see cref="Boolean"/> value that indicates if a translated value exits.</returns>
        public bool DoesTranslatedValueExist(string value_en, string value_es, string value_fr, string value_zh)
        {
            bool translatedValueExists = false;

            switch (this.CurrentLanguage)
            {
                case Language.English:
                    translatedValueExists = !String.IsNullOrEmpty(value_en);
                    break;

                case Language.SpanishMX:
                    translatedValueExists = !String.IsNullOrEmpty(value_es);
                    break;

                case Language.French:
                    translatedValueExists = !String.IsNullOrEmpty(value_fr);
                    break;

                case Language.Mandarin:
                    translatedValueExists = !String.IsNullOrEmpty(value_zh);
                    break;
            }

            return translatedValueExists;
        }

        /// <summary>
        /// Gets the correct translation to be used from one of the provided values and based on the CurrentLanguage specified.
        /// </summary>
        /// <param name="value_en">The value in English.</param>
        /// <param name="value_es">The value in Spanish.</param>
        /// <param name="value_fr">The value in French.</param>
        /// <param name="value_zh">The value in Mandarin Chinese.</param>
        /// <returns>One of the <see cref="string"/> values provided based on the CurrentLanguage.</returns>
        public string GetTranslatedValue(string value_en, string value_es, string value_fr, string value_zh)
        {
            string s = value_en;

            switch (this.CurrentLanguage)
            {
                case Language.English:
                    return s;

                case Language.SpanishMX:
                    s = value_es;
                    break;

                case Language.French:
                    s = value_fr;
                    break;

                case Language.Mandarin:
                    s = value_zh;
                    break;
            }

            if (String.IsNullOrEmpty(s))
            {
                s = value_en;
            }

            return s;
        }

        /// <summary>
        /// Gets a <see cref="decimal"/> currency value converted to the <see cref="CurrentCurrency"/> currecny currency from the provided US dollars value.
        /// </summary>
        /// <param name="usDollarsValue">The <see cref="decimal"/> value in US dollars.</param>
        /// <returns>
        /// A <see cref="decimal"/> currency value converted to the <see cref="CurrentCurrency"/> currecny currency from the provided US dollars value.
        /// </returns>
        public decimal GetLocalCurrencyValueFromUSDollars(decimal usDollarsValue)
        {
            return this.ConvertCurrency(Currency.USD, usDollarsValue, this.CurrentCurrency);
        }

        /// <summary>
        /// Gets a <see cref="decimal"/> currency value converted to US dollars from the <see cref="CurrentCurrency"/> currecny currency.
        /// </summary>
        /// <param name="localCurrencyValue">The <see cref="decimal"/> value in the current local currency.</param>
        /// <returns>
        /// A <see cref="decimal"/> currency value converted to US dollars from the <see cref="CurrentCurrency"/> currecny currency.
        /// </returns>
        public decimal GetUSDollarsFromLocalCurrencyValue(decimal localCurrencyValue)
        {
            return this.ConvertCurrency(this.CurrentCurrency, localCurrencyValue, Currency.USD);
        }

        /// <summary>
        /// Converts a currency value from one currency to another.
        /// </summary>
        /// <param name="sourceCurrencyType">The <see cref="Currency"/> type to convert from.</param>
        /// <param name="sourceCurrencyValue">The <see cref="decimal"/> currency value to be converted.</param>
        /// <param name="targetCurrencyType">The <see cref="Currency"/> type to convert to.</param>
        /// <returns>
        /// A <see cref="NullableDecimal"/> currency value in the desired currency.
        /// </returns>
        public decimal ConvertCurrency(Currency sourceCurrencyType, decimal sourceCurrencyValue, Currency targetCurrencyType)
        {
            decimal targetCurrencyValue;

            if (sourceCurrencyType == targetCurrencyType)
            {
                return sourceCurrencyValue;
            }
            else
            {
                //if both not equal, then we convert source to US, then to target
                if (targetCurrencyType != Currency.USD && sourceCurrencyType != Currency.USD)
                {
                    targetCurrencyValue = this.ConvertCurrency(sourceCurrencyType, sourceCurrencyValue, Currency.USD);

                    targetCurrencyValue = this.ConvertCurrency(Currency.USD, targetCurrencyValue, targetCurrencyType);
                }
                else
                {
                    CurrencyExchangeRate sourceRate =
                        (CurrencyExchangeRate)
                            this.CurrencyExchangeRates.FindByProperty("CurrencyISOCode", sourceCurrencyType.ToString());

                    // Get the rate for the current currency
                    CurrencyExchangeRate targetRate = (CurrencyExchangeRate)this.CurrencyExchangeRates.FindByProperty("CurrencyISOCode", targetCurrencyType.ToString());

                    //if(targetRate != null)
                    if (targetRate != null && sourceRate != null)
                    {
                        //todo: Nam changed on 3/17/2017
                        // If the target currency is USD then multiply the source value by the target rate. (WRONG)
                        // If the target currency is USD then multiply the source value by the SOURCE rate.
                        if (targetCurrencyType == Currency.USD) //So the source is not USD here
                        {
                            /*
                             * SourceRate(CAD)      TargetRate(USD)
                             * 1.37653              1.0
                             *
                             * Source Amount        Target Amount???
                             * 95.00 CAD            = (95.00*1.0)/1.37653
                             */
                            targetCurrencyValue = sourceCurrencyValue / sourceRate.ExchangeRatePerUSD;
                        }
                        //// otherwise the source currency must be USD, so divide the source value by the target rate.(WRONG)
                        //// otherwise the source currency must be USD, so MULTIPLY the source value by the target rate.
                        else
                        {
                            /*
                             * SourceRate(USD)      Targetrate(CAD)
                             * 1.0                  1.37653
                             *
                             * Source Amount        Target Amount???
                             * 108.36               = (108.36*1.37653)/1.0
                             */
                            targetCurrencyValue = sourceCurrencyValue * targetRate.ExchangeRatePerUSD;
                        }
                    }
                    else
                    {
                        //throw new ApplicationException("Unable to find the target rate to convert the currency");
                        throw new ApplicationException("Unable to find the target and source rates to convert the currency");
                    }
                }
            }

            return targetCurrencyValue;
        }
    }
}