using System;
namespace CqrsTodo
{
    public class Todo
    {
        public Todo()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Completed { get; set; }
    }
}