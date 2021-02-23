using KonferansDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public interface IApiClient
    {

        Task<List<SessionResponse>> GetSessionsAsync();
        Task<SessionResponse> GetSessionAsync(int id);
        Task<List<SpeakerResponse>> GetSpeakersAsync();
        Task<SpeakerResponse> GetSpeakerAsync(int id);
        Task PutSessionAsync(Session session);
        Task<bool> AddParticipantAsync(Participant participant);
        Task<ParticipantResponse> GetParticipantAsync(string name);
        Task DeleteSessionAsync(int id);
        Task GetSessionAsync();

        Task<List<SearchResult>> SearchAsync(string query);
    }
}
