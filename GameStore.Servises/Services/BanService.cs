using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using System;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Models.Enums;

namespace GameStore.Services.Services
{
    public class BanService : BaseService, IBanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Ban> _logger;

        public BanService(IUnitOfWork unitOfWork, ILogger<Ban> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public void Add(string userId, BanDuration banDuration)
        {
            var ban = new Ban
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                BeginBan = DateTime.UtcNow
            };

            if (banDuration != BanDuration.Permanent)
            {
                ban.EndBan = DateTime.UtcNow.AddDays((double) banDuration);
            }

            _unitOfWork.GetRepository<Ban>().Add(ban);
            _unitOfWork.Save();
            _logger.WriteCreateActionLog(ban);
        }
    }
}
