using Bunkering.Access.IContracts;
using Bunkering.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bunkering.Access.Services
{
    public class LicenseService
    {
        ApiResponse _response;
        private readonly IUnitOfWork _unitOfWork;

        public LicenseService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

    }
}
