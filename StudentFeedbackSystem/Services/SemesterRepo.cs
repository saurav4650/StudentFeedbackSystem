using AutoMapper;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface ISemester
    {
        int Save(SemestersVM obj);
        bool Delete(int id);
        SemestersVM GetById(int id);
        List<SemestersVM> GetAll();
    }

    public class SemesterRepo : ISemester
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public SemesterRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<Semester, SemestersVM>();
                m.CreateMap<SemestersVM, Semester>();
            });
            map = new Mapper(cfg);
        }

        public int Save(SemestersVM obj)
        {
            try
            {
                if (obj.SemesterId == 0)
                {
                    var entity = map.Map<Semester>(obj);
                    ctx.Semesters.Add(entity);
                    ctx.SaveChanges();
                    return entity.SemesterId;
                }
                else
                {
                    var existing = ctx.Semesters.Find(obj.SemesterId);
                    if (existing == null) return -1;
                    existing.SemesterName = obj.SemesterName;
                    existing.StartDate = obj.StartDate;
                    existing.EndDate = obj.EndDate;
                    ctx.SaveChanges();
                    return existing.SemesterId;
                }
            }
            catch { return -1; }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.Semesters.Find(id);
                if (entity == null) return false;
                ctx.Semesters.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public SemestersVM GetById(int id)
        {
            var entity = ctx.Semesters.Find(id);
            SemestersVM result;

            if (entity == null)
            {
                result = new SemestersVM();
            }
            else
            {
                result = map.Map<SemestersVM>(entity);
            }

            return result;

        }

        public List<SemestersVM> GetAll()
        {
            var list = ctx.Semesters.ToList();
            return map.Map<List<SemestersVM>>(list);
        }
    }
}
