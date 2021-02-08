using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoSite.ApiService.Caches.Interfaces;
using PhotoSite.ApiService.Data;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagCache _tagCache;
        private readonly IPhotoToTagCache _photoToTagCache;

        /// <summary>
        /// ctor
        /// </summary>
        public TagService(ITagRepository tagRepository, ITagCache tagCache, IPhotoToTagCache photoToTagCache)
        {
            _tagRepository = tagRepository;
            _tagCache = tagCache;
            _photoToTagCache = photoToTagCache;
        }

        /// <inheritdoc cref="ITagService.GetExtAll"/>
        public async Task<ICollection<TagExtension>> GetExtAll()
        {
            var tags = await _tagCache.GetAll();
            var photoToTag = await _photoToTagCache.GetAll();

            var result = tags.Select(t => new TagExtension(t) {PhotoCount = photoToTag.Count(pt => pt.TagId == t.Id)});

            return result.ToArray();
        }

        /// <inheritdoc cref="ITagService.GetAll"/>
        public async Task<ICollection<Tag>> GetAll()
        {
            return await _tagCache.GetAll();
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        public async Task<IResult> Update(Tag tag)
        {
            var errors = Validate(tag);
            if (errors is not null)
                return IdResult.GetError(errors);

            var value = await _tagRepository.Get(tag.Id);
            if (value == null)
                return Result.GetError($"Not found tag id={tag.Id}");

            var ext = await _tagRepository.ExistsOtherTagByTagTitle(tag.Id, tag.Title);
            if (ext)
                return Result.GetError($"Tag's title '{tag.Title}' exists in other tag");

            value.Title = tag.Title;
            await _tagRepository.Update(value);

            _tagCache.Remove();
            
            return Result.GetOk();
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tag">Tag</param>
        /// <returns>Identification new tag</returns>
        public async Task<IIdResult> Create(Tag tag)
        {
            var errors = Validate(tag);
            if (errors is not null)
                return IdResult.GetError(errors);

            var ext = await _tagRepository.ExistsByTagTitle(tag.Title!);
            if (ext)
                return IdResult.GetError($"Tag's title '{tag.Title}' exists in other tag");

            var id = await _tagRepository.Create(tag);

            _tagCache.Remove();

            return IdResult.GetOk(id);
        }

        private string? Validate(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.Title))
                return "Title is empty!";

            // TODO: Add validate equal title in albums has one and the same parent

            return null;
        }
    }
}