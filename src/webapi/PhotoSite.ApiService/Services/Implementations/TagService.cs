using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.ApiService.Services.Interfaces;
using PhotoSite.Data.Entities;
using PhotoSite.Data.Repositories.Interfaces;

namespace PhotoSite.ApiService.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _tagRepository.GetAll();
        }

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        public async Task<IResult> Update(Tag tag)
        {
            var value = await _tagRepository.Get(tag.Id);
            if (value == null)
                return Result.GetError($"Not found tag id={tag.Id}");

            var ext = await _tagRepository.ExistsOtherTagByTagTitle(tag.Id, tag.Title);
            if (ext)
                return Result.GetError($"Tag's title '{tag.Title}' exists in other tag");

            value.Title = tag.Title;
            await _tagRepository.Update(value);

            return Result.GetOk();
        }

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagTitle">Tag's title</param>
        /// <returns>Identification new tag</returns>
        public async Task<IIdResult> Create(string tagTitle)
        {
            var ext = await _tagRepository.ExistsByTagTitle(tagTitle);
            if (ext)
                return IdResult.GetError($"Tag's title '{tagTitle}' exists in other tag");

            var tag = new Tag {Title = tagTitle};
            var id = await _tagRepository.Create(tag);

            return IdResult.GetOk(id);
        }
    }
}