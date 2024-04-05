using MicroLab.BussinessEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroLab.DataAccessLogic
{
    public class PatientDAL
    {


        public static async Task<int> CreateAsync(Patient patient)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Add(patient);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Patient patient)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var patientDB = await dbContext.Patient.FirstOrDefaultAsync(c => c.Id == patient.Id);
                if (patientDB != null)
                {
                    patientDB.Name = patient.Name;
                    patientDB.lastname = patient.lastname;
                    patientDB.birthDate = patient.birthDate;
                    patientDB.Age = patient.Age;
                    patientDB.CellPhone = patient.CellPhone;
                    patientDB.address = patient.address;
                    patientDB.gender = patient.gender;
                    //aquí faltan las otras propiedades
                    // no ha puesto las demás propiedades

                    dbContext.Update(patientDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Patient patient)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var patientDB = await dbContext.Patient.FirstOrDefaultAsync(c => c.Id == patient.Id);
                if (patientDB != null)
                {
                    dbContext.Patient.Remove(patientDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<Patient> GetByIdAsync(Patient patient)
        {
            var patientDB = new Patient();
            using (var dbContext = new ContextDB())
            {
                patientDB = await dbContext.Patient.FirstOrDefaultAsync(c => c.Id == patient.Id);
            }
            return patientDB;
        }
        public static async Task<List<Patient>> GetAllAsync()
        {
            var patients = new List<Patient>();
            using (var dbContext = new ContextDB())
            {
                patients = await dbContext.Patient.ToListAsync();
            }
            return patients;
        }
        internal static IQueryable<Patient> QuerySelect(IQueryable<Patient> query, Patient patient)
        {
            if (patient.Id > 0)
                query = query.Where(c => c.Id == patient.Id);
            if (!string.IsNullOrWhiteSpace(patient.Name))
                query = query.Where(c => c.Name.Contains(patient.Name));
            if (!string.IsNullOrWhiteSpace(patient.lastname))
                query = query.Where(c => c.lastname.Contains(patient.lastname));
            if (!string.IsNullOrWhiteSpace(patient.CellPhone))
                query = query.Where(c => c.CellPhone.Contains(patient.CellPhone));
            if (!string.IsNullOrWhiteSpace(patient.address))
                query = query.Where(c => c.address.Contains(patient.address));
            if (!string.IsNullOrWhiteSpace(patient.gender))
                query = query.Where(c => c.gender.Contains(patient.gender));
            if (!string.IsNullOrWhiteSpace(patient.Age))
                query = query.Where(c => c.Age.Contains(patient.Age));


            // aquí también faltan las otras propiedades

            query = query.Where(c => c.Name.Contains(patient.Name));
            query = query.OrderByDescending(c => c.Id).AsQueryable();
            if (patient.Top_Aux > 0)
                query = query.Take(patient.Top_Aux).AsQueryable();
            return query;
        }
        public static async Task<List<Patient>> SearchAsync(Patient patient)
        {
            var patients = new List<Patient>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Patient.AsQueryable();
                select = QuerySelect(select, patient);
                patients = await select.ToListAsync();
                {
                    return patients;
                }
            }
        }
        public static async Task<int> AddExamAsync(int patientId, Exam exam)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                //var patientDB = await dbContext.Patient.Include(p => p.Exams).FirstOrDefaultAsync(p => p.Id == patientId);
                //if (patientDB != null)
                //{
                //    patientDB.Exams.Add(exam);
                //    result = await dbContext.SaveChangesAsync();
                //}
            }
            return result;
        }
        public static async Task<int> UpdateExamAsync(Exam exam)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Update(exam);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> DeleteExamAsync(int examId)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var exam = await dbContext.Exam.FirstOrDefaultAsync(e => e.Id == examId);
                if (exam != null)
                {
                    dbContext.Exam.Remove(exam);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }

    }
}
