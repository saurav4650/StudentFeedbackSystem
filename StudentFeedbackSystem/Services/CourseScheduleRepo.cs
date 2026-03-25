using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface ICourseSchedules
    {
        int Save(ScheduleVM obj);
        bool Delete(int id);
        ScheduleVM GetById(int id);
        List<ScheduleVM> GetAll();
        List<ScheduleVM> GetByInstructor(int userId);
    }

    public class CourseScheduleRepo : ICourseSchedules
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public CourseScheduleRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<CourseSchedule, ScheduleVM>()
                    .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course != null ? src.Course.CourseName : null))
                    .ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.Username : null))
                    .ForMember(dest => dest.Semester, opt => opt.MapFrom(src => src.Semester != null ? src.Semester.SemesterName : null));
                m.CreateMap<ScheduleVM, CourseSchedule>();
            });
            map = new Mapper(cfg);
        }

        public int Save(ScheduleVM obj)
        {
            try
            {
                if (obj.ScheduleId == 0)
                {
                    var entity = map.Map<CourseSchedule>(obj);
                    ctx.CourseSchedules.Add(entity);
                    ctx.SaveChanges();
                    return entity.ScheduleId;
                }
                else
                {
                    var existing = ctx.CourseSchedules.Find(obj.ScheduleId);
                    if (existing == null) return -1;
                    existing.CourseId = obj.CourseId;
                    existing.InstructorUserId = obj.InstructorUserId;
                    existing.SemesterId = obj.SemesterId;
                    existing.SectionNumber = obj.SectionNumber;
                    existing.MaxStudents = obj.MaxStudents;
                    ctx.SaveChanges();
                    return existing.ScheduleId;
                }
            }
            catch { return -1; }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.CourseSchedules.Find(id);
                if (entity == null) return false;
                ctx.CourseSchedules.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public ScheduleVM GetById(int id)
        {
            var entity = ctx.CourseSchedules
                .Include(cs => cs.Course)
                .Include(cs => cs.Instructor)
                .Include(cs => cs.Semester)
                .FirstOrDefault(cs => cs.ScheduleId == id);
            return entity == null ? new ScheduleVM() : map.Map<ScheduleVM>(entity);
        }

        public List<ScheduleVM> GetAll()
        {
            var list = ctx.CourseSchedules
                .Include(cs => cs.Course)
                .Include(cs => cs.Instructor)
                .Include(cs => cs.Semester)
                .ToList();
            return map.Map<List<ScheduleVM>>(list);
        }

        public List<ScheduleVM> GetByInstructor(int userId)
        {
            var list = ctx.CourseSchedules
                .Include(cs => cs.Course)
                .Include(cs => cs.Instructor)
                .Include(cs => cs.Semester)
                .Where(cs => cs.InstructorUserId == userId)
                .ToList();
            return map.Map<List<ScheduleVM>>(list);
        }
    }
}
