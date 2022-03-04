using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Model;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestEquipmentsController : ControllerBase
    {
        private readonly RequestDbContext _context;

        public RequestEquipmentsController(RequestDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestEquipments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestEquipment>>> GetRequestEquipments()
        {
            return await _context.RequestEquipments.ToListAsync();
        }

        // GET: api/RequestEquipments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestEquipment>> GetRequestEquipment(int id)
        {
            var requestEquipment = await _context.RequestEquipments.FindAsync(id);

            if (requestEquipment == null)
            {
                return NotFound();
            }

            return requestEquipment;
        }

        // PUT: api/RequestEquipments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestEquipment(int id, RequestEquipment requestEquipment)
        {
            if (id != requestEquipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestEquipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestEquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RequestEquipments
        [HttpPost]
        public async Task<ActionResult<RequestEquipment>> PostRequestEquipment(RequestEquipment requestEquipment)
        {
            requestEquipment.RequestDate = DateTime.Now;
            _context.RequestEquipments.Add(requestEquipment);
            var equipment = await _context.Equipments.FindAsync(requestEquipment.EquipmentId);
            if (equipment.Quantity < requestEquipment.Quantity) return BadRequest(new { message = "Quantity invalid" });
            equipment.Status = "Pending";
            equipment.Quantity -= requestEquipment.Quantity;
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestEquipment", new { id = requestEquipment.Id }, requestEquipment);
        }

        // DELETE: api/RequestEquipments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestEquipment(int id)
        {
            var requestEquipment = await _context.RequestEquipments.FindAsync(id);
            if (requestEquipment == null)
            {
                return NotFound();
            }
            var equipment = await _context.Equipments.FindAsync(requestEquipment.EquipmentId);
            equipment.Status = null;
            equipment.Quantity += requestEquipment.Quantity;

            _context.RequestEquipments.Remove(requestEquipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestEquipmentExists(int id)
        {
            return _context.RequestEquipments.Any(e => e.Id == id);
        }
    }
}
