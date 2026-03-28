using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Genre;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.BLL.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(GenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _genreRepository.Genres
                .AsNoTracking()
                .ToListAsync();

            var dtos = _mapper.Map<List<GenreDto>>(entities);

            return ServiceResponse.Success("Список жанрів отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Жанр з id '{id}' не знайдений");
            }

            var dto = _mapper.Map<GenreDto>(entity);

            return ServiceResponse.Success($"Жанр отримано", dto);
        }

        public async Task<ServiceResponse> GetByNameAsync(string name)
        {
            var entity = await _genreRepository.GetByNameAsync(name);

            if (entity == null)
            {
                return ServiceResponse.Error($"Жанр '{name}' не знайдений");
            }

            var dto = _mapper.Map<GenreDto>(entity);

            return ServiceResponse.Success($"Жанр отримано", dto);
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _genreRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Жанр з id '{id}' не знайдений");
            }

            bool result = await _genreRepository.DeleteAsync(entity);

            if (!result)
            {
                return ServiceResponse.Error($"Не вдалося видалити жанр '{entity.Name}'");
            }

            return ServiceResponse.Success($"Жанр '{entity.Name}' успішно видалений");
        }

        public async Task<ServiceResponse> CreateAsync(CreateGenreDto dto)
        {
            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return ServiceResponse.Error($"Ім'я '{dto.Name}' вже використовується");
            }

            var entity = _mapper.Map<GenreEntity>(dto);

            bool res = await _genreRepository.CreateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error($"Не вдалося додати жанр");
            }

            var responseDto = _mapper.Map<GenreDto>(entity);

            return ServiceResponse.Success($"Жанр '{dto.Name}' успішно доданий", responseDto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateGenreDto dto)
        {
            var entity = await _genreRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Жанр з id '{dto.Id}' не знайдений");
            }

            if (await _genreRepository.IsExistsAsync(dto.Name))
            {
                return ServiceResponse.Error($"Ім'я '{dto.Name}' вже використовується");
            }

            string oldName = entity.Name;

            _mapper.Map(dto, entity);

            bool res = await _genreRepository.UpdateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error($"Не вдалося змінити назву жанру");
            }

            var responseDto = _mapper.Map<GenreDto>(entity);

            return ServiceResponse.Success($"Жанр '{oldName}' успішно змінений", responseDto);
        }
    }
}
