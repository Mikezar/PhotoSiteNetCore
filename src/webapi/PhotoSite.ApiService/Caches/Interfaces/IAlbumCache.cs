﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoSite.Core.Cache;
using PhotoSite.Data.Entities;

namespace PhotoSite.ApiService.Caches.Interfaces
{
    public interface IAlbumCache : ICache
    {
        Task<ICollection<Album>?> GetChildren(int? id);
    }
}