using System.ComponentModel.DataAnnotations;

namespace Shopping_Tutorial.Models.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter UserName")]
		public string UserName { get; set; }


		[DataType(DataType.Password), Required(ErrorMessage = "Please enter Password")]
		public string Password { get; set; }

		public string ReturnUrl { get; set; }
	}
}
