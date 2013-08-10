using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;

namespace WindowsAzure.Storage.Services
{
    /// <summary>
    /// Factory that returns Azure Storage services for a storage account.
    /// </summary>
    public interface IAzureStorageFactory
    {
        /// <summary>
        /// Get a raw blob container.
        /// </summary>
        /// <param name="containerName">The name of the container to retrieve</param>
        /// <returns>The blob container for the specified container name</returns>
        CloudBlobContainer GetBlobContainer(string containerName);

        /// <summary>
        /// Get a raw queue.
        /// </summary>
        /// <param name="queueName">The name of the queue to retrieve</param>
        /// <returns>The queue of the specified name</returns>
        CloudQueue GetQueue(string queueName);

        /// <summary>
        /// Get a raw table.
        /// </summary>
        /// <param name="tableName">The name of the table to retrieve</param>
        /// <returns>The table of the specified name</returns>
        CloudTable GetTable(string tableName);
    }

    /// <summary>
    /// Factory that returns Azure Storage services for a storage account.
    /// </summary>
    public class AzureStorageFactory : IAzureStorageFactory
    {
        private readonly CloudStorageAccount _storageAccount;

        /// <summary>
        /// Construct an Azure Storage Factory with a connection string.
        /// </summary>
        /// <param name="connectionString">The connection string for the Azure Storage account to provide services for</param>
        public AzureStorageFactory(string connectionString)
        {
            _storageAccount = CloudStorageAccount.Parse(connectionString);
        }

        /// <summary>
        /// Construct an Azure Storage Factory with storage account details.
        /// </summary>
        /// <param name="storageAccount">The storage account details for the Azure Storage account to provide services for</param>
        public AzureStorageFactory(CloudStorageAccount storageAccount)
        {
            _storageAccount = storageAccount;
        }

        public CloudBlobContainer GetBlobContainer(string containerName)
        {
            var blobClient = _storageAccount.CreateCloudBlobClient();
            return blobClient.GetContainerReference(containerName);
        }

        public CloudQueue GetQueue(string queueName)
        {
            var queue = _storageAccount.CreateCloudQueueClient();
            return queue.GetQueueReference(queueName);
        }

        public CloudTable GetTable(string tableName)
        {
            var queue = _storageAccount.CreateCloudTableClient();
            return queue.GetTableReference(tableName);
        }
    }
}
