using Bunkering.Access.DAL;

namespace Bunkering.Access.IContracts
{
	public interface IUnitOfWork : IDisposable
	{
		IApplication Application { get; }
		IAppFee AppFee { get; }
		IApplicationType ApplicationType { get; }
		IApplicationHistory ApplicationHistory { get; }
		IvAppPayment vAppPayment { get; }
		IAppointment Appointment { get; }
		IvAppVessel vAppVessel { get; }
		ICountry Country { get; }
		IFacility Facility { get; }
		IvFacilityPermit vFacilityPermit { get; }
		IFacilityType FacilityType { get; }
		IFacilityTypeDocuments FacilityTypeDocuments { get; }
		IInspection Inspection { get; }
		ILGA LGA { get; }
		IMessage Message { get; }
		IPayment Payment { get; }
		IPermit Permit { get; }
		IProduct Product { get; }
		IState State { get; }
		ISubmittedDocument SubmittedDocument { get; }
		ITank Tank { get; }
		IValidatiionResponse ValidatiionResponse { get; }
		IWorkflow Workflow { get; }
		IVesselType VesselType { get; }
		int Save();
		Task<int> SaveChangesAsync(string userId);
	}
}
