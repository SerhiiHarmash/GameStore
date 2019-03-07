using System.Collections.Generic;
using GameStore.Models.Entities;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface IPlatformTypeService
    {
        IEnumerable<PlatformType> GetAllPlatformTypes();
    }
}
