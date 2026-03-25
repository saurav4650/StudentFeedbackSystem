using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentFeedbackSystem.EntityModels;
using StudentFeedbackSystem.Models;

namespace StudentFeedbackSystem.Services
{
    public interface IFeedbackQuestion
    {
        int Save(FeedbackQuestionVM obj);
        bool Delete(int id);
        FeedbackQuestionVM GetById(int id);
        List<FeedbackQuestionVM> GetAll();
    }

    public class FeedbackQuestionRepo : IFeedbackQuestion
    {
        Mapper map;
        StudentFeedbackDbContext ctx;

        public FeedbackQuestionRepo(StudentFeedbackDbContext _ctx)
        {
            this.ctx = _ctx;
            var cfg = new MapperConfiguration(m =>
            {
                m.CreateMap<FeedbackQuestion, FeedbackQuestionVM>()
                    .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : null));
                m.CreateMap<FeedbackQuestionVM, FeedbackQuestion>();
            });
            map = new Mapper(cfg);
        }

        public int Save(FeedbackQuestionVM obj)
        {
            try
            {
                if (obj.QuestionId == 0)
                {
                    var entity = map.Map<FeedbackQuestion>(obj);
                    ctx.FeedbackQuestions.Add(entity);
                    ctx.SaveChanges();
                    return entity.QuestionId;
                }
                else
                {
                    var existing = ctx.FeedbackQuestions.Find(obj.QuestionId);
                    if (existing == null) return -1;
                    existing.CategoryId = obj.CategoryId;
                    existing.QuestionText = obj.QuestionText;
                    existing.QuestionType = obj.QuestionType;
                    existing.DisplayOrder = obj.DisplayOrder;
                    ctx.SaveChanges();
                    return existing.QuestionId;
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
                var entity = ctx.FeedbackQuestions.Find(id);
                if (entity == null) return false;
                ctx.FeedbackQuestions.Remove(entity);
                ctx.SaveChanges();
                return true;
            }
            catch 
            { 
                return false; 
            }
        }

        public FeedbackQuestionVM GetById(int id)
        {
            var entity = ctx.FeedbackQuestions
                .Include(q => q.Category)
                .FirstOrDefault(q => q.QuestionId == id);
            FeedbackQuestionVM result;

            if (entity == null)
            {
                result = new FeedbackQuestionVM();
            }
            else
            {
                result = map.Map<FeedbackQuestionVM>(entity);
            }

            return result;

        }

        public List<FeedbackQuestionVM> GetAll()
        {
            var list = ctx.FeedbackQuestions
                .Include(q => q.Category)
                .OrderBy(q => q.Category!.DisplayOrder)
                .ThenBy(q => q.DisplayOrder)
                .ToList();

            return map.Map<List<FeedbackQuestionVM>>(list);
        }
    }
}
