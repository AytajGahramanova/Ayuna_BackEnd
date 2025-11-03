using System.ComponentModel.DataAnnotations;

namespace ayuna_main.Models
{
	public class ContactSubmit
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		[StringLength(30, MinimumLength = 3, ErrorMessage = "Name is between 3-30 characters")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; }
		public string Message { get; set; }
	}
}
