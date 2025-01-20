namespace TaskFlow.Models
{
    public class Task{
        public int Id {get; set;}
        public required string Title {get; set;}
        public required string Description {get; set;}
        public DateTime DueDate {get; set;}
        public int Status {get; set;} //  0 for Pending, 1 for InProgress, 2 for Completed
    }
}