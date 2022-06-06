using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MemoryVault.Data;
using MemoryVault.Models;

namespace MemoryVault
{
    [Route("MemoryVault/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly MemoryVaultContext _context;

        public APIController(MemoryVaultContext context)
        {
            _context = context;
        }

        // GET: API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vault>>> GetVault()
        {
          if (_context.Vault == null)
          {
              return NotFound();
          }
            return await _context.Vault.ToListAsync();
        }

        // GET: API/something on GUID LOL
        [HttpGet("{hash}")]
        public async Task<ActionResult<Vault>> GetVault(string hash)
        {
          if (_context.Vault == null)
          {
              return NotFound();
          }
            var vault = await _context.Vault.FirstAsync(P=>P.hash==hash);

            if (vault == null)
            {
                return NotFound();
            }

            return vault;
        }

        //// PUT: API/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVault(string hash, Vault vault)
        //{
        //    if (hash != vault.hash)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(vault).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VaultExists(hash))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/API
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vault>> PostVault(Vault vault)
        {
          if (_context.Vault == null)
          {
              return Problem("Entity set 'MemoryVaultContext.Vault'  is null.");
          }
            _context.Vault.Add(vault);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVault", new { id = vault.ID }, vault);
        }

        // DELETE: api/API/5
        [HttpDelete("{hash}")]
        public async Task<IActionResult> DeleteVault(string hash)
        {
            if (_context.Vault == null)
            {
                return NotFound();
            }
            var vault = await _context.Vault.FirstAsync(P=>P.hash==hash);
            if (vault == null)
            {
                return NotFound();
            }

            _context.Vault.Remove(vault);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VaultExists(string hash)
        {
            return (_context.Vault?.Any(e => e.hash == hash)).GetValueOrDefault();
        }
    }
}
