using System;
using MediatR;
namespace CqrsTodo
{
    public class CompleteTodoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}