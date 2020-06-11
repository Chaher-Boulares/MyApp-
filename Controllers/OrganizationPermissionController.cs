using Microsoft.AspNetCore.Mvc;
using Organization.API.Infrastructure.Interfaces;
using Organization.API.Models;
using Organization.API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationPermissionController : Controller
    {

        private readonly IOrganizationPermissionRepository _orgPermissionRepository;
        public OrganizationPermissionController(IOrganizationPermissionRepository permissionRepository)
        {
            _orgPermissionRepository = permissionRepository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationPermission>>> Get()
        {
            return await _orgPermissionRepository.GetAll();
        }


        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationPermission>> Get(int id)
        {
            var movie = await _orgPermissionRepository.Get(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

        // PUT: api/[controller]/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateOraganizationSettings input)
        {
            var orga =_orgPermissionRepository.GetOrgPermission(input.UserId, input.EntityId);
            if(!(orga is null))
            {
                orga.AddEntities = input.AddEntities;
                orga.ChangeOrganizationProfile = input.ChangeOrganizationProfile;
                orga.ChangeSettings = input.ChangeSettings;
                await _orgPermissionRepository.Update(orga);
                return Ok(orga);
            }
            return Ok("not exist");
            
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<OrganizationPermission>> Post([FromBody] OrganizationPermission input)
        {
            var movie = new OrganizationPermission(input.UserId, input.EntityId, input.AddEntities, input.ChangeSettings, input.ChangeOrganizationProfile);
            await _orgPermissionRepository.Add(input);
            return CreatedAtAction("Get", new { id = movie.Id }, movie);
        }


        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrganizationPermission>> Delete(int id)
        {
            var movie = await _orgPermissionRepository.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }

    }
}
