using KonferansDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Frontend.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> AddParticipantAsync(Participant participant)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/participants", participant);

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                return false;
            }
            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<ParticipantResponse> GetParticipantAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            { 
                return null;
            }
            var response = await _httpClient.GetAsync($"/api/participants/{name}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ParticipantResponse>();
        }


        public async Task<SessionResponse> GetSessionAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/sessions/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SessionResponse>();
        }
        public async Task<List<SessionResponse>> GetSessionsAsync()
        {
            var response = await _httpClient.GetAsync("/api/sessions");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<SessionResponse>>();
        }
        public async Task DeleteSessionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/sessions/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return;
            }

            response.EnsureSuccessStatusCode();
        }
        public async Task<SpeakerResponse> GetSpeakerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/speakers/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SpeakerResponse>();
        }

        public async Task<List<SpeakerResponse>> GetSpeakersAsync()
        {
            var response = await _httpClient.GetAsync("/api/speakers");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<SpeakerResponse>>();
        }

        public async Task PutSessionAsync(Session session)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/sessions/{session.Id}", session);

            response.EnsureSuccessStatusCode();
        }

        public Task GetSessionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
