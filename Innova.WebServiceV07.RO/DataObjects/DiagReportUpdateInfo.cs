using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Innova.WebServiceV07.RO.DataObjects
{
#if (!AUTOZONE)
	/// <summary>
	/// Diagnostic report update information object contains the fix status information and the diagnostic report on it.
	/// </summary>
	public class DiagReportUpdateInfo
	{
		/// <summary>
		/// Contructor for the DiagReportUpdateInfo class.
		/// </summary>
		public DiagReportUpdateInfo()
		{
		}

		/// <summary>
		/// A <see cref="DiagReportInfo"/> object.
		/// </summary>
		public DiagReportInfo DiagReportInfo;

		/// <summary>
		/// A <see cref="FixStatusInfo"/> object.
		/// </summary>
		public FixStatusInfo FixStatusInfo;

	}
#endif
}
