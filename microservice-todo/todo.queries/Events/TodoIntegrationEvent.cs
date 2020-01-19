using CqrsTodo;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo
{
    public class TodoIntegrationEvent : IntegrationEvent
    {
        public CqrsTodo.Todo Todo { get; set; }
        public TodoSummary Summary { get; set; }
    }
}
