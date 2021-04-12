using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public static class DbContextBulkExtensions
    {
        // Async methods

        public static Task BulkInsertAsync<T>(this DbContext context, IList<T> entities, Type type = null, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default) where T : class
        {
            return DbContextBulkTransaction.ExecuteAsync(context, type, entities, OperationType.Insert, bulkConfig, progress, cancellationToken);
        }

        public static Task BulkInsertAsync(this DbContext context, IList<object> entities, Type type = null, BulkConfig bulkConfig = null, Action<decimal> progress = null, CancellationToken cancellationToken = default)
        {
            return DbContextBulkTransaction.ExecuteAsync(context, type, entities, OperationType.Insert, bulkConfig, progress, cancellationToken);
        }

        public static Task BulkInsertAsync<T>(this DbContext context, IList <T> entities, Action<BulkConfig> bulkAction, Type type = null, Action<decimal> progress = null, CancellationToken cancellationToken = default) where T : class
        {
            BulkConfig bulkConfig = new BulkConfig();
            bulkAction?.Invoke(bulkConfig);
            return DbContextBulkTransaction.ExecuteAsync(context, type, entities, OperationType.Insert, bulkConfig, progress, cancellationToken);
        }

        public static Task BulkInsertAsync(this DbContext context, IList<object> entities, Action<BulkConfig> bulkAction, Type type = null, Action<decimal> progress = null, CancellationToken cancellationToken = default)
        {
            BulkConfig bulkConfig = new BulkConfig();
            bulkAction?.Invoke(bulkConfig);
            return DbContextBulkTransaction.ExecuteAsync(context, type, entities, OperationType.Insert, bulkConfig, progress, cancellationToken);
        }

        public static Task TruncateAsync<T>(this DbContext context, Type type, CancellationToken cancellationToken = default) where T : class
        {
            return DbContextBulkTransaction.ExecuteAsync<T>(context, type, new List<T>(), OperationType.Truncate, null, null, cancellationToken);
        }

        public static Task TruncateAsync(this DbContext context, Type type = null, CancellationToken cancellationToken = default)
        {
            return DbContextBulkTransaction.ExecuteAsync(context, type, new List<object>(), OperationType.Truncate, null, null, cancellationToken);
        }

    }
}
