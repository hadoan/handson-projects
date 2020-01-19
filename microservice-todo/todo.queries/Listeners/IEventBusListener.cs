using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Queries.Listeners
{
    public interface IEventBusListener
    {
        void SubscribeEvents();
    }
}
