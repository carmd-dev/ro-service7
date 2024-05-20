using Metafuse3.BusinessObjects;
using Metafuse3.Data.SqlClient;
using System;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Xml;

namespace Innova.Markets
{
    /// <summary>
    /// The CurrencyExchangeRate object handles the business logic and data access for the specialized business object, CurrencyExchangeRate.
    /// The class inherits from <see cref="BusinessObjectBase"/> class.
    /// </summary>
    /// <example>
    /// See the examples of how to construct the CurrencyExchangeRate object.
    ///
    /// To create a new instance of a new of CurrencyExchangeRate.
    /// <code>CurrencyExchangeRate o = (CurrencyExchangeRate)this.Registry.CreateInstance(typeof(CurrencyExchangeRate));</code>
    ///
    /// To create an new instance of an existing CurrencyExchangeRate.
    /// <code>CurrencyExchangeRate o = (CurrencyExchangeRate)this.Registry.CreateInstance(typeof(CurrencyExchangeRate), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));</code>
    /// </example>
    /// <remarks>
    /// In order to create an instance of CurrencyExchangeRate, you need to have a <see cref="Registry"/>
    /// object created, and have set the static member <see cref="BusinessObjectBase.ConnectionString"/> to the <see cref="BusinessObjectBase"/> and <see cref="BusinessObjectCollectionBase"/> objects.
    /// </remarks>
    [Serializable()]
    public class CurrencyExchangeRate : BusinessObjectBase
    {
        private string currencyName = "";
        private string currencyISOCode = "";
        private decimal exchangeRatePerUSD;
        private DateTime effectiveDateTimeUTC = DateTime.MinValue;

        #region Contructors

