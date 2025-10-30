using System.ComponentModel.DataAnnotations;

namespace ayuna_main.ViewModel
{
	public class RegisterVM
	{
		[Required(ErrorMessage = "Fill Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Fill Surname")]
		public string Surname { get; set; }

		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Fill Email")]		
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[StringLength(20, MinimumLength = 8, ErrorMessage = "password 8-20 simvol araligindadir")]
		[Required(ErrorMessage = "Fill Password")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage ="Sifreni duzgun tekrar edin")]
		[Required(ErrorMessage = "Fill ConfirmPassword")]		
		public string ConfirmPassword { get; set; }
	}
}
