namespace backend.classes{
    public class Todo {
        private int Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private DateTime CreatedAt { get; set; }
        private DateTime UpdatedAt { get; set; }
        private DateTime DueDate { get; set; }
        private int UserId { get; set; }
        private string Priority { get; set; }
        private int TimeEstimate { get; set; }
        private string Category { get; set; }
        private bool IsCompleted { get; set; }

        public Todo(int id, string title, string description, DateTime createdAt, DateTime updatedAt, DateTime dueDate, int userId, string priority, int timeEstimate, string category, bool isCompleted) {
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

        public int GetId() {
            return Id;
        }

        public string GetTitle() {
            return Title;
        }

        public string GetDescription() {
            return Description;
        }

        public DateTime GetCreatedAt() {
            return CreatedAt;
        }

        public DateTime GetUpdatedAt() {
            return UpdatedAt;
        }

        public DateTime GetDueDate() {
            return DueDate;
        }

        public int GetUserId() {
            return UserId;
        }

        public string GetPriority() {
            return Priority;
        }

        public int GetTimeEstimate() {
            return TimeEstimate;
        }

        public string GetCategory() {
            return Category;
        }

        public bool GetIsCompleted() {
            return IsCompleted;
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

        public void SetPriority(string priority) {
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