        /***********************************************************************************************
		 *
		 * System Constructors
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Constructor creates a new (one that does not exist, assigning a new <see cref="Guid"/> Id). CurrencyExchangeRate object.
        /// In order to create a new CurrencyExchangeRate which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// CurrencyExchangeRate o = (CurrencyExchangeRate)Registry.CreateInstance(typeof(CurrencyExchangeRate));
        /// </code>
        /// </example>
        protected internal CurrencyExchangeRate()
            : base()
        {
            this.IsObjectCreated = true;
            this.IsObjectLoaded = true;
            this.IsObjectDirty = true;
        }

        /// <summary>
        /// Constructor creates an existing (one that exists, and already has a <see cref="Guid"/> Id).  CurrencyExchangeRate object.
        /// In order to create an existing CurrencyExchangeRate object, which inherits from <see cref="BusinessObjectBase"/>, you need to use the <see cref="Registry"/>.
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> or SQL uniqueidentifier indentification "Id" of the current object.</param>
        /// <example>
        /// <code>
        /// CurrencyExchangeRate o = (CurrencyExchangeRate)Registry.CreateInstance(typeof(CurrencyExchangeRate), new Guid("{250426B4-3C7A-463c-93C4-045668C8A1AA}"));
        /// </code>
        /// </example>
        protected internal CurrencyExchangeRate(Guid id)
            : base(id)
        {
            this.id = id;
        }

        #endregion Contructors

        #region System Properties DO NOT EDIT

        // private member variables used to handle the system properties.
        private bool isObjectDirty = false;

        private bool isObjectLoaded = false;
        private bool isObjectCreated = false;
        private StringCollection updatedFields = null;
        /*****************************************************************************************
		 *
		 * System Properties: DO NOT EDIT
		 *
		 * **************************************************************************************/

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been loaded from the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectLoaded property.
        /// Base layers may or may not be loaded.  The IsObjectLoaded propery is automatically set to true when the object is loaded from the database.
        /// The IsObjectLoaded property is used primarily for the internal Load methods to determine whether or not the object needs to load itself when a property is accessed or the Load method is invoked.
        /// </summary>
        public new bool IsObjectLoaded
        {
            get
            {
                return this.isObjectLoaded;
            }
            set
            {
                this.isObjectLoaded = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has been updated and needs to be saved to the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectDirty property.
        /// Base layers may or may not be dirty.  The IsObjectDirty flag should set to true when a property is updated, and the object automatically sets the IsObjectDirty flag to false when the object is saved successfully.
        /// The IsObjectDirty property is used primarly for the internal Save methods to determine whether or not the object needs to save itself when the Save method is invoked.
        /// </summary>
        public new bool IsObjectDirty
        {
            get
            {
                return this.isObjectDirty;
            }
            set
            {
                this.isObjectDirty = value;

                if (!this.isObjectDirty)
                {
                    this.isObjectCreated = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="bool"/> flag which determines whether or not the current object has just been created (is new) and will need to be saved to the database.
        /// The property is marked new so that each layer of the inheritence chain has it's own IsObjectCreated property.
        /// The IsObjectCreated flag is automatically set to false when the object is saved.
        /// Base layers may or may not be created.  The IsObjectCreated flag is set to false when saved.
        /// </summary>
        public new bool IsObjectCreated
        {
            get
            {
                return this.isObjectCreated;
            }
            set
            {
                this.isObjectCreated = value;
            }
        }

        /// <summary>
        /// Adds an updated field to the collection of updated fields used to create the update statement for the object.
        /// </summary>
        /// <param name="databaseField"><see cref="string"/> updated database field to add.</param>
        protected internal new void UpdatedField(string databaseField)
        {
            //if this is not a created object, then we need to keep track of the updated list
            if (!this.isObjectCreated)
            {
                if (this.updatedFields == null)
                {
                    this.updatedFields = new StringCollection();
                }

                if (this.updatedFields.Contains(databaseField.ToLower()) == false)
                {
                    this.updatedFields.Add(databaseField.ToLower());
                }
            }
        }

        #endregion System Properties DO NOT EDIT

        #region Public Properties
        /**************************************************************************************
		 *
		 * Object Properties: Add Custom Fields
		 *
		 * **************************************************************************************/

        /// <summary>
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
        public string CurrencyName
        {
            get
            {
                this.EnsureLoaded();
                return this.currencyName;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.currencyName)
                {
                    this.IsObjectDirty = true;
                    this.currencyName = value;
                    this.UpdatedField("CurrencyName");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="string"/> .
        /// </summary>
        public string CurrencyISOCode
        {
            get
            {
                this.EnsureLoaded();
                return this.currencyISOCode;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.currencyISOCode)
                {
                    this.IsObjectDirty = true;
                    this.currencyISOCode = value;
                    this.UpdatedField("CurrencyISOCode");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="decimal"/> .
        /// </summary>
        public decimal ExchangeRatePerUSD
        {
            get
            {
                this.EnsureLoaded();
                return this.exchangeRatePerUSD;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.exchangeRatePerUSD)
                {
                    this.IsObjectDirty = true;
                    this.exchangeRatePerUSD = value;
                    this.UpdatedField("ExchangeRatePerUSD");
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DateTime"/> .
        /// </summary>
        public DateTime EffectiveDateTimeUTC
        {
            get
            {
                this.EnsureLoaded();
                return this.effectiveDateTimeUTC;
            }
            set
            {
                this.EnsureLoaded();
                if (value != this.effectiveDateTimeUTC)
                {
                    this.IsObjectDirty = true;
                    this.effectiveDateTimeUTC = value;
                    this.UpdatedField("EffectiveDateTimeUTC");
                }
            }
        }

        #endregion Public Properties

        #region Object Properties (Related Objects)
        /*****************************************************************************************
		 *
		 * Object Relationships: Add correlated Object Collections
		 *
		*******************************************************************************************/

        #endregion Object Properties (Related Objects)

        #region Business Logic Methods

        /***********************************************************************************************
		 *
		 * Custom Business Logic
		 *
		 * **********************************************************************************************/

        /// <summary>
        /// Gets a collection of the most recent rates for each currency that exists in the db.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <returns>A <see cref="CurrencyExchangeRateCollection"/> of <see cref="CurrencyExchangeRate"/> objects.</returns>
        public static CurrencyExchangeRateCollection GetMostRecentRates(Registry registry)
        {
            CurrencyExchangeRateCollection rates = new CurrencyExchangeRateCollection(registry);

            SqlProcedureCommand command = new SqlProcedureCommand();
            command.ProcedureName = "CurrencyExchangeRate_LoadMostRecent";

            rates.Load(command, "CurrencyExchangeRateId", true, true, false);

            return rates;
        }

        /// <summary>
        /// Imports the currency exchange rates from the provided XML doc.
        /// </summary>
        /// <param name="registry">The <see cref="Registry"/> currently in use.</param>
        /// <param name="currencyXml">The <see cref="XmlDocument"/> that contains the currency exchange rate data to be imported.</param>
        public static void ImportCurrencyExchangeRates(Registry registry, XmlDocument currencyXml)
        {
            // Setup the namespace manager
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(currencyXml.NameTable);
            string nsPrefix = "ratesns";
            namespaceManager.AddNamespace(nsPrefix, currencyXml.DocumentElement.NamespaceURI);

            // Look for a returncode node. If we get one then that means there was an error.
            XmlNode returnCodeNode = currencyXml.SelectSingleNode("//" + nsPrefix + ":returncode", namespaceManager);
            if (returnCodeNode != null)
            {
                // Get the value.
                string returnCode = returnCodeNode.InnerText;

                // Now look for a returnstring (error message) node.
                XmlNode returnStringNode = currencyXml.SelectSingleNode("//" + nsPrefix + ":returnstring", namespaceManager);
                string returnString = "";
                if (returnStringNode != null)
                {
                    // Get the value.
                    returnString = returnStringNode.InnerText;
                }

                // Create the error message.
                string errorMessage = "The currency service returned an error";

                if (returnString != "")
                {
                    // Add the message returned from the web service.
                    errorMessage += ": " + returnString;
                }

                // Throw an exception since we can't proceed.
                throw new ApplicationException(errorMessage);
            }

            string application = "";
            string licenseKey = "";
            string serviceExpiration = "";
            string language = "";
            string titleStyle = "";
            string requestBaseCurrency = "";
            string requestDateTime = "";
            string type = "";

            // Attempt the get the header values. These are not critical so if any one of them is missing just keep going.
            try { application = currencyXml.SelectSingleNode("//" + nsPrefix + ":application", namespaceManager).InnerText; } catch { }
            try { licenseKey = currencyXml.SelectSingleNode("//" + nsPrefix + ":licensekey", namespaceManager).InnerText; } catch { }
            try { serviceExpiration = currencyXml.SelectSingleNode("//" + nsPrefix + ":serviceexpiration", namespaceManager).InnerText; } catch { }
            try { language = currencyXml.SelectSingleNode("//" + nsPrefix + ":language", namespaceManager).InnerText; } catch { }
            try { titleStyle = currencyXml.SelectSingleNode("//" + nsPrefix + ":titlestyle", namespaceManager).InnerText; } catch { }
            try { requestBaseCurrency = currencyXml.SelectSingleNode("//" + nsPrefix + ":basecurrency", namespaceManager).InnerText; } catch { }
            try { requestDateTime = currencyXml.SelectSingleNode("//" + nsPrefix + ":time", namespaceManager).InnerText; } catch { }
            try { type = currencyXml.SelectSingleNode("//" + nsPrefix + ":type", namespaceManager).InnerText; } catch { }

            // Get a collection of all the currency nodes
            XmlNodeList currencyNodes = currencyXml.SelectNodes("//" + nsPrefix + ":currency", namespaceManager);

            foreach (XmlNode node in currencyNodes)
            {
                string baseCurrency = "";
                string code = "";
                decimal rate = decimal.MinValue;
                DateTime time = DateTime.MinValue;
                string name = "";

                bool successfullyReadXmlNode = false;

                // Attempt the read the values from this node.
                try
                {
                    baseCurrency = node.SelectSingleNode("./" + nsPrefix + ":basecurrency", namespaceManager).InnerText;
                    code = node.SelectSingleNode("./" + nsPrefix + ":code", namespaceManager).InnerText;
                    rate = decimal.Parse(node.SelectSingleNode("./" + nsPrefix + ":rate", namespaceManager).InnerText);
                    time = DateTime.Parse(node.SelectSingleNode("./" + nsPrefix + ":time", namespaceManager).InnerText);
                    name = node.SelectSingleNode("./" + nsPrefix + ":name", namespaceManager).InnerText;

                    // There were no exceptions, so set the flag to true
                    successfullyReadXmlNode = true;
                }
                catch { }

                // If the currency node was successfully read then save the values.
                if (successfullyReadXmlNode)
                {
                    CurrencyExchangeRate cer = (CurrencyExchangeRate)registry.CreateInstance(typeof(CurrencyExchangeRate));
                    cer.CurrencyISOCode = code;
                    cer.CurrencyName = name;
                    cer.EffectiveDateTimeUTC = time;
                    cer.ExchangeRatePerUSD = rate;
                    cer.Save();
                }
            }
        }

        /*
		public static void TestCurrencyImport(Registry registry)
		{
			# region Create A Sample XML Document

			string xmlString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<rates xmlns=""http://fx.currencysystem.com"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xsi:schemaLocation=""http://fx.currencysystem.com http://fx.currencysystem.com/xsd/rates-v5.xsd"">
<application>Cloanto(R) Currency Server(TM)</application>
<licensekey>22222-44444-22222-44444</licensekey>
<serviceexpiration>2100-12-31T23:59:59Z</serviceexpiration>
<language>en</language>
<titlestyle>true</titlestyle>
<basecurrency>USD</basecurrency>
<time>2011-09-20T19:55:18Z</time>
<type>daily_mid_rates</type>
<currency><basecurrency>USD</basecurrency><code>AED</code><rate>3.6692801736826</rate><time>2011-09-20T16:55:16Z</time><name>UAE Dirham</name></currency>
<currency><basecurrency>USD</basecurrency><code>ARS</code><rate>4.4339696624589</rate><time>2011-09-20T16:55:16Z</time><name>Argentine Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>AUD</code><rate>0.9724288840263</rate><time>2011-09-20T12:55:17Z</time><name>Australian Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>BBD</code><rate>2.107950401167</rate><time>2011-09-20T12:55:17Z</time><name>Barbados Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>BDT</code><rate>74.906382202772</rate><time>2011-09-20T12:55:17Z</time><name>Bangladeshi Taka</name></currency>
<currency><basecurrency>USD</basecurrency><code>BGN</code><rate>1.4265499635303</rate><time>2011-09-20T12:55:17Z</time><name>Bulgarian Lev</name></currency>
<currency><basecurrency>USD</basecurrency><code>BOB</code><rate>7.2408460977389</rate><time>2011-09-20T12:55:17Z</time><name>Bolivian Boliviano</name></currency>
<currency><basecurrency>USD</basecurrency><code>BRL</code><rate>1.7787746170678</rate><time>2011-09-20T12:55:17Z</time><name>Brazilian Real</name></currency>
<currency><basecurrency>USD</basecurrency><code>BSD</code><rate>0.9990633966549</rate><time>2011-09-20T16:55:16Z</time><name>Bahamian Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>BWP</code><rate>7.1377552397666</rate><time>2011-09-20T19:55:18Z</time><name>Botswana Pula</name></currency>
<currency><basecurrency>USD</basecurrency><code>BZD</code><rate>2.092633114515</rate><time>2011-09-20T12:55:17Z</time><name>Belize Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>CAD</code><rate>0.9905178701678</rate><time>2011-09-20T12:55:17Z</time><name>Canadian Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>CDF</code><rate>918.39555069292</rate><time>2011-09-20T12:55:17Z</time><name>Congolese Franc</name></currency>
<currency><basecurrency>USD</basecurrency><code>CHF</code><rate>0.8799416484318</rate><time>2011-09-20T12:55:17Z</time><name>Swiss Franc</name></currency>
<currency><basecurrency>USD</basecurrency><code>CLP</code><rate>489.35308934851</rate><time>2011-09-20T16:55:16Z</time><name>Chilean Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>CNY</code><rate>6.3843180160467</rate><time>2011-09-20T12:55:17Z</time><name>Chinese Yuan Renminbi</name></currency>
<currency><basecurrency>USD</basecurrency><code>COP</code><rate>1846.237232992</rate><time>2011-09-20T16:55:16Z</time><name>Colombian Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>CRC</code><rate>539.89788475565</rate><time>2011-09-20T12:55:17Z</time><name>Costa Rican Colon</name></currency>
<currency><basecurrency>USD</basecurrency><code>CVE</code><rate>80.417213712619</rate><time>2011-09-20T12:55:17Z</time><name>Cape Verde Escudo</name></currency>
<currency><basecurrency>USD</basecurrency><code>CZK</code><rate>17.981765134938</rate><time>2011-09-20T12:55:17Z</time><name>Czech Koruna</name></currency>
<currency><basecurrency>USD</basecurrency><code>DKK</code><rate>5.4318745441284</rate><time>2011-09-20T12:55:17Z</time><name>Danish Krone</name></currency>
<currency><basecurrency>USD</basecurrency><code>DOP</code><rate>40.309263311452</rate><time>2011-09-20T12:55:17Z</time><name>Dominican Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>DZD</code><rate>73.768417213713</rate><time>2011-09-20T12:55:17Z</time><name>Algerian Dinar</name></currency>
<currency><basecurrency>USD</basecurrency><code>EEK</code><rate>11.412545587163</rate><time>2011-09-20T12:55:17Z</time><name>Estonian Kroon</name></currency>
<currency><basecurrency>USD</basecurrency><code>EGP</code><rate>5.9369803063457</rate><time>2011-09-20T12:55:17Z</time><name>Egyptian Pound</name></currency>
<currency><basecurrency>USD</basecurrency><code>EUR</code><rate>0.7293946024799</rate><time>2011-09-20T12:55:17Z</time><name>EU Euro</name></currency>
<currency><basecurrency>USD</basecurrency><code>FJD</code><rate>1.7748568946032</rate><time>2011-09-20T16:55:16Z</time><name>Fiji Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>GBP</code><rate>0.6362509117433</rate><time>2011-09-20T12:55:17Z</time><name>British Pound</name></currency>
<currency><basecurrency>USD</basecurrency><code>GHS</code><rate>1.5634841485807</rate><time>2011-09-20T16:55:16Z</time><name>Ghanaian New Cedi</name></currency>
<currency><basecurrency>USD</basecurrency><code>GTQ</code><rate>7.9123996784996</rate><time>2011-09-20T16:55:16Z</time><name>Guatemalan Quetzal</name></currency>
<currency><basecurrency>USD</basecurrency><code>GYD</code><rate>212.90299051787</rate><time>2011-09-20T12:55:17Z</time><name>Guyana Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>HKD</code><rate>7.7935083880379</rate><time>2011-09-20T12:55:17Z</time><name>Hong Kong SAR Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>HNL</code><rate>19.857038657914</rate><time>2011-09-20T12:55:17Z</time><name>Honduran Lempira</name></currency>
<currency><basecurrency>USD</basecurrency><code>HRK</code><rate>5.454704595186</rate><time>2011-09-20T12:55:17Z</time><name>Croatian Kuna</name></currency>
<currency><basecurrency>USD</basecurrency><code>HTG</code><rate>42.522975929978</rate><time>2011-09-20T12:55:17Z</time><name>Haitian Gourde</name></currency>
<currency><basecurrency>USD</basecurrency><code>HUF</code><rate>211.765134938</rate><time>2011-09-20T12:55:17Z</time><name>Hungarian Forint</name></currency>
<currency><basecurrency>USD</basecurrency><code>IDR</code><rate>8902.1735959154</rate><time>2011-09-20T12:55:17Z</time><name>Indonesian Rupiah</name></currency>
<currency><basecurrency>USD</basecurrency><code>ILS</code><rate>3.6760218935369</rate><time>2011-09-20T16:55:16Z</time><name>Israeli New Shekel</name></currency>
<currency><basecurrency>USD</basecurrency><code>INR</code><rate>48.054704595186</rate><time>2011-09-20T12:55:17Z</time><name>Indian Rupee</name></currency>
<currency><basecurrency>USD</basecurrency><code>ISK</code><rate>116.52904139584</rate><time>2011-09-20T16:55:16Z</time><name>Icelandic Krona</name></currency>
<currency><basecurrency>USD</basecurrency><code>JMD</code><rate>85.689638779643</rate><time>2011-09-20T16:55:16Z</time><name>Jamaican Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>JPY</code><rate>76.528081692195</rate><time>2011-09-20T12:55:17Z</time><name>Japanese Yen</name></currency>
<currency><basecurrency>USD</basecurrency><code>KMF</code><rate>358.8386214442</rate><time>2011-09-20T12:55:17Z</time><name>Comorian Franc</name></currency>
<currency><basecurrency>USD</basecurrency><code>KRW</code><rate>1143.3843909555</rate><time>2011-09-20T12:55:17Z</time><name>South Korean Won</name></currency>
<currency><basecurrency>USD</basecurrency><code>LKR</code><rate>109.96330025826</rate><time>2011-09-20T16:55:16Z</time><name>Sri Lanka Rupee</name></currency>
<currency><basecurrency>USD</basecurrency><code>LTL</code><rate>2.5184536834427</rate><time>2011-09-20T12:55:17Z</time><name>Lithuanian Litas</name></currency>
<currency><basecurrency>USD</basecurrency><code>LVL</code><rate>0.5172866520788</rate><time>2011-09-20T12:55:17Z</time><name>Latvian Lats</name></currency>
<currency><basecurrency>USD</basecurrency><code>LYD</code><rate>1.2237053245806</rate><time>2011-09-20T12:55:17Z</time><name>Libyan Dinar</name></currency>
<currency><basecurrency>USD</basecurrency><code>MAD</code><rate>8.2004099163961</rate><time>2011-09-20T16:55:16Z</time><name>Moroccan Dirham</name></currency>
<currency><basecurrency>USD</basecurrency><code>MXN</code><rate>13.072283005106</rate><time>2011-09-20T12:55:17Z</time><name>Mexican Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>MYR</code><rate>3.1194748358862</rate><time>2011-09-20T12:55:17Z</time><name>Malaysian Ringgit</name></currency>
<currency><basecurrency>USD</basecurrency><code>NAD</code><rate>7.417943107221</rate><time>2011-09-20T12:55:17Z</time><name>Namibian Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>NIO</code><rate>23.819110138585</rate><time>2011-09-20T12:55:17Z</time><name>Nicaraguan Cordoba Oro</name></currency>
<currency><basecurrency>USD</basecurrency><code>NOK</code><rate>5.6611962071481</rate><time>2011-09-20T12:55:17Z</time><name>Norwegian Krone</name></currency>
<currency><basecurrency>USD</basecurrency><code>NZD</code><rate>1.2131291028446</rate><time>2011-09-20T12:55:17Z</time><name>New Zealand Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>PAB</code><rate>1.0539752005835</rate><time>2011-09-20T12:55:17Z</time><name>Panamanian Balboa</name></currency>
<currency><basecurrency>USD</basecurrency><code>PEN</code><rate>2.7387245479007</rate><time>2011-09-20T16:55:16Z</time><name>Peruvian Nuevo Sol</name></currency>
<currency><basecurrency>USD</basecurrency><code>PHP</code><rate>43.477753464624</rate><time>2011-09-20T12:55:17Z</time><name>Philippine Peso</name></currency>
<currency><basecurrency>USD</basecurrency><code>PKR</code><rate>87.737138756594</rate><time>2011-09-20T16:55:16Z</time><name>Pakistani Rupee</name></currency>
<currency><basecurrency>USD</basecurrency><code>PLN</code><rate>3.1845368344274</rate><time>2011-09-20T12:55:17Z</time><name>Polish Zloty</name></currency>
<currency><basecurrency>USD</basecurrency><code>PYG</code><rate>4105.2516411379</rate><time>2011-09-20T12:55:17Z</time><name>Paraguayan Guaraní</name></currency>
<currency><basecurrency>USD</basecurrency><code>QAR</code><rate>3.6137126185266</rate><time>2011-09-20T12:55:17Z</time><name>Qatari Rial</name></currency>
<currency><basecurrency>USD</basecurrency><code>RON</code><rate>3.1254558716266</rate><time>2011-09-20T12:55:17Z</time><name>Romanian New Leu</name></currency>
<currency><basecurrency>USD</basecurrency><code>RUB</code><rate>31.463530269876</rate><time>2011-09-20T12:55:17Z</time><name>Russian Ruble</name></currency>
<currency><basecurrency>USD</basecurrency><code>SAR</code><rate>3.7499999731342</rate><time>2011-09-20T19:55:18Z</time><name>Saudi Riyal</name></currency>
<currency><basecurrency>USD</basecurrency><code>SEK</code><rate>6.6519328956966</rate><time>2011-09-20T12:55:17Z</time><name>Swedish Krona</name></currency>
<currency><basecurrency>USD</basecurrency><code>SGD</code><rate>1.2579868708972</rate><time>2011-09-20T12:55:17Z</time><name>Singapore Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>SRD</code><rate>3.4781181619256</rate><time>2011-09-20T12:55:17Z</time><name>Suriname Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>THB</code><rate>30.420131291028</rate><time>2011-09-20T12:55:17Z</time><name>Thai Baht</name></currency>
<currency><basecurrency>USD</basecurrency><code>TND</code><rate>1.4175457423214</rate><time>2011-09-20T16:55:16Z</time><name>Tunisian Dinar</name></currency>
<currency><basecurrency>USD</basecurrency><code>TRY</code><rate>1.7819839533187</rate><time>2011-09-20T12:55:17Z</time><name>Turkish Lira</name></currency>
<currency><basecurrency>USD</basecurrency><code>TTD</code><rate>6.3840121303369</rate><time>2011-09-20T16:55:16Z</time><name>Trinidad and Tobago Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>TWD</code><rate>29.862348952015</rate><time>2011-09-20T16:55:16Z</time><name>Taiwan New Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>UAH</code><rate>8.4303428154632</rate><time>2011-09-20T12:55:17Z</time><name>Ukrainian Hryvnia</name></currency>
<currency><basecurrency>USD</basecurrency><code>UGX</code><rate>2974.3253099927</rate><time>2011-09-20T12:55:17Z</time><name>Uganda New Shilling</name></currency>
<currency><basecurrency>USD</basecurrency><code>USD</code><rate>1</rate><time>2011-09-20T12:55:17Z</time><name>US Dollar</name></currency>
<currency><basecurrency>USD</basecurrency><code>UYU</code><rate>19.603938730853</rate><time>2011-09-20T12:55:17Z</time><name>Uruguayan Peso Uruguayo</name></currency>
<currency><basecurrency>USD</basecurrency><code>VEF</code><rate>4.2900141149802</rate><time>2011-09-20T16:55:16Z</time><name>Venezuelan Bolivar Fuerte</name></currency>
<currency><basecurrency>USD</basecurrency><code>VND</code><rate>20654.778932562</rate><time>2011-09-20T16:55:16Z</time><name>Vietnamese Dong</name></currency>
<currency><basecurrency>USD</basecurrency><code>XAF</code><rate>478.45149525894</rate><time>2011-09-20T12:55:17Z</time><name>CFA Franc BEAC</name></currency>
<currency><basecurrency>USD</basecurrency><code>XDR</code><rate>0.6363187454413</rate><time>2011-09-20T19:55:18Z</time><name>IMF Special Drawing Right</name></currency>
<currency><basecurrency>USD</basecurrency><code>XOF</code><rate>478.45149525894</rate><time>2011-09-20T12:55:17Z</time><name>CFA Franc BCEAO</name></currency>
<currency><basecurrency>USD</basecurrency><code>XPF</code><rate>87.039928665208</rate><time>2011-09-20T12:55:17Z</time><name>CFP Franc</name></currency>
<currency><basecurrency>USD</basecurrency><code>ZAR</code><rate>7.6474835886214</rate><time>2011-09-20T12:55:17Z</time><name>South African Rand</name></currency>
<currency><basecurrency>USD</basecurrency><code>ZMK</code><rate>5229.8322392414</rate><time>2011-09-20T12:55:17Z</time><name>Zambian Kwacha</name></currency>
</rates>
";
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xmlString);

			#endregion Business Logic Methods

			CurrencyExchangeRate.ImportCurrencyExchangeRates(registry, xmlDoc);
		}
		*/

        #endregion

        #region Required Methods (Load, Save, Delete, Etc)

        /***********************************************************************************************
		 *
		 * Required Object Methods (Load, Save, Save Collections, Delete)
		 *
		 * **********************************************************************************************/
        // Edit Required Object Methods

        #region System Load Calls (DO NOT EDIT)

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        public new void Load()
        {
            this.Load(null, null, false);
        }

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
        /// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
        /// <returns><see cref="SqlConnection"/> supplied or the one created internally.</returns>
        public new SqlConnection Load(SqlConnection connection, bool isLoadBase)
        {
            this.Load(connection, null, isLoadBase);

            return connection;
        }

        /// <summary>
        /// Loads the data for the current object (this layer in the inheritence chain) if <see cref="IsObjectLoaded"/> is false.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used (if any), if null, a new <see cref="SqlConnection"/> is created to perform the operation.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used (if any), otherwise if set to null the command will be executed outside the contect of a current transaction.</param>
        /// <param name="isLoadBase"><see cref="bool"/> when set to true, base layers (if any) will also be loaded.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used (if any) to access the database.  If none, returns null.</returns>
        public new SqlTransaction Load(SqlConnection connection, SqlTransaction transaction, bool isLoadBase)
        {
            this.EnsureValidId();

            if (isLoadBase)
            {
                //load the base item if user selected it.
                transaction = base.Load(connection, transaction, isLoadBase);
            }

            if (!this.IsObjectLoaded)
            {
                SqlDataReaderWrapper dr;
                if (connection == null)
                {
                    dr = new SqlDataReaderWrapper(ConnectionString);
                }
                else
                {
                    dr = new SqlDataReaderWrapper(connection, false);
                }

                using (dr)
                {
                    this.SetLoadProcedureCall(dr);

                    if (transaction == null)
                    {
                        dr.Execute();
                    }
                    else
                    {
                        dr.Execute(transaction);
                    }

                    if (dr.Read())
                    {
                        this.LoadPropertiesFromDataReader(dr, isLoadBase);
                    }
                    else
                    {
                        throw (new ApplicationException("Load Failed for type " + this.GetType().ToString() + " using Procedure: " + dr.ProcedureCall));
                    }
                }
            }

            return transaction;
        }

        /// <summary>
        /// Loads all the properties of this object from <see cref="SqlDataReaderWrapper"/> supplied.
        /// This method calls the protected internal method <see cref="SetPropertiesFromDataReader"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> recordset containing the fields required to set the properties of this object.</param>
        /// <param name="isSetBase"><see cref="bool"/> determines whether or not to load base layers (if any) of the object.  Set to true if the recordset contains the fields necessary to load the properties of base layers of this object.</param>
        public new void LoadPropertiesFromDataReader(SqlDataReaderWrapper dr, bool isSetBase)
        {
            if (isSetBase)
            {
                base.LoadPropertiesFromDataReader(dr, isSetBase);
            }

            if (!IsObjectLoaded)
            {
                this.SetPropertiesFromDataReader(dr);
            }

            this.IsObjectLoaded = true;
        }

        /// <summary>
        /// Method ensures the object is loaded.  This method is located in the get portion of the a property representing data in the database and is called there.  If the object's <see cref="IsObjectLoaded"/> property is false and the <see cref="IsObjectCreated"/> property is false, then the <see cref="Load()"/> method is invoked.
        /// </summary>
        protected new void EnsureLoaded()
        {
            if (!this.IsObjectLoaded && !this.IsObjectCreated)
            {
                this.Load();
            }
        }

        #endregion

        /// <summary>
        /// Sets the base load procedure call and parameters to the supplied <see cref="SqlDataReaderWrapper"/>, to be executed.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> to set the procedure call and parameters to.</param>
        protected new void SetLoadProcedureCall(SqlDataReaderWrapper dr)
        {
            dr.ProcedureName = "CurrencyExchangeRate_Load";
            dr.AddGuid("CurrencyExchangeRateId", this.Id);
        }

        /// <summary>
        /// Sets the properties of this object from the fields in the recordset located in the supplied <see cref="SqlDataReaderWrapper"/>.
        /// </summary>
        /// <param name="dr"><see cref="SqlDataReaderWrapper"/> containing the recordset and fields used to set the properties of this object (this layer only).</param>
        protected new void SetPropertiesFromDataReader(SqlDataReaderWrapper dr)
        {
            this.currencyName = dr.GetString("CurrencyName");
            this.currencyISOCode = dr.GetString("CurrencyISOCode");
            this.exchangeRatePerUSD = dr.GetDecimal("ExchangeRatePerUSD");
            this.effectiveDateTimeUTC = dr.GetDateTime("EffectiveDateTimeUTC");

            this.IsObjectLoaded = true;
        }

        /// <summary>
        /// Saves the current object, all base layers, and all related collections (calls <see cref="SaveCollections"/>).
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used if any.  If null is supplied a new <see cref="SqlConnection"/> is created.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        public override SqlTransaction Save(SqlConnection connection, SqlTransaction transaction)
        {
            // Call base save method of base class.
            transaction = base.Save(connection, transaction);

            //Custom save business logic here. Modify procedure name.
            if (this.IsObjectDirty)
            {
                transaction = this.EnsureDatabasePrepared(connection, transaction);

                using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
                {
                    if (this.IsObjectCreated)
                    {
                        dr.ProcedureName = "CurrencyExchangeRate_Create";
                        //Remove the lines below if they are not needed.
                        //this.CreatedDateTimeUTC = DateTime.UtcNow;
                        //this.UpdatedDateTimeUTC = this.CreatedDateTimeUTC;
                    }
                    else
                    {
                        dr.UpdateFields("CurrencyExchangeRate", "CurrencyExchangeRateId", this.updatedFields);
                    }

                    dr.AddGuid("CurrencyExchangeRateId", this.Id);
                    dr.AddNVarChar("CurrencyName", this.CurrencyName);
                    dr.AddNVarChar("CurrencyISOCode", this.CurrencyISOCode);
                    dr.AddDecimal("ExchangeRatePerUSD", this.ExchangeRatePerUSD);
                    dr.AddDateTime("EffectiveDateTimeUTC", this.EffectiveDateTimeUTC);

                    dr.Execute(transaction);
                }

                this.IsObjectDirty = false;
            }

            // call the save collections method
            transaction = this.SaveCollections(connection, transaction);

            return transaction;
        }

        /// <summary>
        /// Saves the related collections (normally set as properties) that the object needs to save to maintain integrity.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        protected new SqlTransaction SaveCollections(SqlConnection connection, SqlTransaction transaction)
        {
            // Customized related object collection saving business logic.

            return transaction;
        }

        /// <summary>
        /// Deletes the object, base layers, and related collections necessary to delete the object.
        /// </summary>
        /// <param name="connection"><see cref="SqlConnection"/> currently being used, if null a <see cref="SqlConnection"/> is created.</param>
        /// <param name="transaction"><see cref="SqlTransaction"/> currently being used, if null a <see cref="SqlTransaction"/> is created.</param>
        /// <returns><see cref="SqlTransaction"/> currently being used.</returns>
        public override SqlTransaction Delete(SqlConnection connection, SqlTransaction transaction)
        {
            this.EnsureValidId();

            transaction = this.EnsureDatabasePrepared(connection, transaction);

            // Custom delete business logic here.

            // delete this current object
            using (SqlDataReaderWrapper dr = new SqlDataReaderWrapper(connection, false))
            {
                //delete the item
                dr.ProcedureName = "CurrencyExchangeRate_Delete";
                dr.AddGuid("CurrencyExchangeRateId", this.Id);

                dr.ExecuteNonQuery(transaction);
            }

            transaction = base.Delete(connection, transaction);

            return transaction;
        }

        #endregion
    }
}