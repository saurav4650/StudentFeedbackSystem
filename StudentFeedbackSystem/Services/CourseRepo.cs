using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface ICourse
    {
        int Save(CoursesVM obj);
        bool Delete(int id);
        CoursesVM GetById(int id);
        List<CoursesVM> GetAll();
    }

    public class CourseRepo : ICourse
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public CourseRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<Course, CoursesVM>()
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null));
                m.CreateMap<CoursesVM, Course>();
            });
            map = new Mapper(cfg);
        }

        public int Save(CoursesVM obj)
        {
            try
            {
                if (obj.CourseId == 0)
                {
                    var entity = map.Map<Course>(obj);
                    ctx.Courses.Add(entity);
                    ctx.SaveChanges();
                    return entity.CourseId;
                }
                else
                {
                    var existing = ctx.Courses.Find(obj.CourseId);
                    if (existing == null) return -1;
                    existing.DepartmentId = obj.DepartmentId;
                    existing.CourseCode = obj.CourseCode;
                    existing.CourseName = obj.CourseName;
                    existing.Credits = obj.Credits;
                    ctx.SaveChanges();
                    return existing.CourseId;
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
                var entity = ctx.Courses.Find(id);
                if (entity == null) 
                    return false;
                ctx.Courses.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        public CoursesVM GetById(int id)
        {
            var entity = ctx.Courses
                .Include(c => c.Department)
                .FirstOrDefault(c => c.CourseId == id);

            CoursesVM result;

            if (entity == null)
            {
                result = new CoursesVM();
            }
            else
            {
                result = map.Map<CoursesVM>(entity);
            }

            return result;

        }

        public List<CoursesVM> GetAll()
        {
            var list = ctx.Courses.Include(c => c.Department).ToList();
            return map.Map<List<CoursesVM>>(list);
        }
    }
}
