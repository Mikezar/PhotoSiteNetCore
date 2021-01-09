using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPhotoCache _photoCache;
        private readonly IAlbumCache _albumCache;

        /// <summary>
        /// ctor
        /// </summary>
        public AlbumService(IAlbumRepository albumRepository, IPhotoCache photoCache, IAlbumCache albumCache)
        {
            _albumRepository = albumRepository;
            _photoCache = photoCache;
            _albumCache = albumCache;
        }

        /// <inheritdoc cref="IAlbumService.Get"/>
        public async Task<Album?> Get(int id)
        {
            return await _albumCache.Get(id);
        }

        /// <inheritdoc cref="IAlbumService.GetChildren"/>
        public async Task<ICollection<Album>?> GetChildren(int? id)
        {
            return await _albumCache.GetChildren(id);
        }

        /// <inheritdoc cref="IAlbumService.Create"/>
        public async Task<IIdResult> Create(Album album)
        {
            var errors = Validate(album);
            if (errors is not null)
                return IdResult.GetError(errors);

            var id = await _albumRepository.Create(album);
            _albumCache.Remove();
            return IdResult.GetOk(id);
        }

        /// <inheritdoc cref="IAlbumService.Update"/>
        public async Task<IResult> Update(Album album)
        {
            var ext = await _albumRepository.Exists(album.Id);
            if (!ext)
                return Result.GetError($"Album (id={album.Id}) doesn't exists");

            var errors = Validate(album);
            if (errors is not null)
                return IdResult.GetError(errors);

            await _albumRepository.Update(album);
            _albumCache.Remove();
            return Result.GetOk();
        }

        /// <inheritdoc cref="IAlbumService.Delete"/>
        public async Task<IResult> Delete(int id)
        {
            var ext = await _albumRepository.Exists(id);
            if (!ext)
                return Result.GetError($"Album (id={id}) doesn't exists");

            if (await _albumRepository.ChildrenExists(id))
                return Result.GetError($"Album (id={id}) has children albums");

            var childPhotos = await _photoCache.GetByAlbum(id);
            if (childPhotos is not null && childPhotos.Any())
                return Result.GetError($"Album (id={id}) has children photos");

            await _albumRepository.Delete(id);
            _albumCache.Remove();
            return Result.GetOk();
        }

        private string? Validate(Album album)
        {
            if (string.IsNullOrEmpty(album.Title))
                return "Title is empty!";

            // TODO: Add validate equal title in albums has one and the same parent

            return null;
        }

    }
}