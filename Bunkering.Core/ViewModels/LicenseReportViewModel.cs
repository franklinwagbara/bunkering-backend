using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Core.ViewModels
{
	public class LicenseReportViewModel
	{
		public DateTime StartDate { get; set; }
		public DateTime ToDate { get; set; }
		public IEnumerable<int> VesselType { get; set; }


	}
}
