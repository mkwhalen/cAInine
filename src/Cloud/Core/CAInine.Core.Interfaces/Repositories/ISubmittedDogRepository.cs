using CAInine.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Core.Interfaces.Repositories
{
    /// <summary>
    /// Repository for submitted dog models
    /// </summary>
    public interface ISubmittedDogRepository
    {
        /// <summary>
        /// Adds a new submitted dog
        /// </summary>
        /// <param name="entity">The entity</param>
        /// <returns>The final entity after processing</returns>
        Task<SubmittedDog> AddAsync(SubmittedDog entity);
    }
}
