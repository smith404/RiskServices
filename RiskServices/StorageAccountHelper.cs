using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskServices
{
    public class StorageAccountHelper
    {
        public static BlobServiceClient GetBlobServiceClient()
        {
            return GetBlobServiceClient(ConnectionBuilder.GetStorageAccountConnectionString());

        }

        public static BlobServiceClient GetBlobServiceClient(string connectionString)
        {
            try
            {
                BlobServiceClient serviceClient = new(connectionString);

                return serviceClient;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public static QueueServiceClient GetQueueServiceClient()
        {
            return GetQueueServiceClient(ConnectionBuilder.GetStorageAccountConnectionString());
        }

        public static QueueServiceClient GetQueueServiceClient(string connectionString)
        {
            try
            {
                QueueServiceClient serviceClient = new(connectionString);

                return serviceClient;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static BlobContainerClient GetBlobContainerClient(BlobServiceClient serviceClient, string container)
        {
            try
            {
                BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(container);

                return containerClient;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static QueueClient GetQueueClient(QueueServiceClient serviceClient, string queue)
        {
            try
            {
                QueueClient queueClient = serviceClient.GetQueueClient(queue);

                return queueClient;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static async Task<bool> WriteContentToBlobAsync(BlobContainerClient containerClient, string guid, string filename, string content)
        {
            try
            {
                string localPath = Path.GetTempPath();
                string localFileName = guid + "_" + filename;
                string localFilePath = Path.Combine(localPath, localFileName);
                
                BlobClient blobClient = containerClient.GetBlobClient(guid + "/" + filename);

                // Write text to the file
                await File.WriteAllTextAsync(localFilePath, content);

                // Upload data from local file
                await blobClient.UploadAsync(localFilePath, true);

                // Clean up
                File.Delete(localFilePath);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<bool> DumpDataTableToBlobAsync(BlobContainerClient containerClient, DataTable table, string guid, string filename, string delimiter = ",")
        {
            if (table == null)
            {
                return false;
            }

            try
            {
                string localPath = Path.GetTempPath();
                string localFileName = guid + "_" + filename;
                string localFilePath = Path.Combine(localPath, localFileName);

                using (StreamWriter file = new(localFilePath))
                {
                    if (table.Rows != null && table.Columns != null)
                    {
                        bool first = true;
                        foreach (object column in table.Columns)
                        {
                            if (!first)
                            {
                                await file.WriteAsync(delimiter);
                            }
                            else
                            {
                                first = false;
                            }
                            await file.WriteAsync(column.ToString());
                        }
                        await file.WriteLineAsync();

                        foreach (DataRow row in table.Rows)
                        {
                            first = true;
                            foreach (object item in row.ItemArray)
                            {
                                if (!first)
                                {
                                    await file.WriteAsync(delimiter);
                                }
                                else
                                {
                                    first = false;
                                }
                                await file.WriteAsync(item.ToString());
                            }
                            await file.WriteLineAsync();
                        }
                    }
                }

                BlobClient blobClient = containerClient.GetBlobClient(guid + "/" + filename);

                // Upload data from local file
                await blobClient.UploadAsync(localFilePath, true);

                // Upload data from local file
                await blobClient.UploadAsync(localFilePath, true);

                // Clean up
                File.Delete(localFilePath);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
