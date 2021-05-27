using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.DotNet6.WebApi.ToDo
{
	public record TodoAddCommand(string Text) : IRequest<Unit>;

	public class TodoAddHandler : IRequestHandler<TodoAddCommand, Unit>
	{
		private readonly List<TodoItem> _todoItems;
		public TodoAddHandler(List<TodoItem> todoItems) => _todoItems = todoItems;

		public Task<Unit> Handle(TodoAddCommand request, CancellationToken cancellationToken)
		{
			var id = _todoItems.Count == 0 ? 1 : _todoItems.Count + 1;
			_todoItems.Add(new TodoItem(id, request.Text));

			return Task.FromResult(new Unit());
		}
	}
}
