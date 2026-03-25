using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IUser
    {
        int Save(UsersVM obj);
        bool Delete(int id);
        UsersVM GetById(int id);
        List<UsersVM> GetAll();
        List<UsersVM> GetByRole(string roleName);
        UsersVM? GetByUsername(string username);
    }

    public class UserRepo : IUser
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public UserRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<User, UsersVM>()
                    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.RoleName : null))
                    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.DepartmentName : null));
                m.CreateMap<UsersVM, User>();
            });
            map = new Mapper(cfg);
        }

        public int Save(UsersVM obj)
        {
            try
            {
                if (obj.UserId == 0)
                {
                    var entity = map.Map<User>(obj);
                    ctx.Users.Add(entity);
                    ctx.SaveChanges();
                    return entity.UserId;
                }
                else
                {
                    var existing = ctx.Users.Find(obj.UserId);
                    if (existing == null) return -1;
                    existing.RoleId = obj.RoleId;
                    existing.DepartmentId = obj.DepartmentId;
                    existing.Username = obj.Username;
                    existing.Email = obj.Email;
                    existing.UserNumber = obj.UserNumber;
                    if (!string.IsNullOrEmpty(obj.PasswordHash))
                        existing.PasswordHash = obj.PasswordHash;
                    ctx.SaveChanges();
                    return existing.UserId;
                }
            }
            catch { return -1; }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.Users.Find(id);
                if (entity == null) return false;
                ctx.Users.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public UsersVM GetById(int id)
        {
            var entity = ctx.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefault(u => u.UserId == id);
            UsersVM result;

            if (entity == null)
            {
                result = new UsersVM();
            }
            else
            {
                result = map.Map<UsersVM>(entity);
            }

            return result;

        }

        public List<UsersVM> GetAll()
        {
            var list = ctx.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .ToList();
            return map.Map<List<UsersVM>>(list);
        }

        public List<UsersVM> GetByRole(string roleName)
        {
            var list = ctx.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Where(u => u.Role != null && u.Role.RoleName == roleName)
                .ToList();
            return map.Map<List<UsersVM>>(list);
        }

        public UsersVM? GetByUsername(string username)
        {
            var entity = ctx.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefault(u => u.Username == username); 
            
            UsersVM result;

            if (entity == null)
            {
                result = null;
            }
            else
            {
                result = map.Map<UsersVM>(entity);
            }

            return result;

        }
    }
}
