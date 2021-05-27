using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.DotNet6.WebApi.ToDo
{
	public record TodoQuery(int Id) : IRequest<TodoItem>;

	public class TodoQueryHandler : IRequestHandler<TodoQuery, TodoItem>
	{
		private readonly List<TodoItem> _todoItems;
		public TodoQueryHandler(List<TodoItem> todoItems) => _todoItems = todoItems;

		public Task<TodoItem> Handle(TodoQuery request, CancellationToken cancellationToken) =>
			Task.FromResult(_todoItems.FirstOrDefault(todo => todo.Id == request.Id));
	}
}
