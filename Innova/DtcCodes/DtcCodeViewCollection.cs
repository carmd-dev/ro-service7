using System.Collections.Specialized;

namespace Innova.DtcCodes
{
    /// <summary>
    /// Summary description for DtcCodeViewCollection.
    /// </summary>
    public class DtcCodeViewCollection : Metafuse3.BusinessObjects.SmartCollectionBase
    {
        /// <summary>
        /// Creates a new instance of a DtcCodeViewCollection
        /// </summary>
        public DtcCodeViewCollection()
        {
        }

        /// <summary>
        /// Gets the <see cref="StringCollection"/> of all DTCs.
        /// </summary>
        public StringCollection AllDtcs
        {
            get
            {
                StringCollection dtcs = new StringCollection();

                foreach (DtcCodeView dcv in this)
                {
                    foreach (string dtc in dcv.Codes)
                    {
                        if (!dtcs.Contains(dtc))
                        {
                            dtcs.Add(dtc);
                        }
                    }
                }

                return dtcs;
            }
        }

        /// <summary>
        /// An indexer into the collecton.
        /// </summary>
        public DtcCodeView this[int index]
        {
            get
            {
                return (DtcCodeView)List[index];
            }
        }

        /// <summary>
        /// Adds a DtcCodeView to the collection.
        /// </summary>
        /// <param name="value">The DtcCodeView to be added.</param>
        public void Add(DtcCodeView value)
        {
            base.Add(value);
        }

        /// <summary>
        /// Removes a DtcCodeView from the collection.
        /// </summary>
        /// <param name="value">The DtcCodeView to be removed.</param>
        public void Remove(DtcCodeView value)
        {
            base.Remove(value);
        }

        /// <summary>
        /// Inserts a DtcCodeView into the collection.
        /// </summary>
        /// <param name="index">The location in the collection which to insert the value.</param>
        /// <param name="value">The value to be inserted.</param>
        public void Insert(int index, DtcCodeView value)
        {
            base.Insert(index, value);
        }

        /// <summary>
        /// Clears the contents of the collection.
        /// </summary>
        public override void Clear()
        {
            base.Clear();
        }
    }
}