using Backend.Data;
using KonferansDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    public class ParticipantsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ParticipantsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ParticipantResponse>> Get(string username)
        {
            var participant = await _db.Participants.Include(a => a.SessionParticipants)
                                                       .ThenInclude(sa => sa.Session)
                                                       .SingleOrDefaultAsync(a => a.UserName == username);
            if (participant == null)
            {
                return NotFound();
            }

            var result = participant.MapParticipantRespose();

            return result;
        }

        [HttpGet("{username}/sessions")]
        public async Task<ActionResult<List<SessionResponse>>> GetSessions(string username)
        {
            var sessions = await _db.Sessions.AsNoTracking()
                                .Include(s => s.Track)
                                .Include(s => s.SessionSpeakers)
                                    .ThenInclude(ss => ss.Speaker)
                                .Where(s => s.SessionParticipants.Any(sa => sa.Participant.UserName == username))
                                .Select(m => m.MapSessionResponse())
                                .ToListAsync();
            return sessions;
                        
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ParticipantResponse>> Post(KonferansDTO.Participant input)
        {
            var existingParticipant = await _db.Participants
                .Where(a => a.UserName == input.UserName)
                .FirstOrDefaultAsync();
            if (existingParticipant != null)
            {
                return Conflict(input);
            }
            var participant = new Data.Participant
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            _db.Participants.Add(participant);
            await _db.SaveChangesAsync();

            var result = participant.MapParticipantRespose();

            return CreatedAtAction(nameof(Get), new { username = result.UserName }, result);

        }

        [HttpPost("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParticipantResponse>> AddSession(string username, int sessionId)
        {
            var participant = await _db.Participants.Include(a => a.SessionParticipants)
                                                        .ThenInclude(sa => sa.Session)
                                                      .SingleOrDefaultAsync(a => a.UserName == username);
            if (participant == null)
            {
                return NotFound();
            }

            var session = await _db.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return BadRequest();
            }

            participant.SessionParticipants.Add(new SessionParticipant
            {
                ParticipantId = participant.Id,
                SessionId = sessionId
            });
            await _db.SaveChangesAsync();
            var result = participant.MapParticipantRespose();
            return result;
        }

        [HttpDelete("{username}/session/{sessionId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ParticipantResponse>> RemoveSession(string username, int sessionId)
        {
                var participant = await _db.Participants.Include(a => a.SessionParticipants)
                                                .SingleOrDefaultAsync(a => a.UserName == username);
                if (participant == null)
                {
                    return NotFound();
                }

            var session = await _db.Sessions.FindAsync(sessionId);

            if (session == null)
            {
                return BadRequest();
            }

            var sessionParticipant = participant.SessionParticipants.FirstOrDefault(sa => sa.SessionId == sessionId);
            participant.SessionParticipants.Remove(sessionParticipant);

            await _db.SaveChangesAsync();

            return NoContent();
                
        }
        
    }
}
