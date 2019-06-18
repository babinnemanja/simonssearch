using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchEngine _searchEngine;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        public SearchController(ISearchEngine searchEngine, ILoggerFactory loggerFactory)
        {
            _searchEngine = searchEngine ?? throw new ArgumentNullException(nameof(searchEngine));
            _loggerFactory = loggerFactory ??  throw new ArgumentNullException(nameof(loggerFactory));

            _logger = _loggerFactory.CreateLogger(nameof(SearchController));
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string term)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    return new OkObjectResult(new SearchResult());

                }

                var data = _searchEngine.GetSearchResult(term);

                return new OkObjectResult(data);

            } catch (Exception ex)
            {
                _logger.LogError($"There was an error while executing serch request {ex.Message}");
                return new BadRequestObjectResult("There was an error while executing serch request");
            }
            
        }
    }
}