using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.DataAccess;
using Api.Models;
using Api.Migrations;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsgsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MsgsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Msgs
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Msgs>>> GetMsgs()
        //{
        //    return await _context.Msgs.ToListAsync();
        //}

        

        [Route("messagesRead")]
        [HttpPost]
        public async Task<ActionResult> SetMessagesRead([FromBody] List<int> ids)
        {
            List<Msgs> listOfMessages = new List<Msgs>();
            if(ids != null)
            {
                foreach (var item in ids)
                    listOfMessages.Add((Msgs)_context.Msgs.Where(x => x.Message_ID == item));

                foreach(var item in listOfMessages)
                {
                    await messageIsRead(item.Message_ID);
                }

                return StatusCode(202);

            }

            return BadRequest();
        }

        // GET: api/Msgs/tester1233
        [HttpGet("{usernameTo}")]
        public async Task<ActionResult<IEnumerable<string>>> GetMsgs(string usernameTo)
        {
            var msgs = await _context.Msgs.Where(x => (x.To == usernameTo || x.From == usernameTo ) ).ToListAsync();

            if (msgs == null)
            {
                return NotFound();
            }

            List<string> listOfNames = new List<string>();

            foreach(var item in msgs)
            {
                if(item.From == usernameTo)
                {
                    listOfNames.Add(item.To);
                }
                else
                {
                    listOfNames.Add(item.From);
                }                
            };

            return listOfNames.Distinct().ToList();
        }



        // PUT: api/Msgs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutMsgs(int id, Msgs msgs)
        //{
        //    if (id != msgs.Message_ID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(msgs).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!MsgsExists(id))
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

        [HttpGet("messages/{username}/{usernameTwo}")]
        public async Task<ActionResult<IEnumerable<Msg>>> GetAllMessagesBeetwen(string username, string usernameTwo)
        {
            var msgs = await _context.Msgs.Where(x => (x.From == username && x.To == usernameTwo) || (x.To == username && x.From == usernameTwo) ).ToListAsync();

            if (msgs == null)
            {
                return NotFound();
            }

            List<Msg> listOfMessages = new List<Msg>();

            foreach (var item in msgs)
            {
                listOfMessages.Add(new Msg() 
                { 
                    Value = item.Value,
                    To = item.To,
                    From = item.From,
                    Read = item.Read
                });
            }

            return listOfMessages;
        }

        // POST: api/Msgs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostMsgs(Msg msg)
        {
            Msgs newMsg = new Msgs()
            {
                Value = msg.Value,
                From = msg.From,
                To = msg.To,
                Read = false
            };

            _context.Msgs.Add(newMsg);

            var accepted = await _context.SaveChangesAsync();

            if (accepted > 0)
                return StatusCode(201);
            else
                return BadRequest();
        }

        // DELETE: api/Msgs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Msgs>> DeleteMsgs(int id)
        {
            var msgs = await _context.Msgs.FindAsync(id);
            if (msgs == null)
            {
                return NotFound();
            }

            _context.Msgs.Remove(msgs);
            await _context.SaveChangesAsync();

            return msgs;
        }

        private async Task messageIsRead(int idOfMessage)
        {
            var msg = _context.Msgs.First(x => x.Message_ID == idOfMessage);

            if(msg != null)
            {
                msg.Read = true;
                await _context.SaveChangesAsync();
            }
        }

        private bool MsgsExists(int id)
        {
            return _context.Msgs.Any(e => e.Message_ID == id);
        }
    }
}
