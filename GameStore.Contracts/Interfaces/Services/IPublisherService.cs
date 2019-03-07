using System.Collections.Generic;
using GameStore.Models.Entities;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetAllPublisher();

        void AddPublisher(Publisher publisher);

        Publisher GetPublisherByCompanyName(string companyName);

        void EditPublisher(Publisher publisher);

        bool CheckIfPublisherExists(string companyName);
    }
}
