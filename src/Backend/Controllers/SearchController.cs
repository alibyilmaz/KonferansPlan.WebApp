using Backend.Data;
using KonferansDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static KonferansDTO.SearchResult;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public SearchController( ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<List<SearchResult>>> Search(SearchTerm term)
        {
            var query = term.Query.ToLowerInvariant();
            var sessionResults = (await _applicationDbContext.Sessions.Include(s => s.Track)
                                                            .Include(s => s.SessionSpeakers)
                                                                .ThenInclude(ss => ss.Speaker)
                                                            .ToListAsync())
                                                            .Where(s =>
                                                                    s.Title.ToLowerInvariant().Contains(query) ||
                                                                    s.Track.Name.ToLowerInvariant().Contains(query)
                                                                    )
                                                            .ToList();
            var speakerResults = (await _applicationDbContext.Speakers.Include(s => s.SessionSpeakers)
                                                                    .ThenInclude(ss => ss.Session)
                                                                   .ToListAsync())
                                                                .Where(s =>
                                                                    (s.Name?.ToLowerInvariant().Contains(query) ?? false)||
                                                                    (s.Bio?.ToLowerInvariant().Contains(query) ?? false) ||
                                                                    (s.Website?.ToLowerInvariant().Contains(query) ?? false)
                                                                    )
                                                              .ToList();
            var results = sessionResults.Select(s => new SearchResult
            {
                Type = SearchResultType.Session,
                Session = s.MapSessionResponse()
            })
             .Concat(speakerResults.Select(s => new SearchResult
             {
                 Type = SearchResultType.Speaker,
                 Speaker = s.MapSpeakerResponse()
             }));
            return results.ToList();
        }
    }
}
