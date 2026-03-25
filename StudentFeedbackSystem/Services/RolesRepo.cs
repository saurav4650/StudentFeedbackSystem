using AutoMapper;
using Microsoft.Extensions.Logging;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IRoles
    {
        int Save(RolesVM obj);
        bool Delete(int id);
        RolesVM GetById(int id);
        List<RolesVM> GetAll();
    }

    public class RolesRepo : IRoles
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public RolesRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<Role, RolesVM>();
                m.CreateMap<RolesVM, Role>();
            });
            map = new Mapper(cfg);
        }

        public int Save(RolesVM obj)
        {
            try
            {
                if (obj.RoleId == 0)
                {
                    var entity = map.Map<Role>(obj);
                    ctx.Roles.Add(entity);
                    ctx.SaveChanges();
                    return entity.RoleId;
                }
                else
                {
                    var existing = ctx.Roles.Find(obj.RoleId);
                    if (existing == null) return -1;
                    existing.RoleName = obj.RoleName;
                    ctx.SaveChanges();
                    return existing.RoleId;
                }
            }
            catch { return -1; }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.Roles.Find(id);
                if (entity == null) return false;
                ctx.Roles.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public RolesVM GetById(int id)
        {
            var entity = ctx.Roles.Find(id);
            RolesVM result;

            if (entity == null)
            {
                result = new RolesVM();
            }
            else
            {
                result = map.Map<RolesVM>(entity);
            }

            return result;

        }

        public List<RolesVM> GetAll()
        {
            var list = ctx.Roles.ToList();
            return map.Map<List<RolesVM>>(list);
        }
    }
}
