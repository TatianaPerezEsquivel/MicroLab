using MicroLab.BussinessEntities;
using MicroLab.DataAccessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.BussinessLogic
{
    public class PatientBL
    {
        public async Task<int> CreateAsync(Patient patient)
        {
            return await PatientDAL.CreateAsync(patient);
        }

        public async Task<int> UpdateAsync(Patient patient)
        {
            return await PatientDAL.UpdateAsync(patient);
        }

        public async Task<int> DeleteAsync(Patient patient)
        {
            return await PatientDAL.DeleteAsync(patient);
        }

        public async Task<Patient> GetByIdAsync(Patient patient)
        {
            return await PatientDAL.GetByIdAsync(patient);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await PatientDAL.GetAllAsync();
        }

        public async Task<List<Patient>> SearchAsync(Patient patient)
        {
            return await PatientDAL.SearchAsync(patient);
        }
    }
}
