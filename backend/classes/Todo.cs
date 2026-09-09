namespace backend.classes{
    public enum Priority
    {
        Low,
        Medium,
        High
    }
    
    public class Todo {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public DateTime DueDate { get; private set; }
        public int UserId { get; private set; }
        public Priority Priority { get; private set; }
        public int TimeEstimate { get; private set; }
        public string Category { get; private set; }
        public bool IsCompleted { get; private set; }

        public Todo(int id, string title, string description, DateTime createdAt, DateTime updatedAt, DateTime dueDate, int userId, Priority priority, int timeEstimate, string category, bool isCompleted) {
            Id = id;
            Title = title;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            DueDate = dueDate;
            UserId = userId;
            Priority = priority;
            TimeEstimate = timeEstimate;
            Category = category;
            IsCompleted = isCompleted;
        }

        public void SetTitle(string title) {
            Title = title;
        }

        public void SetDescription(string description) {
            Description = description;
        }

        public void SetUpdatedAt(DateTime updatedAt) {
            UpdatedAt = updatedAt;
        }

        public void SetDueDate(DateTime dueDate) {
            DueDate = dueDate;
        }

        public void SetPriority(Priority priority) {
            Priority = priority;
        }

        public void SetTimeEstimate(int timeEstimate) {
            TimeEstimate = timeEstimate;
        }

        public void SetCategory(string category) {
            Category = category;
        }

        public void SetIsCompleted(bool isCompleted) {
            IsCompleted = isCompleted;
        }
           
    }
}