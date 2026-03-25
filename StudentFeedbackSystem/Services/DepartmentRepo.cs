using AutoMapper;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IDepartment
    {
        int Save(DepartmentsVM obj);
        bool Delete(int id);
        DepartmentsVM GetById(int id);
        List<DepartmentsVM> GetAll();
    }

    public class DepartmentRepo : IDepartment
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public DepartmentRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<Department, DepartmentsVM>();
                m.CreateMap<DepartmentsVM, Department>();
            });
            map = new Mapper(cfg);
        }

        public int Save(DepartmentsVM obj)
        {
            try
            {
                if (obj.DepartmentId == 0)
                {
                    var entity = map.Map<Department>(obj);
                    ctx.Departments.Add(entity);
                    ctx.SaveChanges();
                    return entity.DepartmentId;
                }
                else
                {
                    var existing = ctx.Departments.Find(obj.DepartmentId);
                    if (existing == null) return -1;
                    existing.DepartmentCode = obj.DepartmentCode;
                    existing.DepartmentName = obj.DepartmentName;
                    ctx.SaveChanges();
                    return existing.DepartmentId;
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
                var entity = ctx.Departments.Find(id);
                if (entity == null) return false;
                ctx.Departments.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch 
            {
                 return false; 
            }
        }

        public DepartmentsVM GetById(int id)
        {
            var entity = ctx.Departments.Find(id);
            DepartmentsVM result;

            if (entity == null)
            {
                result = new DepartmentsVM();
            }
            else
            {
                result = map.Map<DepartmentsVM>(entity);
            }

            return result;

        }

        public List<DepartmentsVM> GetAll()
        {
            var list = ctx.Departments.ToList();
            return map.Map<List<DepartmentsVM>>(list);
        }
    }
}
