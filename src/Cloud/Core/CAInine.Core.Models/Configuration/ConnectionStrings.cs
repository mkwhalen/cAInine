using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Core.Models.Configuration
{
    /// <summary>
    /// Model for representing connection strings found from the appsettings.json file.
    /// This contains connection strings
    /// </summary>
    public class ConnectionStrings
    {
        public string DatabaseConnectionString { get; set; }
        public string BlobStorageConnectionString { get; set; }
    }
}
