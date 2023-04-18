using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Core.ViewModels
{
    public class AppStageDocsViewModel
    {
        public int ApplicationTypeId { get; set; }
        public int FacilityTypeId { get; set; }
        public List<int> DocumentTypeId { get; set; }
    }
}
