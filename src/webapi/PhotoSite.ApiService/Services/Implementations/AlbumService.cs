using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Core.ExtException;
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
        public async Task<ICollection<Album>?> GetChildren(int id)
        {
            return await _albumCache.GetChildren(id == 0 ? (int?)null : id);
        }

        /// <inheritdoc cref="IAlbumService.Create"/>
        public async Task<IIdResult> Create(Album album)
        {
            Validate(album);

            var id = await _albumRepository.Create(album);
            _albumCache.Remove();
            return new IdResult(id);
        }

        /// <inheritdoc cref="IAlbumService.Update"/>
        public async Task Update(Album album)
        {
            if (!await _albumRepository.Exists(album.Id))
                throw new UserException($"Album (id={album.Id}) doesn't exists");

            Validate(album);

            await _albumRepository.Update(album);
            _albumCache.Remove();
        }

        /// <inheritdoc cref="IAlbumService.Delete"/>
        public async Task Delete(int id)
        {
            if (!await _albumRepository.Exists(id))
                throw new UserException($"Album (id={id}) doesn't exists");

            if (await _albumRepository.ChildrenExists(id))
                throw new UserException($"Album (id={id}) has children albums");

            var childPhotos = await _photoCache.GetByAlbum(id);
            if (childPhotos is not null && childPhotos.Any())
                throw new UserException($"Album (id={id}) has children photos");

            await _albumRepository.Delete(id);
            _albumCache.Remove();
        }

        private void Validate(Album album)
        {
            if (string.IsNullOrEmpty(album.Title))
                throw new UserException("Title is empty!");

            // TODO: Add validate equal title in albums has one and the same parent
        }

    }
}