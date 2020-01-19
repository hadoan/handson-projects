using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Todo.Commands.Commands;

namespace CqrsTodo
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Guid>
    {
        private readonly TodoDbContext _context;
        private readonly IEventBus _eventbus;
        private readonly ILogger<AddTodoCommandHandler> _logger;
        public AddTodoCommandHandler(TodoDbContext context, IEventBus eventbus, ILogger<AddTodoCommandHandler> logger)
        {
            this._context = context;
            _eventbus = eventbus;
            _logger = logger;
        }

        public async Task<Guid> Handle(AddTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo { Name = request.Name };
            _context.Todoes.Add(todo);
            var summary = await _context.TodoSummaries.Where(x => x.Date == DateTime.Today).FirstOrDefaultAsync();
            if (summary == null)
            {
                summary = new TodoSummary { Date = DateTime.Today, AddedCount = 1 };
                _context.TodoSummaries.Add(summary);
            }
            else
            {
                summary.AddedCount += 1;
                _context.TodoSummaries.Update(summary);
            }
            await _context.SaveChangesAsync();

            //notify query service to update
            _logger.LogDebug("Publish command from AddTodo");
            _eventbus.Publish(new TodoIntegrationEvent() { Todo = todo, Summary = summary });
            return todo.Id;
        }
    }
}