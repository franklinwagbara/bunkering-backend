
namespace Bunkering.Core.Data
{
	public class WorkFlow
	{
		public int Id { get; set; }
		public int FacilityTypeId { get; set; }
		public int? ApplicationTypeId { get; set; }
		public string TriggeredByRole { get; set; }
		public string Action { get; set; }
		public string TargetRole { get; set; }
		public string Rate { get; set; }
		public string Status { get; set; }
	}
}
