using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Todo.Commands.Commands;

namespace CqrsTodo
{
    public class CompleteTodoCommandHadler : IRequestHandler<CompleteTodoCommand, bool>
    {
        private readonly TodoDbContext _context;
        private readonly IEventBus _eventbus;

        public CompleteTodoCommandHadler(TodoDbContext context, IEventBus eventbus)
        {
            this._context = context;
        }
        public async Task<bool> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _context.Todoes.FindAsync(request.Id);
            todo.Completed = true;
            _context.Todoes.Update(todo);

            var summary = await _context.TodoSummaries.Where(x => x.Date == DateTime.Today).FirstOrDefaultAsync();
            if (summary == null)
            {

                summary = new TodoSummary { Date = DateTime.Today, CompletedCount = 1 };
                _context.TodoSummaries.Add(summary);
            }
            else
            {
                summary.CompletedCount += 1;
                _context.TodoSummaries.Update(summary);
            }

            await _context.SaveChangesAsync();
            //notify query service to update
            Console.WriteLine("Publish command from CompleteTodo");
            _eventbus.Publish(new TodoIntegrationEvent() { Todo = todo, Summary = summary });
            return true;
        }
    }
}