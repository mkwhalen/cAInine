using CAInine.Core.Models.Entities;
using CAInine.Core.Models.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Core.Interfaces.Services
{
    /// <summary>
    /// Business logic for processing dog data. Every method should return the result model
    /// </summary>
    public interface IDogProcessingService
    {
        Task<Result<SubmittedDog>> AnalyzeDogImageAsync(string fileName, byte[] imageDate);
    }
}
