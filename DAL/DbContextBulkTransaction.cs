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
    public static class DbContextBulkTransaction
    {
        public static void Execute<T>(DbContext context, Type type, IList<T> entities, OperationType operationType, BulkConfig bulkConfig, Action<decimal> progress) where T : class
        {
            using (ActivitySources.StartExecuteActivity((EFCore.BulkExtensions.OperationType)operationType, entities.Count))
            {
                if (operationType != OperationType.Truncate && entities.Count == 0)
                {
                    return;
                }

                TableInfo tableInfo = TableInfo.CreateInstance(context, type, entities, (EFCore.BulkExtensions.OperationType)operationType, bulkConfig);

                if (operationType == OperationType.Insert && !tableInfo.BulkConfig.SetOutputIdentity)
                {
                    SqlBulkOperation.Insert(context, entities, tableInfo, progress);
                }
                else if (operationType == OperationType.Read)
                {
                    SqlBulkOperation.Read(context, entities, tableInfo, progress);
                }
                else if (operationType == OperationType.Truncate)
                {
                    SqlBulkOperation.Truncate(context, tableInfo);
                }
                else
                {
                    SqlBulkOperation.Merge(context, entities, tableInfo, operationType, progress);
                }
            }
        }

        public static void Execute(DbContext context, Type type, IList<object> entities, OperationType operationType, BulkConfig bulkConfig, Action<decimal> progress)
        {
            using (ActivitySources.StartExecuteActivity((EFCore.BulkExtensions.OperationType)operationType, entities.Count))
            {
                if (operationType != OperationType.Truncate && entities.Count == 0)
                {
                    return;
                }

                TableInfo tableInfo = TableInfo.CreateInstance(context, type, entities, (EFCore.BulkExtensions.OperationType)operationType, bulkConfig);

                if (operationType == OperationType.Insert && !tableInfo.BulkConfig.SetOutputIdentity)
                {
                    SqlBulkOperation.Insert(context, type, entities, tableInfo, progress);
                }
                else if (operationType == OperationType.Read)
                {
                    SqlBulkOperation.Read(context, type, entities, tableInfo, progress);
                }
                else if (operationType == OperationType.Truncate)
                {
                    SqlBulkOperation.Truncate(context, tableInfo);
                }
                else
                {
                    SqlBulkOperation.Merge(context, type, entities, tableInfo, operationType, progress);
                }
            }
        }

        public static Task ExecuteAsync<T>(DbContext context, Type type, IList<T> entities, OperationType operationType, BulkConfig bulkConfig, Action<decimal> progress, CancellationToken cancellationToken) where T : class
        {
            using (ActivitySources.StartExecuteActivity((EFCore.BulkExtensions.OperationType)operationType, entities.Count))
            {
                if (operationType != OperationType.Truncate && entities.Count == 0)
                {
                    return Task.CompletedTask;
                }

                TableInfo tableInfo = TableInfo.CreateInstance(context, type, entities, (EFCore.BulkExtensions.OperationType)operationType, bulkConfig);

                if (operationType == OperationType.Insert && !tableInfo.BulkConfig.SetOutputIdentity)
                {
                    return SqlBulkOperation.InsertAsync(context, entities, tableInfo, progress, cancellationToken);
                }
                else if (operationType == OperationType.Read)
                {
                    return SqlBulkOperation.ReadAsync(context, entities, tableInfo, progress, cancellationToken);
                }
                else if (operationType == OperationType.Truncate)
                {
                    return SqlBulkOperation.TruncateAsync(context, tableInfo, cancellationToken);
                }
                else
                {
                    return SqlBulkOperation.MergeAsync(context, entities, tableInfo, operationType, progress, cancellationToken);
                }
            }
        }

        public static Task ExecuteAsync(DbContext context, Type type, IList<object> entities, OperationType operationType, BulkConfig bulkConfig, Action<decimal> progress, CancellationToken cancellationToken)
        {
            using (ActivitySources.StartExecuteActivity((EFCore.BulkExtensions.OperationType)operationType, entities.Count))
            {
                if (operationType != OperationType.Truncate && entities.Count == 0)
                {
                    return Task.CompletedTask;
                }

                TableInfo tableInfo = TableInfo.CreateInstance(context, type, entities, (EFCore.BulkExtensions.OperationType)operationType, bulkConfig);

                if (operationType == OperationType.Insert && !tableInfo.BulkConfig.SetOutputIdentity)
                {
                    return SqlBulkOperation.InsertAsync(context, type, entities, tableInfo, progress, cancellationToken);
                }
                else if (operationType == OperationType.Read)
                {
                    return SqlBulkOperation.ReadAsync(context, type, entities, tableInfo, progress, cancellationToken);
                }
                else if (operationType == OperationType.Truncate)
                {
                    return SqlBulkOperation.TruncateAsync(context, tableInfo, cancellationToken);
                }
                else
                {
                    return SqlBulkOperation.MergeAsync(context, type, entities, tableInfo, operationType, progress, cancellationToken);
                }
            }
        }

    }
}
