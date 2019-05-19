using System.Linq;
using System.Threading.Tasks;
using Quorum.BusinessCore.Interfaces;
using Quorum.DataAccess.AdoDataProvider.Base;
using Quorum.DataAccess.AdoDataProvider.Extensions;
using Quorum.Entities.Domain;
using SqlKata.Execution;

namespace Quorum.DataAccess.AdoDataProvider.Repositories
{
	public sealed class UserRepository : AdoRepositoryBase<User>, IUserRepository
	{
		public UserRepository(QueryFactory queryFactory) : base(queryFactory)
		{
		}

		public override async Task<int> CreateAsync(User entity)
		{
			Query.ResetConditions();
			
			var id = await Query.InsertReturningIdAsync<int>(entity);

			entity.Id = id;

			return id;
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			Query.ResetConditions();

			var users = await Query.Where("Email", email).GetAsync<User>();

			return users.FirstOrDefault();
		}
	}
}