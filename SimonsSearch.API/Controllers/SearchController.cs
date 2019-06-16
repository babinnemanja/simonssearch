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
        private readonly ISearchRepository _searchRepository;

        public SearchController(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository ?? throw new ArgumentNullException(nameof(searchRepository));
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string term)
        {
            var data = _searchRepository.LoadData();
            return Ok();
        }
    }
}