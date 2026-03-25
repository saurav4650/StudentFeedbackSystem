namespace StudentFeedbackSystem.Models
{
    public class FeedbackSubmissionVM
    {
        public int StudentUserId { get; set; }
        public int ScheduleId { get; set; }
        public int OverallRating { get; set; }
        public string? CourseName { get; set; }
        public string? InstructorName { get; set; }
        public string? SemesterName { get; set; }
        public string? SectionNumber { get; set; }
     
    }
}
