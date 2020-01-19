using System;
namespace CqrsTodo
{
    public class TodoSummary
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int CompletedCount { get; set; }

        public int AddedCount { get; set; }
    }
}