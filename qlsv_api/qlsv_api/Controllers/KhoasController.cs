using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlsv_api.DTO;
using qlsv_api.Models;

namespace qlsv_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoasController : ControllerBase
    {
        private readonly QlsvApiContext _context;

        public KhoasController(QlsvApiContext context)
        {
            _context = context;
        }

        // GET: api/Khoas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhoaDTO>>> GetKhoas()
        {
          if (_context.Khoas == null)
          {
              return NotFound();
          }
            return await _context.Khoas.Select(k => khoaDTO(k)).ToListAsync();
        }

        // GET: api/Khoas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhoaDTO>> GetKhoa(int id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null) return NotFound();
            return khoaDTO(khoa);
        }

        // PUT: api/Khoas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhoa(int id, KhoaDTO khoaDTO)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return NotFound();
            }
            khoa.Tenkhoa = khoaDTO.Tenkhoa;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Khoas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhoaDTO>> PostKhoa(KhoaDTO khoaDTO)
        {
            if(_context.Khoas == null)
            {
                return Problem("zz");
            }
            var khoa = new Khoa
            {
                Makhoa = khoaDTO.Makhoa,
                Tenkhoa = khoaDTO.Tenkhoa
            };
            _context.Khoas.Add(khoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetKhoa), new { id = khoa.Makhoa }, khoa);
        }

        // DELETE: api/Khoas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhoa(int id)
        {
            if (_context.Khoas == null)
            {
                return NotFound();
            }
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return NotFound();
            }

            _context.Khoas.Remove(khoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhoaExists(int id)
        {
            return (_context.Khoas?.Any(e => e.Makhoa == id)).GetValueOrDefault();
        }
        private static KhoaDTO khoaDTO(Khoa k) => new KhoaDTO
        {
            Makhoa = k.Makhoa,
            Tenkhoa = k.Tenkhoa
        };
    }
}
