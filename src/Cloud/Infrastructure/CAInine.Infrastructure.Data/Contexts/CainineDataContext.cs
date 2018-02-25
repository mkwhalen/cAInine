using CAInine.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAInine.Infrastructure.Data.Contexts
{
    /// <summary>
    /// Context for the sql database using Entity Framework to map
    /// our entity models to SQL tables. Each DbSet is a table in sql
    /// </summary>
    public class CainineDataContext : DbContext
    {
        public DbSet<SubmittedDog> SubmittedDogs { get; set; }
        public CainineDataContext(DbContextOptions options) : base(options)
        {
            // intentionally empty
        }
    }
}
