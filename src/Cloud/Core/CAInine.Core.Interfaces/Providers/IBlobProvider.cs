using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Core.Interfaces.Providers
{
    /// <summary>
    /// Data provider for blog data such as files.
    /// </summary>
    public interface IBlobProvider
    {
        /// <summary>
        /// Uploads an image to the blog storage provider
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns>The url of the file after processing</returns>
        Task<string> UploadImageAsync(string fileName, byte[] data);
    }
}
