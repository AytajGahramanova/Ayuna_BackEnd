using System.ComponentModel.DataAnnotations;

namespace ayuna_main.ViewModel
{
	public class LoginVM
	{
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Email is not empty")]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password is not empty")]
		public string Password { get; set; }
	}
}
