using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Data
{
    public static class EntityExtensions
    {

        public static KonferansDTO.SessionResponse MapSessionResponse(this Session session) =>
            new KonferansDTO.SessionResponse
            {
                Id = session.Id,
                Title = session.Title,
                StartTime = session.StartTime,
                EndTime = session.EndTime,
                Speakers = session.SessionSpeakers?
                                   .Select(ss => new KonferansDTO.Speaker
                                   {
                                       Id = ss.SpeakerId,
                                       Name = ss.Speaker.Name
                                   })
                                   .ToList(),
                TrackId = session.TrackId,
                Track = new KonferansDTO.Track
                {
                    Id = session?.TrackId ?? 0,
                    Name = session.Track.Name
                },
                Abstract = session.Abstract

            };

        public static KonferansDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new KonferansDTO.SpeakerResponse
            {
                Id = speaker.Id,
                Name = speaker.Name,
                Bio = speaker.Bio,
                Website = speaker.Website,
                Sessions = speaker.SessionSpeakers?
                        .Select(ss =>
                        new KonferansDTO.Session
                        {
                            Id = ss.SessionId,
                            Title = ss.Session.Title
                        })
                        .ToList(),
               
            };

        public static KonferansDTO.ParticipantResponse MapParticipantRespose(this Participant participant) =>
            new KonferansDTO.ParticipantResponse
            {
               Id = participant.Id,
               FirstName = participant.FirstName,
               LastName = participant.LastName,
               UserName = participant.UserName,
               EmailAddress = participant.EmailAddress,

               Sessions = participant.SessionParticipants?
                .Select(sa=>
                new KonferansDTO.Session
                {
                    Id = sa.SessionId,
                    Title = sa.Session.Title,
                    StartTime = sa.Session.StartTime,
                    EndTime = sa.Session.EndTime
                })
                .ToList(),
                

            };
    }
}
