using System.ComponentModel.DataAnnotations;

namespace APIValidationDemo.Commands
{
    public class CreateItemCommand
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [MinValue(1, ErrorMessage = "Must be greater than 0")]
        public int Amount { get; set; }
    }
}
