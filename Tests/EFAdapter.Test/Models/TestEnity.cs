using System.ComponentModel.DataAnnotations;

namespace EFAdapter.Tests.Models
{
    public class TestEnity
    {
        [Key]
        public Guid ID { get; set; }

        public string? Name { get; set; }
        public string? Surname { get;set; }
        public int Age { get; set; }
        public DateTime CreatinDate { get; set; }

    }
}
