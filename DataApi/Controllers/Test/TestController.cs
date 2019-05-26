using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quorum.BusinessCore.Interfaces;
using Quorum.BusinessCore.Interfaces.Repositories;
using Quorum.DataApi.Controllers.Test.ResultModels;
using Quorum.DataApi.Controllers.Test.ViewModels;
using Quorum.Shared.Extensions;
using ControllerBase = Quorum.DataApi.Base.ControllerBase;

namespace Quorum.DataApi.Controllers.Test
{
	[Route("api/test")]
	public sealed class TestController : ControllerBase
	{
		private readonly ITestRepository _testRepository;

		public TestController(ITestRepository testRepository)
		{
			_testRepository = testRepository;
		}

		[HttpPost]
		[Authorize(Roles = Domain.Entities.Enums.UserRole.Tutor)]
		public async Task<ActionResult<int>> Create([FromBody] CreateTestViewModel test)
		{
			var entity = test.MapTo<Domain.Entities.Domain.Test>();

			entity.UserId = UserId;

			await _testRepository.CreateAsync(entity);

			return entity.Id;
		}

		[HttpGet]
		[Authorize(Roles = Domain.Entities.Enums.UserRole.Tutor)]
		public async Task<ActionResult<IEnumerable<TestPreviewResultModel>>> GetTestsPreviews()
		{
			var tests = await _testRepository.GetTutorOwnTestsAsync(UserId);

			return tests.Select(t => t.MapTo<TestPreviewResultModel>()).ToList();
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = Domain.Entities.Enums.UserRole.Tutor)]
		public async Task<ActionResult<int>> DeleteTest(int id)
		{
			var test = await _testRepository.GetByIdAsync(id);

			if (test == null)
			{
				return NotFound(id);
			}

			await _testRepository.DeleteAsync(test);
			return Ok(id);
		}
	}
}