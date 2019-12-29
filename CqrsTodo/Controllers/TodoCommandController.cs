using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsTodo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoCommandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoCommandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> AddTodo(string name)
        {
            return await _mediator.Send<Guid>(new AddTodoCommand { Name = name });
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CompleteTodo(Guid id)
        {
            return await _mediator.Send<bool>(new CompleteTodoCommand { Id = id });
        }

    }
}