using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BusinessCore.Interfaces;

using Quorum.BusinessCore.Interfaces;
using Quorum.DataAccess.AdoDataProvider.Base;
using Quorum.DataAccess.AdoDataProvider.Extensions;
using Quorum.Entities;

using SqlKata.Execution;

namespace Quorum.DataAccess.AdoDataProvider.Repositories
{
	public class TestRepository : AdoRepositoryBase<Test>, ITestRepository
	{
		private readonly ITagRepository _tagRepository;
		private readonly IQuestionRepository _questionRepository;

		public TestRepository(QueryFactory queryFactory,
							  ITagRepository tagRepository,
							  IQuestionRepository questionRepository) : base(queryFactory)
		{
			_tagRepository = tagRepository;
			_questionRepository = questionRepository;
		}

		public override async Task<int> Create(Test test)
		{
			var id = await Query.InsertReturningIdAsync<int>(new
			{
				test.Name,
				test.Description
			});

			test.Id = id;

			foreach (var tag in test.Tags)
			{
				tag.TestId = id;
			}

			foreach (var question in test.Questions)
			{
				question.TestId = id;
			}

			await _tagRepository.Create(test.Tags);
			await _questionRepository.Create(test.Questions);

			return id;
		}

		public override async Task<Test> GetByIdAsync(int id)
		{
			var tests = await Query.Where("Id", id).GetAsync<Test>();

			var test = tests.FirstOrDefault();

			if (test == null)
			{
				return null;
			}

			test.Tags = await _tagRepository.GetByParentTestAsync(test);
			test.Questions = await _questionRepository.GetByParentTestAsync(test);

			return test;
		}

		public override async Task<ICollection<Test>> GetAll()
		{
			var tests = await Query.GetAsync<Test>();

			foreach (var test in tests)
			{
				test.Tags = await _tagRepository.GetByParentTestAsync(test);
				test.Questions = await _questionRepository.GetByParentTestAsync(test);
			}

			return tests.ToList();
		}
	}
}