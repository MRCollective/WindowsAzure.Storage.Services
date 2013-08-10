using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;

namespace WindowsAzure.Storage.Services.Tests
{
    class AzureStorageFactoryTests
    {
        private const string Name = "azurestoragefactorytests";
        private static readonly CloudStorageAccount StorageAccount = CloudStorageAccount.DevelopmentStorageAccount;

        private CloudQueue _queue;
        private CloudBlobContainer _blob;
        private CloudTable _table;

        [SetUp]
        public void Setup()
        {
            _queue = StorageAccount.CreateCloudQueueClient().GetQueueReference(Name);
            _blob = StorageAccount.CreateCloudBlobClient().GetContainerReference(Name);
            _table = StorageAccount.CreateCloudTableClient().GetTableReference(Name);

            _queue.DeleteIfExists();
            _blob.DeleteIfExists();
            _table.DeleteIfExists();
        }

        [Test]
        public void GivenStorageFactoryConstructedWithStorageAccount_WhenGettingBlobContainer_ThenReturnCorrectBlobContainer()
        {
            var factory = new AzureStorageFactory(StorageAccount);

            var blobContainer = factory.GetBlobContainer(Name);

            Assert.That(blobContainer.Exists(), Is.False);
            _blob.CreateIfNotExists();
            Assert.That(blobContainer.Exists(), Is.True);
        }

        [Test]
        public void GivenStorageFactoryConstructedWithStorageAccount_WhenGettingQueue_ThenReturnCorrectQueue()
        {
            var factory = new AzureStorageFactory(StorageAccount);

            var queue = factory.GetQueue(Name);

            Assert.That(queue.Exists(), Is.False);
            _queue.CreateIfNotExists();
            Assert.That(queue.Exists(), Is.True);
        }

        [Test]
        public void GivenStorageFactoryConstructedWithStorageAccount_WhenGettingTable_ThenReturnCorrectTable()
        {
            var factory = new AzureStorageFactory(StorageAccount);

            var table = factory.GetTable(Name);

            Assert.That(table.Exists(), Is.False);
            _table.CreateIfNotExists();
            Assert.That(table.Exists(), Is.True);
        }
    }
}
