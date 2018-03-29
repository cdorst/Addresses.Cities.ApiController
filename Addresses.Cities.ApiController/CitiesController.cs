// Copyright © Christopher Dorst. All rights reserved.
// Licensed under the GNU General Public License, Version 3.0. See the LICENSE document in the repository root for license information.

using Addresses.Cities.DatabaseContext;
using DevOps.Code.DataAccess.Interfaces.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Addresses.Cities.ApiController
{
    /// <summary>ASP.NET Core web API controller for City entities</summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        /// <summary>Represents the application events logger</summary>
        private readonly ILogger<CitiesController> _logger;

        /// <summary>Represents repository of City entity data</summary>
        private readonly IRepository<CityDbContext, City, int> _repository;

        /// <summary>Constructs an API controller for City entities using the given repository service</summary>
        public CitiesController(ILogger<CitiesController> logger, IRepository<CityDbContext, City, int> repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>Handles HTTP GET requests to access City resources at the given ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int id)
        {
            if (id < 1) return NotFound();
            var resource = await _repository.FindAsync(id);
            if (resource == null) return NotFound();
            return resource;
        }

        /// <summary>Handles HTTP HEAD requests to access City resources at the given ID</summary>
        [HttpHead("{id}")]
        public ActionResult<City> Head(int id) => null;

        /// <summary>Handles HTTP POST requests to save City resources</summary>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<City>> Post(City resource)
        {
            var saved = await _repository.AddAsync(resource);
            return CreatedAtAction(nameof(Get), new { id = saved.GetKey() }, saved);
        }
    }
}
