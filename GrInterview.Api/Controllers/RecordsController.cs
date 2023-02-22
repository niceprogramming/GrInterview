using GrInterview.Api.Db;
using GrInterview.Api.Models;
using GrInterview.Common.Interfaces;
using GrInterview.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;

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
            try
            {
                using var reader = new StringReader(record.Data!);
                var userRecords = (await _parser.Parse(reader, false)).ToList();
                _repository.AddUser(userRecords.FirstOrDefault()!);
                return StatusCode(201, userRecords.FirstOrDefault());
            }
            catch (InvalidDataException e)
            {

                return Problem(e.Message, title: "unable to parse supplied data", statusCode: 400);
            }
            
         
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