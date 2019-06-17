using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchEngine _searchEngine;

        public SearchController(ISearchEngine searchEngine)
        {
            _searchEngine = searchEngine ?? throw new ArgumentNullException(nameof(searchEngine));
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string term)
        {
            var data = _searchEngine.GetSearchResult(term);
            return Ok(data);
        }
    }
}