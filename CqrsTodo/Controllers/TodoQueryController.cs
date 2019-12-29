using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CqrsTodo.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoQueryController : ControllerBase
    {
        private readonly TodoDbContext _db;

        public TodoQueryController(TodoDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAll()
        {
            return await _db.Todoes.ToListAsync();
        }

        [HttpGet]
        public async Task<ActionResult<TodaySummary>> GetSummary()
        {

            var summary = await _db.TodoSummaries.FirstOrDefaultAsync(x => x.Date == DateTime.Today);
            if (summary == null) return new TodaySummary();
            else return new TodaySummary
            {
                AddedCount = summary.AddedCount,
                CompletedCount = summary.CompletedCount
            };

        }

    }
}