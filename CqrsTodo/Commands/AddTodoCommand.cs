using System;
using MediatR;
namespace CqrsTodo
{
    public class AddTodoCommand : IRequest<Guid>
    {
        public string Name { get; set; }

    }
}