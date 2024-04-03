using MicroLab.BussinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.DataAccessLogic
{
    public class ExamDAL
    {

        public static async Task<List<Exam>> GetExamsInRange(DateTime startDate, DateTime endDate)
        {

            using (var dbContext = new ContextDB())
            {
                // Aplica el filtro directamente en la consulta de la base de datos
                var examsInRange = await dbContext.Exam
                    .Where(e => e.Date >= startDate && e.Date <= endDate)
                    .ToListAsync();

                return examsInRange;
            }
        }
        public static async Task<int> CreateAsync(Exam exam)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(exam);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Exam exam)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var examDB = await dbContext.Exam.FirstOrDefaultAsync(c => c.Id == exam.Id);
                if (examDB != null)
                {
                    examDB.Name = exam.Name;
                    examDB.Price = exam.Price;

                    dbContext.Update(examDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Exam exam)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var examDB = await dbContext.Exam.FirstOrDefaultAsync(c => c.Id == exam.Id);
                if (examDB != null)
                {
                    dbContext.Exam.Remove(examDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<Exam> GetByIdAsync(Exam exam)
        {
            var examDB = new Exam();
            using (var dbContext = new ContextDB())
            {
                examDB = await dbContext.Exam.FirstOrDefaultAsync(c => c.Id == exam.Id);
            }
            return examDB;
        }
        public static async Task<List<Exam>> GetAllAsync()
        {
            var exames = new List<Exam>();
            using (var dbContext = new ContextDB())
            {
                exames = await dbContext.Exam.ToListAsync();
            }
            return exames;
        }
        internal static IQueryable<Exam> QuerySelect(IQueryable<Exam> query, Exam exam)
        {
            if (exam.Id > 0)
                query = query.Where(c => c.Id == exam.Id);
            if (!string.IsNullOrWhiteSpace(exam.Name))
                query = query.Where(c => c.Name.Contains(exam.Name));
            query = query.OrderByDescending(c => c.Id).AsQueryable();
            //if (!decimal.MinValue(exam.Price))
            //    query = query.Where(c => c.Price.Contains(exam.Price));
            //query = query.OrderByDescending(c => c.Id).AsQueryable();


            if (exam.Top_Aux > 0)

                if (exam.Date.Year > 1000)
                {
                    DateTime inicialDate = new DateTime(exam.Date.Year, exam.Date.Month, exam.Date.Day, 0, 0, 0);
                    DateTime finalDate = inicialDate.AddDays(1).AddMilliseconds(-1);
                    query = query.Where(u => u.Date >= inicialDate && u.Date <= finalDate);
                }

            query = query.OrderByDescending(u => u.Id).AsQueryable();

            if (exam.Top_Aux > 0)
                query = query.Take(exam.Top_Aux).AsQueryable();

            return query;
        }
        public static async Task<List<Exam>> SearchAsync(Exam exam)
        {
            var exams = new List<Exam>();
            using (var dbContext = new ContextDB())
            {
                var query = dbContext.Exam.AsQueryable();
                query = QuerySelect(query, exam);
                exams = await query.ToListAsync(); // Realizar la consulta y asignar los resultados a 'exams'
            }
            return exams; // Retornar los resultados de la consulta
        }

    }
}
