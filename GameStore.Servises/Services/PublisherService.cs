using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using System;
using System.Collections.Generic;

namespace GameStore.Services.Services
{
    public class PublisherService : BaseService, IPublisherService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<Publisher> _logger;

        public PublisherService(IUnitOfWork unitOfWork, ILogger<Publisher> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Publisher> GetAllPublisher()
        {
            var publishers = _unitOfWork.GetRepository<Publisher>().GetMany(null, null, null, null, i => i.Games);
            return publishers;
        }

        public void AddPublisher(Publisher publisher)
        {
            CheckForNull(publisher, "Publisher is null");

            publisher.Id = Guid.NewGuid().ToString();

            _unitOfWork.GetRepository<Publisher>().Add(publisher);

            _unitOfWork.Save();

            _logger.WriteCreateActionLog(publisher);
        }

        public void EditPublisher(Publisher publisher)
        {
            var publisherRepository = _unitOfWork.GetRepository<Publisher>();

            var genreFromDb = publisherRepository.GetSingle(x => x.Id == publisher.Id, i => i.Games);
            CheckForNull(genreFromDb, "The genre does not exist");

            var oldGenre = Mapper.Map<Publisher>(genreFromDb);
            Mapper.Map(publisher, genreFromDb);

            publisherRepository.Update(genreFromDb);

            _unitOfWork.Save();

            _logger.WriteUpdateActionLog(oldGenre, genreFromDb);
        }

        public bool CheckIfPublisherExists(string companyName)
        {
            var exists = _unitOfWork.GetRepository<Publisher>().Any(x => x.CompanyName == companyName);
            return exists;
        }

        public Publisher GetPublisherByCompanyName(string companyName)
        {
            CheckStringIsNullOrWhiteSpace(companyName, "Error companyName is null or write space");

            var publisher = _unitOfWork.GetRepository<Publisher>().GetSingle(x => x.CompanyName == companyName, i => i.Games);

            return publisher;
        }
    }
}
