using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Core.ViewModels
{
	public class ApplicationReportViewModel
	{
		public DateTime StartDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Status { get; set; }
		public int ApplicationTypeId { get; set; }
	}
}
