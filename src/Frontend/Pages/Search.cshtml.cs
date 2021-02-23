using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Services;
using KonferansDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Frontend.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public SearchModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        public string Term { get; set; }
        public List<SearchResult> SearchResults { get; set; }
        public async Task OnGetAsync(string term)
        {
            if (string.IsNullOrEmpty(term))
            {
                SearchResults = new List<SearchResult>();
                return;
            }
            Term = term;
            SearchResults = await _apiClient.SearchAsync(term);
        }
    }
}
