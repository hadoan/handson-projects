using CqrsTodo;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Queries.Listeners
{
    public class TodoListener: IEventBusListener
    {
        private readonly IEventBus _eventbus;

        public TodoListener(IEventBus eventbus)
        {
            
            _eventbus = eventbus;
            //_eventbus.Subscribe<TodoIntegrationEvent, TodoIntegrationEventHandler>();
        }

        public void SubscribeEvents()
        {
            Console.WriteLine("Call TodoListenr constructor!");
            _eventbus.Subscribe<TodoIntegrationEvent, TodoIntegrationEventHandler>();
        }
    }

    public class TodoIntegrationEventHandler : IIntegrationEventHandler<TodoIntegrationEvent>
    {
        private readonly TodoDbContext _context;

        public TodoIntegrationEventHandler(TodoDbContext context)
        {
            _context = context;
        }

        public async Task Handle(TodoIntegrationEvent @event)
        {
            Console.WriteLine("rececived event: " + JsonConvert.SerializeObject(@event));

            if (!await _context.Todoes.AnyAsync(x => x.Id == @event.Todo.Id))
                _context.Todoes.Add(@event.Todo);
            else _context.Todoes.Update(@event.Todo);

            if (!await _context.TodoSummaries.AnyAsync(x => x.Id == @event.Summary.Id))
                _context.TodoSummaries.Add(@event.Summary);
            else _context.TodoSummaries.Update(@event.Summary);

            await _context.SaveChangesAsync();

            Console.WriteLine("sync database successfully!");
        }
    }
}
