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
    public class SinhviensController : ControllerBase
    {
        private readonly QlsvApiContext _context;

        public SinhviensController(QlsvApiContext context)
        {
            _context = context;
        }

        //GETALL
        [HttpGet("/getall")]
        public async Task<ActionResult<IEnumerable<SinhvienDTO>>> GetAll()
        {
            if (_context.Sinhviens == null)
            {
                return NotFound();
            }

            return await _context.Sinhviens.Select(s => SvDTO(s)).ToListAsync();
        }

        // GET: api/Sinhviens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SinhvienDTO>>> GetSinhviens(int pageNumber)
        {
            int pageSize = 5;
            if (_context.Sinhviens == null)
            {
                return NotFound();
            }
            var sinhviens = await _context.Sinhviens.Select(s => SvDTO(s)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            /*return await _context.Sinhviens.Select(s => SvDTO(s)).ToListAsync();*/
            return sinhviens;
        }

        // GET: api/Sinhviens/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<SinhvienDTO>> GetSinhvien(int id)
        {
            var Sinhvien = await _context.Sinhviens.FindAsync(id);

            if (Sinhvien == null)
            {
                return NotFound();
            }

            return SvDTO(Sinhvien);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<SinhvienDTO>> GetSinhvienByName(string name)
        {
            var Sinhvien = await _context.Sinhviens.FirstOrDefaultAsync(n => n.Tensv == name);

            if (Sinhvien == null)
            {
                return NotFound();
            }

            return SvDTO(Sinhvien);
        }


        // PUT: api/Sinhviens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSinhvien(int id, SinhvienDTO sinhvienDTO)
        {
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }
            sinhvien.Tensv = sinhvienDTO.Tensv;
            sinhvien.Ngaysinh = sinhvienDTO.Ngaysinh;
            sinhvien.Gioitinh = sinhvienDTO.Gioitinh;
            sinhvien.Makhoa = sinhvienDTO.Makhoa;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            /*return NoContent();*/
            return Ok();
        }

        // POST: api/Sinhviens
        [HttpPost]
        public async Task<ActionResult<SinhvienDTO>> PostSinhvien(SinhvienDTO sinhvienDTO)
        {
            var sinhvien = new Sinhvien
            {
                Masv = sinhvienDTO.Masv,
                Tensv = sinhvienDTO.Tensv,
                Ngaysinh = sinhvienDTO.Ngaysinh,
                Gioitinh = sinhvienDTO.Gioitinh,
                Makhoa = sinhvienDTO.Makhoa,
            };
            try
            {
                _context.Sinhviens.Add(sinhvien);
                await _context.SaveChangesAsync();
                /*return CreatedAtAction(nameof(GetSinhvien), new { id = sinhvien.Masv }, sinhvien);*/
                
            }
            catch (Exception)
            {

                return BadRequest();
            }


            return Ok();
        }

        // DELETE: api/Sinhviens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSinhvien(int id)
        {
            if (_context.Sinhviens == null)
            {
                return NotFound();
            }
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound();
            }

            _context.Sinhviens.Remove(sinhvien);
            await _context.SaveChangesAsync();

            /*return NoContent();*/
            return Ok();
        }


        private bool SinhvienExists(int id)
        {
            return (_context.Sinhviens?.Any(e => e.Masv == id)).GetValueOrDefault();
        }

        private static SinhvienDTO SvDTO(Sinhvien sinhvien) => new SinhvienDTO
        {
            Masv = sinhvien.Masv,
            Tensv = sinhvien.Tensv,
            Ngaysinh = sinhvien.Ngaysinh,
            Gioitinh = sinhvien.Gioitinh,
            Makhoa = sinhvien.Makhoa
        };
    }
}
