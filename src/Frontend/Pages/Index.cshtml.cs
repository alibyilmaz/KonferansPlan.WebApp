using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Frontend.Services;
using KonferansDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Frontend.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IApiClient _apiClient;

        public IndexModel(ILogger<IndexModel> logger, IApiClient apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }
        public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; }
        public IEnumerable<(int Offset, DayOfWeek? DayOfWeek)> DayOffSets { get; set; }
        public int CurrentDayOffSet { get; set; }

        public bool IsAdmin { get; set; }
        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        public async Task OnGetAsync(int day=0)
        {

            IsAdmin = User.IsAdmin();
            CurrentDayOffSet = day;

            var sessions = await _apiClient.GetSessionsAsync();

            var startDate = sessions.Min(s => s.StartTime?.Date);

            var offset = 0;
            DayOffSets = sessions.Select(s => s.StartTime?.Date)
                                  .Distinct()
                                  .OrderBy(d => d)
                                  .Select(day => (offset++, day?.DayOfWeek));

            var filterDate = startDate?.AddDays(day);

            Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
                                    .OrderBy(s => s.TrackId)
                                    .GroupBy(s => s.StartTime)
                                    .OrderBy(g => g.Key);

            
        }
    }
}
