using CAInine.Core.Interfaces.Repositories;
using CAInine.Core.Models.Entities;
using CAInine.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CAInine.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Submitted dog repository using entity framework to store the entities
    /// </summary>
    public class SubmittedDogRepository : ISubmittedDogRepository
    {
        private readonly CainineDataContext _context;
        public SubmittedDogRepository(CainineDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds the entity to the dbcontext and saves the changes to the database
        /// </summary>
        /// <param name="entity">The original entity</param>
        /// <returns>The final entity after processing</returns>
        public async Task<SubmittedDog> AddAsync(SubmittedDog entity)
        {
            await _context.SubmittedDogs.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
