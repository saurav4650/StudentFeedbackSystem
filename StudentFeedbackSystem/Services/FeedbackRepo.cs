using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IFeedback
    {
        int SubmitFeedback(FeedbackSubmissionVM obj);
        FeedbackSubmissionVM GetSubmissionForm(int scheduleId);
        bool HasSubmitted(int studentUserId, int scheduleId);
    }

    public class FeedbackRepo : IFeedback
    {
        StudentFeedbackDbContext ctx;

        public FeedbackRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
        }

        public bool HasSubmitted(int studentUserId, int scheduleId)
        {
            return ctx.FeedbackForms.Any(f => f.StudentUserId == studentUserId && f.ScheduleId == scheduleId);
        }

        public FeedbackSubmissionVM GetSubmissionForm(int scheduleId)
        {
            var schedule = ctx.CourseSchedules
                .Include(s => s.Course)
                .Include(s => s.Instructor)
                .Include(s => s.Semester)
                .FirstOrDefault(s => s.ScheduleId == scheduleId);

            if (schedule == null) return new FeedbackSubmissionVM();

            return new FeedbackSubmissionVM
            {
                ScheduleId = scheduleId,
                CourseName = schedule.Course?.CourseName,
                InstructorName = schedule.Instructor?.Username,
                SemesterName = schedule.Semester?.SemesterName,
                SectionNumber = schedule.SectionNumber
            };
        }

        public int SubmitFeedback(FeedbackSubmissionVM obj)
        {
            try
            {
                if (HasSubmitted(obj.StudentUserId, obj.ScheduleId))
                    return -1;

                var form = new FeedbackForm
                {
                    StudentUserId = obj.StudentUserId,
                    ScheduleId = obj.ScheduleId,
                    SubmissionDate = DateTime.Now,
                    OverallRating = obj.OverallRating
                };

                ctx.FeedbackForms.Add(form);
                ctx.SaveChanges();
                return form.FeedbackFormId;
            }
            catch
            {
                return -1;
            }
        }
    }
}
