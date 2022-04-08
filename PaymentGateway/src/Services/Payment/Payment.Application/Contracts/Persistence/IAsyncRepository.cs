using PaymentNs.Domain.Common;
using System.Linq.Expressions;

namespace PaymentNs.Application.Contracts.Persistence
{
	public interface IAsyncRepository<T> where T : EntityBase
	{
		// Get all
		Task<IReadOnlyList<T>> GetAllAsync();
		// Get with simple filter expression
		Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
		// Get with filter expression with sorting and include for join
		Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			string? includeString = null,
			bool disableTracking = true
		);
		// Get with filter expression with sorting and include (as a function) for join
		Task<IReadOnlyList<T>> GetAsync(
			Expression<Func<T, bool>>? predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
			List<Expression<Func<T, object>>>? includes = null,
			bool disableTracking = true
		);
		// Get by ID
		Task<T> GetByIdAsync(int id);
		// Create
		Task<T> CreateAsync(T entity);
		// Update
		Task UpdateAsync(T entity);
		// Delete
		Task DeleteAsync(T entity);
	}
}
