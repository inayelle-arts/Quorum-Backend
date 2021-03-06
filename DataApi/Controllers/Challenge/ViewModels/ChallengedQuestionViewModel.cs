using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Quorum.Shared.Interfaces;

namespace Quorum.DataApi.Controllers.Challenge.ViewModels
{
	public sealed class ChallengedQuestionViewModel : IDataTransferObject
	{
		[Required]
		public int SourceQuestionId { get; set; }

		[Required]
		[MinLength(2)]
		public ICollection<ChallengedAnswerViewModel> Answers { get; set; }
	}
}