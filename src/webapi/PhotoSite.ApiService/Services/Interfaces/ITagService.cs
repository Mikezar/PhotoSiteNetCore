﻿using System.Threading.Tasks;
using PhotoSite.ApiService.Base;
using PhotoSite.ApiService.Data.Common;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Services.Interfaces
{
    public interface ITagService : IService
    {
        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        Task<Tag[]> GetAll();

        /// <summary>
        /// Update tag
        /// </summary>
        /// <param name="tag">Tag</param>
        Task<Result> Update(Tag tag);

        /// <summary>
        /// Create new tag
        /// </summary>
        /// <param name="tagTitle">Tag's title</param>
        /// <returns>Identification new tag</returns>
        Task<IdResult> Create(string tagTitle);
    }
}