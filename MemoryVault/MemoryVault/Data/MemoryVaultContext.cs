using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MemoryVault.Models;

namespace MemoryVault.Data
{
    public class MemoryVaultContext : DbContext
    {
        public MemoryVaultContext (DbContextOptions<MemoryVaultContext> options)
            : base(options)
        {
            this.Database.EnsureCreatedAsync();
            
        }

        public DbSet<MemoryVault.Models.Vault>? Vault { get; set; }
    }
}
