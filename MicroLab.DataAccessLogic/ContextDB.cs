using MicroLab.BussinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MicroLab.DataAccessLogic;

namespace MicroLab.DataAccessLogic
{
    public class ContextDB : DbContext
    {


        
         public DbSet<Exam> Exam{ get; set; }
         public DbSet<ExamType> ExamType { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
  
        public DbSet<Patient> Patient { get; set; }
  
        public DbSet<Waterfall> Waterfall { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(@"Data source = localhost; Initial catalog = Laboratorio; Integrated Security = true; Encrypt = false; TrustServerCertificate = true");
            }
        }
    }

