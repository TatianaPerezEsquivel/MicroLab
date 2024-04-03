using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessLogic
{
    public class ExamBL
    {
        public async Task<int> CreateAsync(Exam exam)
        {
            return await ExamDAL.CreateAsync(exam);
        }

        public async Task<int> UpdateAsync(Exam exam)
        {
            return await ExamDAL.UpdateAsync(exam);
        }

        public async Task<int> DeleteAsync(Exam exam)
        {
            return await ExamDAL.DeleteAsync(exam);
        }

        public async Task<Exam> GetByIdAsync(Exam exam)
        {
            return await ExamDAL.GetByIdAsync(exam);
        }

        public async Task<List<Exam>> GetAllAsync()
        {
            return await ExamDAL.GetAllAsync();
        }

        public async Task<List<Exam>> SearchAsync(Exam exam)
        {
            return await ExamDAL.SearchAsync(exam);
        }
        public async Task<List<Exam>> GetExamsInRange(DateTime startDate, DateTime endDate)
        {
            return await ExamDAL.GetExamsInRange(startDate, endDate);
        }


    }
}
