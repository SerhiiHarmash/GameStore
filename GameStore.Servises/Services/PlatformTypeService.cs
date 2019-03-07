using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using System.Collections.Generic;

namespace GameStore.Services.Services
{
    public class PlatformTypeService : IPlatformTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlatformTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlatformType> GetAllPlatformTypes()
        {
            var platforms = _unitOfWork.GetRepository<PlatformType>().GetMany();
            return platforms;
        }
    }
}
