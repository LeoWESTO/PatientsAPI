using Microsoft.EntityFrameworkCore;
using PatientsAPI.Database;
using PatientsAPI.Database.Models;

namespace PatientsAPI.Services
{
    public class DoctorService
    {
        private readonly DataContext _context;

        public DoctorService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Doctor>> GetDoctorsAsync(string sortBy, bool orderAsc, int page, int pageSize = 10)
        {
            var query = _context.Doctors
                .Include(d => d.Cabinet)
                .Include(d => d.Specialization)
                .Include(d => d.Area)
                .AsQueryable();

            // сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "fullname":
                        query = orderAsc ? query.OrderBy(d => d.FullName) : query.OrderByDescending(d => d.FullName);
                        break;
                    default:
                        query = orderAsc ? query.OrderBy(d => d.FullName) : query.OrderByDescending(d => d.FullName);
                        break;
                }
            }

            // постраничный вывод
            var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

        public async Task<Doctor> GetDoctorAsync(Guid id)
        {
            return await _context.Doctors.FindAsync(id);
        }

        public async Task AddAsync(Doctor entity)
        {
            if (entity != null)
            {
                await _context.Doctors.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Doctor entity)
        {
            if (entity != null)
            {
                _context.Doctors.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Doctors.FindAsync(id);
            if (entity != null)
            {
                _context.Doctors.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
