using GameStore.Models.Entities;
using System.Collections.Generic;

namespace GameStore.Contracts.Interfaces.Services
{
    public interface IGenreService
    {
        IEnumerable<Genre> GetAllGenres();

        void Add(Genre genre);

        void Edit(Genre genre);
    }
}
