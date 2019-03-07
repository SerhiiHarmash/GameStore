using GameStore.Models.Enums;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface IBanService
    {
        void Add(string userId, BanDuration banDuration);
    }
}
