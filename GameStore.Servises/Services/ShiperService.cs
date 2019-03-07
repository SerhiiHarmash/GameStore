using GameStore.Models.Entities;
using System.Collections.Generic;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Services;

namespace GameStore.Services.Services
{
    public class ShipperService : IShipperService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShipperService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Shipper> GetAllShippers()
        {
            var shippers = _unitOfWork.GetRepository<Shipper>().GetMany();
            return shippers;
        }
    }
}
