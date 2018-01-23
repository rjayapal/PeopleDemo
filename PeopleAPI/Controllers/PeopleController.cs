using Microsoft.AspNetCore.Mvc;
using PeopleAPI.Infrastructure;
using System.Net;

namespace PeopleAPI.Controllers
{
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        IPersonRepository _personRepository;
        public PeopleController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Get names of cats by owner's gender.
        /// </summary>
        /// <remarks>
        /// Sample requests:
        ///
        ///     GET /api/people
        ///
        /// </remarks>
        /// <returns>List of pet names grouped by owner's gender</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _personRepository.GetPeople();
            if (result == null)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            else
                return Ok(result);
        }

    }
}
