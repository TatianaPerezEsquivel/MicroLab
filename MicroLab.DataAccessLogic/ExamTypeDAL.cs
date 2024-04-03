using MicroLab.BussinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace MicroLab.DataAccessLogic
{
    public class ExamTypeDAL
    {
        public static async Task<int> CreateAsync(ExamType examType)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(examType);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(ExamType examType)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var examTypeDB = await dbContext.ExamType.FirstOrDefaultAsync(c => c.Id == examType.Id);
                if (examTypeDB != null)
                {
                    examTypeDB.Name = examType.Name;
                    
                    dbContext.Update(examTypeDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(ExamType examType)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var examTypeDB = await dbContext.ExamType.FirstOrDefaultAsync(c => c.Id == examType.Id);
                if (examTypeDB != null)
                {
                    dbContext.ExamType.Remove(examTypeDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<ExamType> GetByIdAsync(ExamType examType)
        {
            var examTypeDB = new ExamType();
            using (var dbContext = new ContextDB())
            {
                examTypeDB = await dbContext.ExamType.FirstOrDefaultAsync(c => c.Id == examType.Id);
            }
            return examTypeDB;
        }
        public static async Task<List<ExamType>> GetAllAsync()
        {
            var examTypes = new List<ExamType>();
            using (var dbContext = new ContextDB())
            {
                examTypes = await dbContext.ExamType.ToListAsync();
            }
            return examTypes;
        }
        internal static IQueryable<ExamType> QuerySelect(IQueryable<ExamType> query, ExamType examType)
        {
            if (examType.Id > 0)
                query = query.Where(c => c.Id == examType.Id);
            if (!string.IsNullOrWhiteSpace(examType.Name))
                query = query.Where(c => c.Name.Contains(examType.Name));
            query = query.OrderByDescending(c => c.Id).AsQueryable();
            if (examType.Top_Aux > 0)
                query = query.Take(examType.Top_Aux).AsQueryable();
            return query;
        }
        public static async Task<List<ExamType>> SearchAsync(ExamType examType)
        {
            var examTypes = new List<ExamType>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.ExamType.AsQueryable();
                select = QuerySelect(select, examType);
                examTypes = await select.ToListAsync();
                {
                    return examTypes;
                }
            }
        }
    }
}
