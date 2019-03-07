using AutoMapper;
using GameStore.Contracts.Interfaces.DAL;
using GameStore.Contracts.Interfaces.Logger;
using GameStore.Contracts.Interfaces.Services;
using GameStore.Models.Entities;
using System;
using System.Collections.Generic;

namespace GameStore.Services.Services
{
    public class GenreService : BaseService, IGenreService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<Genre> _logger;

        public GenreService(IUnitOfWork unitOfWork, ILogger<Genre> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            var genres = _unitOfWork.GetRepository<Genre>().GetMany();
            return genres;
        }

        public void Add(Genre genre)
        {
            CheckForNull(genre, "The genre is null");

            genre.Id = Guid.NewGuid().ToString();
            _unitOfWork.GetRepository<Genre>().Add(genre);
            _unitOfWork.Save();
            _logger.WriteCreateActionLog(genre);
        }

        public void Edit(Genre genre)
        {
            var genreRepository = _unitOfWork.GetRepository<Genre>();

            var genreFromDb = genreRepository.GetSingle(x => x.Id == genre.Id, i => i.Games);
            CheckForNull(genreFromDb, "The genre does not exist");

            var oldGenre = Mapper.Map<Genre>(genre);
            Mapper.Map(genre, genreFromDb);

            genreRepository.Update(genreFromDb);

            _unitOfWork.Save();

            _logger.WriteUpdateActionLog(oldGenre, genre);
        }
    }
}
