using AutoMapper;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IQuestionCat
    {
        int Save(QuestionCategoryVM obj);
        bool Delete(int id);
        QuestionCategoryVM GetById(int id);
        List<QuestionCategoryVM> GetAll();
    }

    public class QuestionCategoriesRepo : IQuestionCat
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public QuestionCategoriesRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<QuestionCategory, QuestionCategoryVM>();
                m.CreateMap<QuestionCategoryVM, QuestionCategory>();
            });
            map = new Mapper(cfg);
        }

        public int Save(QuestionCategoryVM obj)
        {
            try
            {
                if (obj.CategoryId == 0)
                {
                    var entity = map.Map<QuestionCategory>(obj);
                    ctx.QuestionCategories.Add(entity);
                    ctx.SaveChanges();
                    return entity.CategoryId;
                }
                else
                {
                    var existing = ctx.QuestionCategories.Find(obj.CategoryId);
                    if (existing == null) return -1;
                    existing.CategoryName = obj.CategoryName;
                    existing.DisplayOrder = obj.DisplayOrder;
                    ctx.SaveChanges();
                    return existing.CategoryId;
                }
            }
            catch { return -1; }
        }

        public bool Delete(int id)
        {
            try
            {
                var entity = ctx.QuestionCategories.Find(id);
                if (entity == null) return false;
                ctx.QuestionCategories.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public QuestionCategoryVM GetById(int id)
        {
            var entity = ctx.QuestionCategories.Find(id);
            QuestionCategoryVM result;

            if (entity == null)
            {
                result = new QuestionCategoryVM();
            }
            else
            {
                result = map.Map<QuestionCategoryVM>(entity);
            }

            return result;

        }

        public List<QuestionCategoryVM> GetAll()
        {
            var list = ctx.QuestionCategories.OrderBy(x => x.DisplayOrder).ToList();
            return map.Map<List<QuestionCategoryVM>>(list);
        }
    }
}
