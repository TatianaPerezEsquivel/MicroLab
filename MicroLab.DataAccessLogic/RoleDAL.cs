using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroLab.BussinessEntities;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace MicroLab.DataAccessLogic
{
    public class RoleDAL
    {
        public static async Task<int> CreateAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                dbContext.Role.Add(role);
                result = await dbContext.SaveChangesAsync();
            }
            return result;
        }
        public static async Task<int> UpdateAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDB = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == role.Id);
                if (roleDB != null)
                {
                    roleDB.Name = role.Name;
                    dbContext.Role.Update(roleDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<int> DeleteAsync(Role role)
        {
            int result = 0;
            using (var dbContext = new ContextDB())
            {
                var roleDB = await dbContext.Role.FirstOrDefaultAsync(c => c.Id == role.Id);
                if (roleDB != null)
                {
                    dbContext.Role.Remove(roleDB);
                    result = await dbContext.SaveChangesAsync();
                }
            }
            return result;
        }
        public static async Task<Role> GetByIdAsync(Role role)
        {
            var roleDB = new Role();
            using (var dbContext = new ContextDB())
            {
                roleDB = await dbContext.Role.FirstOrDefaultAsync(r => r.Id == role.Id);
            }
            return roleDB;
        }
        public static async Task<List<Role>> GetAllAsync()
        {
            var roles = new List<Role>();
            using (var dbContext = new ContextDB())
            {
                roles = await dbContext.Role.ToListAsync();
            }
            return roles;
        }
        internal static IQueryable<Role> QuerySelect(IQueryable<Role> query, Role role)
        {
            if (role.Id > 0)
                query = query.Where(r => r.Id == role.Id);
            if (!string.IsNullOrWhiteSpace(role.Name))
                query = query.Where(r => r.Name.Contains(role.Name));
            query = query.OrderByDescending(r => r.Id).AsQueryable();
            if (role.Top_Aux > 0)
                query = query.Take(role.Top_Aux).AsQueryable();
            return query;
        }
        public static async Task<List<Role>> SearchAsync(Role role)
        {
            var roles = new List<Role>();
            using (var dbContext = new ContextDB())
            {
                var select = dbContext.Role.AsQueryable();
                select = QuerySelect(select, role);
                roles = await select.ToListAsync();
                {
                    return roles;
                }
            }
        }
    }
}

