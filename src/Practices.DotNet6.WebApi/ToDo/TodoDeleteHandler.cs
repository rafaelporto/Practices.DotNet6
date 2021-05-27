using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Practices.DotNet6.WebApi.ToDo
{
	public record TodoDeleteCommand(int Id) : IRequest<Unit>;

	public class TodoDeleteHandler : IRequestHandler<TodoDeleteCommand, Unit>
	{
		private readonly List<TodoItem> _todoItems;
		public TodoDeleteHandler(List<TodoItem> todoItems) => _todoItems = todoItems;

		public Task<Unit> Handle(TodoDeleteCommand request, CancellationToken cancellationToken)
		{
			_todoItems.RemoveAll(todo => todo.Id == request.Id);
			return Task.FromResult(new Unit());
		}
	}
}
