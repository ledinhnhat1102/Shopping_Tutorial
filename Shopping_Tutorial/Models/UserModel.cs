using System.ComponentModel.DataAnnotations;

namespace Shopping_Tutorial.Models
{
	public class UserModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter UserName")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Please enter Email"), EmailAddress]
		public string Email { get; set; }

		[DataType(DataType.Password), Required(ErrorMessage = "Please enter Password")]
		public string Password { get; set; }
	}
}
