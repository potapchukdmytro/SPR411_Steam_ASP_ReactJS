using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.BLL.Settings;
using SPR411_SteamClone.DAL.Entities;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.BLL.Services
{
    public class GameService
    {
        private readonly GameRepository _gameRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;

        public GameService(GameRepository gameRepository, IMapper mapper, GenreRepository genreRepository, FileService fileService)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _genreRepository = genreRepository;
            _fileService = fileService;
        }

        private async Task SaveImagesAsync(GameEntity entity, CreateGameDto dto)
        {
            string guid = Guid.NewGuid().ToString();
            string folderPath = Path.Combine(StaticFilesSettings.Games, guid);
            if (dto.PreviewImage != null)
            {
                var res = await _fileService.SaveImageAsync(dto.PreviewImage, folderPath);
                if (res.IsSuccess)
                {
                    var previewImage = new GameImageEntity
                    {
                        IsPreview = true,
                        Name = $"{guid}/{res.Payload!}",
                    };
                    entity.Images.Add(previewImage);
                }
            }

            if (dto.Images.Count > 0)
            {
                var res = await _fileService.SaveImagesAsync(dto.Images, folderPath);

                foreach (var r in res)
                {
                    if (r.IsSuccess)
                    {
                        var image = new GameImageEntity
                        {
                            IsPreview = false,
                            Name = $"{guid}/{r.Payload!}"
                        };
                        entity.Images.Add(image);
                    }
                }
            }
        }

        public async Task<ServiceResponse> CreateAsync(CreateGameDto dto)
        {
            var entity = _mapper.Map<GameEntity>(dto);

            entity.Genres = await _genreRepository.Genres
                .Where(g => dto.Genres.Select(g => g.ToLower()).Contains(g.Name.ToLower()))
                .ToListAsync();

            // images
            await SaveImagesAsync(entity, dto);

            var res = await _gameRepository.CreateAsync(entity);

            if(!res)
            {
                return ServiceResponse.Error("Не вдалося додати гру");
            }

            return ServiceResponse.Success($"Гра '{dto.Name}' успішно додана");
        }
    }
}
