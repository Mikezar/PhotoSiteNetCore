﻿namespace PhotoSite.ApiService.Data.Common
{
    public class IdResult : Result, IIdResult
    {
        public int Id { get; set; }

        public static IIdResult GetOk(int id) => new IdResult {Id = id};
    }
}