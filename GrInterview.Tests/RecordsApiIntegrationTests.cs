using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;
using GrInterview.Api;
using GrInterview.Api.Controllers;
using GrInterview.Api.Db;
using GrInterview.Api.Models;
using GrInterview.Common.Models;
using GrInterview.Common.Parsers;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace GrInterview.Tests
{
    public class RecordsApiIntegrationTests : IClassFixture<TestWebApplicationFactory>
    {
        private static readonly Func<IEnumerable<User>, IEnumerable<User>> ColorSorter = users => users.SortByColor();
        private static readonly Func<IEnumerable<User>, IEnumerable<User>> NameSorter = users => users.SortByLastName();
        private static readonly Func<IEnumerable<User>, IEnumerable<User>> BirthSorter = users => users.SortByBirthdate();

        private readonly TestWebApplicationFactory _application;
        private readonly IUserRepository _repo;
        private readonly HttpClient _http;


        public RecordsApiIntegrationTests(TestWebApplicationFactory application)
        {
            _application = application;
            _repo = _application.Services.GetRequiredService<IUserRepository>();
            _http = _application.CreateClient();
        }
        [Fact]
        public async Task Posting_a_valid_record_adds_value_to_repository_with_created_response()
        {
            var expected = new User("Z", "Test", "test.user@gmail.com", "yellow", new DateTime(1994, 8, 26));
            var lines = File.ReadAllLines("./TestFiles/FullTest.txt").Skip(1);
            foreach (var line in lines)
            {
                var record = new DelimitedRecord()
                {
                    Data = line
                };
                var result = await _http.PostAsJsonAsync("/records", record);
                Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            }

            Assert.Equal(lines.Count(), _repo.GetAllUsers().Count());
            Assert.Equal(expected, _repo.GetAllUsers().FirstOrDefault());
        }

        [Fact]
        public async Task Posting_with_invalid_data_returns_400_with_errors()
        {
            var record = new DelimitedRecord
            {
                Data = "1,2,3,4"
            };
            var result = await _http.PostAsJsonAsync<DelimitedRecord>("/records", record);
            var content = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("Expected 5 columns", content);
        }


        [Fact]
        public async Task Posting_with_empty_body_returns_400_with_errors()
        {
            var result = await _http.PostAsJsonAsync<DelimitedRecord>("/records", null);
            var content = await result.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("One or more validation errors occurred.", content);
        }

        [Fact]
        public async Task Posting_with_empty_record_returns_400_with_errors()
        {
            var result = await _http.PostAsJsonAsync("/records", new DelimitedRecord());
            var content = await result.Content.ReadAsStringAsync();
            
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("The Data field is required", content);
        }

        [Theory]
        [MemberData(nameof(SortingData))]
        public async Task Getting_returns_sorted_data_equal_to_data_in_the_repository(string url, Func<IEnumerable<User>, IEnumerable<User>> sorter)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            
            options.Converters.Add(new DateTimeConverter());
            var result = await _http.GetFromJsonAsync<User[]>(url, options);
            var sortedData = sorter(_repo.GetAllUsers());
            Assert.Equal(sortedData, result);
        }

        public static IEnumerable<object[]> SortingData =>
            new List<object[]>
            {
                new object[] { "/records/color", ColorSorter },
                new object[] { "/records/birthdate", BirthSorter },
                new object[] { "/records/name", NameSorter },

            };


    }
}
