using System.ComponentModel.DataAnnotations;

namespace GrInterview.Api.Models
{
    public class DelimitedRecord
    {
        [Required]
        public string? Data { get; init; }
    }
}
