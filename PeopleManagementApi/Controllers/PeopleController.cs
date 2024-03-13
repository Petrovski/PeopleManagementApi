using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleManagementApi.Services;

namespace PeopleManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            this.peopleRepository = peopleRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Person>))]
        public IActionResult GetAll()
        {
            return Ok(peopleRepository.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Person))]
        public IActionResult GetById(int id)
        {
            var person = peopleRepository.GetById(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Person))]
        public IActionResult Add([FromBody] Person newPerson)
        {
            if (newPerson.Id < 1)
            {
                return BadRequest("Invalid person ID");
            }

            peopleRepository.Add(newPerson);
            return CreatedAtAction(nameof(GetById), new { id = newPerson.Id }, newPerson);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int personId)
        {
            try
            {
                peopleRepository.Delete(personId);
            }

            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
