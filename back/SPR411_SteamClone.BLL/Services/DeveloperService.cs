using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Developer;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.BLL.Services
{
    public class DeveloperService
    {
        private readonly DeveloperRepository _developerRepository;

        public DeveloperService(DeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var entities = await _developerRepository.Developers
                .Include(d => d.Games)
                .ToListAsync();

            var dtos = entities.Select(e => new DeveloperDto
            {
                Image = e.Image,
                Name = e.Name,
                Id = e.Id
            });

            return ServiceResponse.Success("Розробників отримано", dtos);
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var entity = await _developerRepository.GetByIdAsync(id);

            if(entity == null)
            {
                return ServiceResponse.Error($"Розробник з id '{id}' не знайдений");
            }

            var dto = new DeveloperDto
            {
                Id = entity.Id,
                Image = entity.Image,
                Name = entity.Name
            };

            return ServiceResponse.Success("Розробника отримано", dto);
        }

        public async Task<ServiceResponse> CreateAsync(CreateDeveloperDto dto)
        {
            if (await _developerRepository.IsExistsAsync(dto.Name))
            {
                return ServiceResponse.Error($"Ім'я '{dto.Name}' вже використовується");
            }

            // mapping
            var entity = new DeveloperEntity
            {
                Name = dto.Name,
                Image = dto.Image?.FileName
            };

            bool res = await _developerRepository.CreateAsync(entity);

            if(!res)
            {
                return ServiceResponse.Error($"Не вдалося додати розробника");
            }

            var responseDto = new DeveloperDto
            {
                Id = entity.Id,
                Image = entity.Image,
                Name = entity.Name
            };

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
            entity.Name = dto.Name;
            entity.Image = dto.Image?.FileName;

            bool res = await _developerRepository.UpdateAsync(entity);

            if (!res)
            {
                return ServiceResponse.Error($"Не вдалося змінити дані розробника");
            }

            var responseDto = new DeveloperDto
            {
                Id = entity.Id,
                Image = entity.Image,
                Name = entity.Name
            };

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
