using GrInterview.Api.Db;
using GrInterview.Api.Models;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GrInterview.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IParser<User> _parser;
        private readonly IUserRepository _repository;

        public RecordsController(IParser<User> parser, IUserRepository repository)
        {
            _parser = parser;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        public async Task<ActionResult> Post([FromBody] DelimitedRecord record)
        {
            using var reader = new StringReader(record.Data!);
            var userRecords = await _parser.Parse(reader, false);
            _repository.AddUser(userRecords.FirstOrDefault());
            return StatusCode(201, userRecords.FirstOrDefault());
        }

        [HttpGet("color")]
        public IEnumerable<User> GetColor()
        {
            return _repository
                .GetAllUsers()
                .SortByColor();
        }

        [HttpGet("birthdate")]
        public IEnumerable<User> GetBirth()
        {
            return _repository
                .GetAllUsers()
                .SortByBirthdate();
        }

        [HttpGet("name")]
        public IEnumerable<User> GetLastName()
        {
            return _repository
                .GetAllUsers()
                .SortByLastName();
        }
    }
}