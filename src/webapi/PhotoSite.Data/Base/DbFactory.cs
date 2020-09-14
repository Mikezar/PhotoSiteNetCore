using System;
using Microsoft.Extensions.Options;

namespace PhotoSite.Data.Base
{
    internal class DbFactory
    {
        private readonly string _connectionString;
        private MainDbContext? _readOnlyContext;
        private static readonly object SyncRoot = new Object();

        public DbFactory(IOptionsSnapshot<DatabaseOptions> config)
        {
            _connectionString = config.Value.ConnectionString ?? throw new Exception("Database connection string is not provided");
        }

        public MainDbContext GetReadContext()
        {
            if (_readOnlyContext == null)
                lock(SyncRoot)
                    if (_readOnlyContext == null)
                        _readOnlyContext = new MainDbContext(_connectionString);
            return _readOnlyContext;
        }

        public MainDbContext GetWriteContext()
        {
            return new MainDbContext(_connectionString);
        }
    }
}