using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Developer;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.BLL.Services
{
    public class DeveloperService
    {
        private readonly DeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DeveloperService(DeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _developerRepository.Developers
                .Include(d => d.Games)
                .ToListAsync();

            var dtos = _mapper.Map<List<DeveloperDto>>(entities);

            return ServiceResponse.Success("Розробників отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _developerRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return ServiceResponse.Error($"Розробник з id '{id}' не знайдений");
            }

            var dto = _mapper.Map<DeveloperDto>(entity);

            return ServiceResponse.Success("Розробника отримано", dto);
        }

        public async Task<ServiceResponse> CreateAsync(CreateDeveloperDto dto)
        {
            if (await _developerRepository.IsExistsAsync(dto.Name))
            {
                return ServiceResponse.Error($"Ім'я '{dto.Name}' вже використовується");
            }

            // mapping
            var entity = _mapper.Map<DeveloperEntity>(dto);

            bool res = await _developerRepository.CreateAsync(entity);

            if(!res)
            {
                return ServiceResponse.Error($"Не вдалося додати розробника");
            }

            var responseDto = _mapper.Map<DeveloperDto>(entity);

            return ServiceResponse.Success($"Розробник '{dto.Name}' успішно доданий", responseDto);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateDeveloperDto dto)
        {
            var entity = await _developerRepository.GetByIdAsync(dto.Id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Розробник з id '{dto.Id}' не знайдений");
            }

            if (await _developerRepository.IsExistsAsync(dto.Name, dto.Id))
            {
                return ServiceResponse.Error($"Ім'я '{dto.Name}' вже використовується");
            }

            string oldName = entity.Name;

            _mapper.Map(dto, entity);

            bool res = await _developerRepository.UpdateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error($"Не вдалося змінити дані розробника");
            }

            var responseDto = _mapper.Map<DeveloperDto>(entity);

            return ServiceResponse.Success($"Розробник '{oldName}' успішно змінений", responseDto);
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var entity = await _developerRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return ServiceResponse.Error($"Розробник з id '{id}' не знайдений");
            }

            bool res = await _developerRepository.DeleteAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error($"Не вдалося видалити розробника");
            }

            return ServiceResponse.Success($"Розробник '{entity.Name}' успішно видалений");
        }
    }
}
