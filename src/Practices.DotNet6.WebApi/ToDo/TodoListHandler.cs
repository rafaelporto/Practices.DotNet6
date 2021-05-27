using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.DotNet6.WebApi.ToDo
{
	public record TodoListQuery(int Limit, int Skip) : IRequest<IEnumerable<TodoItem>>;

	public class TodoListHandler : IRequestHandler<TodoListQuery, IEnumerable<TodoItem>>
	{
		private readonly List<TodoItem> _todoItems;
		public TodoListHandler(List<TodoItem> todoItems) => _todoItems = todoItems;

		public Task<IEnumerable<TodoItem>> Handle(TodoListQuery request, CancellationToken cancellationToken) =>
			Task.FromResult(_todoItems.Skip(request.Skip).Take(request.Limit));
	}
}
