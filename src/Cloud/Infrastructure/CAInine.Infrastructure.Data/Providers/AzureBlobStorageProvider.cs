using CAInine.Core.Interfaces.Providers;
using CAInine.Core.Models.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Infrastructure.Data.Providers
{
    /// <summary>
    /// Blob storage provider using Azure blob storage
    /// </summary>
    public class AzureBlobStorageProvider : IBlobProvider
    {
        private readonly IOptions<ConnectionStrings> _connectionStringSettings;
        public AzureBlobStorageProvider(IOptions<ConnectionStrings> connectionStringsSettings)
        {
            _connectionStringSettings = connectionStringsSettings;
        }

        /// <summary>
        /// Uploads an image to blob storage in Azure. This can throw an exception if there are issues with uploading.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns>The end url after processing the request</returns>
        public async Task<string> UploadImageAsync(string fileName, byte[] data)
        {
            // upload to blob storage, get full url, then add to repo
            var storageAccount = CloudStorageAccount.Parse(_connectionStringSettings.Value.BlobStorageConnectionString);

            // Create the CloudBlobClient that is used to call the Blob Service for that storage account.
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container called 'image-uploads'. 
            var cloudBlobContainer = cloudBlobClient.GetContainerReference("image-uploads");
            await cloudBlobContainer.CreateIfNotExistsAsync();

            // Set the permissions so the blobs are public. 
            var permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            await cloudBlobContainer.SetPermissionsAsync(permissions);


            // Get a reference to the location where the blob is going to go, then upload the file.
            // Upload the file you created, use localFileName for the blob name.
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            await cloudBlockBlob.UploadFromByteArrayAsync(data, 0, data.Length);

            var url = cloudBlockBlob.StorageUri.PrimaryUri.ToString();

            return url;
        }
    }
}
