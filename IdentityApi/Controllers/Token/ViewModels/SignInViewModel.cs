using System.ComponentModel.DataAnnotations;
using Quorum.Shared.Interfaces;

namespace Quorum.IdentityApi.Controllers.Token.ViewModels
{
	public sealed class SignInViewModel : IDataTransferObject
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}