using CqrsTodo;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
namespace Todo.Commands.Commands
{
    public class TodoIntegrationEvent:IntegrationEvent
    {
        public CqrsTodo.Todo Todo { get; set; }
        public TodoSummary Summary { get; set; }
    }
}
