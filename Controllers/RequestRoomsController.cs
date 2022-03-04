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
    public class RequestRoomsController : ControllerBase
    {
        private readonly RequestDbContext _context;

        public RequestRoomsController(RequestDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestRooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestRoom>>> GetRequestRooms()
        {
            return await _context.RequestRooms.Include(r => r.Room).ToListAsync();
        }

        // GET: api/RequestRooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestRoom>> GetRequestRoom(int id)
        {
            var requestRoom = await _context.RequestRooms.FindAsync(id);

            if (requestRoom == null)
            {
                return NotFound();
            }

            return requestRoom;
        }

        // PUT: api/RequestRooms/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestRoom(int id, RequestRoom requestRoom)
        {
            if (id != requestRoom.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestRoomExists(id))
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

        // POST: api/RequestRooms
        [HttpPost]
        public async Task<ActionResult<RequestRoom>> PostRequestRoom(RequestRoom requestRoom)
        {
            requestRoom.RequestDate = DateTime.Now;
            _context.RequestRooms.Add(requestRoom);
            var room = await _context.Rooms.FindAsync(requestRoom.RoomId);
            room.Status = "Pending";

            _context.Entry(room).Property(m => m.Status).IsModified = true;

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequestRoom", new { id = requestRoom.Id }, requestRoom);
        }

        // DELETE: api/RequestRooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestRoom(int id)
        {
            var requestRoom = await _context.RequestRooms.FindAsync(id);
            if (requestRoom == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(requestRoom.RoomId);
            room.Status = null;

            _context.RequestRooms.Remove(requestRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestRoomExists(int id)
        {
            return _context.RequestRooms.Any(e => e.Id == id);
        }
    }
}
