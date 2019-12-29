using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace CqrsTodo
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, Guid>
    {
        private readonly TodoDbContext _context;
        public AddTodoCommandHandler(TodoDbContext context)
        {
            this._context = context;
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
            return todo.Id;
        }
    }
}