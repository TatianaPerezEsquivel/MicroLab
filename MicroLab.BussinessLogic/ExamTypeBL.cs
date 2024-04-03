using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessLogic
{
    public class ExamTypeBL
    {
        public async Task<int> CreateAsync(ExamType ExamType)
        {
            return await ExamTypeDAL.CreateAsync(ExamType);
        }

        public async Task<int> UpdateAsync(ExamType ExamType)
        {
            return await ExamTypeDAL.UpdateAsync(ExamType);
        }

        public async Task<int> DeleteAsync(ExamType ExamType)
        {
            return await ExamTypeDAL.DeleteAsync(ExamType);
        }

        public async Task<ExamType> GetByIdAsync(ExamType ExamType)
        {
            return await ExamTypeDAL.GetByIdAsync(ExamType);
        }

        public async Task<List<ExamType>> GetAllAsync()
        {
            return await ExamTypeDAL.GetAllAsync();
        }

        public async Task<List<ExamType>> SearchAsync(ExamType ExamType)
        {
            return await ExamTypeDAL.SearchAsync(ExamType);
        }
    }
}
