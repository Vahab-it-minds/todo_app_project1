using backend.classes;

namespace backend.models
{
    public class UpdateTodoRequest
    {
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public int? TimeEstimate { get; set; }
        public string? Category { get; set; }
        public bool IsCompleted { get; set; }
    }
}