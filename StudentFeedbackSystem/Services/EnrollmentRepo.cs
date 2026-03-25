using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IEnrollment
    {
        int Save(EnrollmentVM obj);
        bool Delete(int id);
        EnrollmentVM GetById(int id);
        List<EnrollmentVM> GetAll();
        List<EnrollmentVM> GetByStudent(int userId);
        List<EnrollmentVM> GetBySchedule(int scheduleId);
    }

    public class EnrollmentRepo : IEnrollment
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public EnrollmentRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<Enrollment, EnrollmentVM>()
                    .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student != null ? src.Student.Username : null))
                    .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Schedule != null && src.Schedule.Course != null ? src.Schedule.Course.CourseName : null))
                    .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Schedule != null && src.Schedule.Instructor != null ? src.Schedule.Instructor.Username : null))
                    .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Schedule != null && src.Schedule.Semester != null ? src.Schedule.Semester.SemesterName : null))
                    .ForMember(dest => dest.SectionNumber, opt => opt.MapFrom(src => src.Schedule != null ? src.Schedule.SectionNumber : null));
                m.CreateMap<EnrollmentVM, Enrollment>();
            });
            map = new Mapper(cfg);
        }

        public int Save(EnrollmentVM obj)
        {
            try
            {
                if (obj.EnrollmentId == 0)
                {
                    var entity = map.Map<Enrollment>(obj);
                    ctx.Enrollments.Add(entity);
                    ctx.SaveChanges();
                    return entity.EnrollmentId;
                }
                else
                {
                    var existing = ctx.Enrollments.Find(obj.EnrollmentId);
                    if (existing == null) return -1;
                    existing.StudentUserId = obj.StudentUserId;
                    existing.ScheduleId = obj.ScheduleId;
                    existing.Grade = obj.Grade;
                    ctx.SaveChanges();
                    return existing.EnrollmentId;
                }
            }
            catch 
            {  
                return -1; 
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.Enrollments.Find(id);
                if (entity == null) return false;
                ctx.Enrollments.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        public EnrollmentVM GetById(int id)
        {
            var entity = ctx.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Schedule).ThenInclude(s => s!.Course)
                .Include(e => e.Schedule).ThenInclude(s => s!.Instructor)
                .Include(e => e.Schedule).ThenInclude(s => s!.Semester)
                .FirstOrDefault(e => e.EnrollmentId == id);

            EnrollmentVM result;

            if (entity == null)
            {
                result = new EnrollmentVM();
            }
            else
            {
                result = map.Map<EnrollmentVM>(entity);
            }

            return result;

        }

        public List<EnrollmentVM> GetAll()
        {
            var list = ctx.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Schedule).ThenInclude(s => s!.Course)
                .Include(e => e.Schedule).ThenInclude(s => s!.Instructor)
                .Include(e => e.Schedule).ThenInclude(s => s!.Semester)
                .ToList();
            return map.Map<List<EnrollmentVM>>(list);
        }

        public List<EnrollmentVM> GetByStudent(int userId)
        {
            var list = ctx.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Schedule).ThenInclude(s => s!.Course)
                .Include(e => e.Schedule).ThenInclude(s => s!.Instructor)
                .Include(e => e.Schedule).ThenInclude(s => s!.Semester)
                .Where(e => e.StudentUserId == userId)
                .ToList();

            return map.Map<List<EnrollmentVM>>(list);
        }

        public List<EnrollmentVM> GetBySchedule(int scheduleId)
        {
            var list = ctx.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Schedule).ThenInclude(s => s!.Course)
                .Include(e => e.Schedule).ThenInclude(s => s!.Instructor)
                .Include(e => e.Schedule).ThenInclude(s => s!.Semester)
                .Where(e => e.ScheduleId == scheduleId)
                .ToList();

            return map.Map<List<EnrollmentVM>>(list);
        }
    }
}
