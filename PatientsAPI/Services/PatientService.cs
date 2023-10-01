using PatientsAPI.Database.Models;
using PatientsAPI.Database;
using Microsoft.EntityFrameworkCore;

namespace PatientsAPI.Services
{
    public class PatientService
    {
        private readonly DataContext _context;

        public PatientService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync(string sortBy, bool orderAsc, int page, int pageSize = 10)
        {
            var query = _context.Patients
                .Include(p => p.Area)
                .AsQueryable();

            // сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "lastname":
                        query = orderAsc ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
                        break;
                    case "firstname":
                        query = orderAsc ? query.OrderBy(p => p.FirstName) : query.OrderByDescending(p => p.FirstName);
                        break;
                    case "middlename":
                        query = orderAsc ? query.OrderBy(p => p.MiddleName) : query.OrderByDescending(p => p.MiddleName);
                        break;
                    case "address":
                        query = orderAsc ? query.OrderBy(p => p.Address) : query.OrderByDescending(p => p.Address);
                        break;
                    case "birthdate":
                        query = orderAsc ? query.OrderBy(p => p.BirthDate) : query.OrderByDescending(p => p.BirthDate);
                        break;
                    case "gender":
                        query = orderAsc ? query.OrderBy(p => p.Gender) : query.OrderByDescending(p => p.Gender);
                        break;
                    case "areanumber":
                        query = orderAsc ? query.OrderBy(p => p.Area.Number) : query.OrderByDescending(p => p.Area.Number);
                        break;
                    default:
                        query = orderAsc ? query.OrderBy(p => p.LastName) : query.OrderByDescending(p => p.LastName);
                        break;
                }
            }

            // постраничный вывод
            var result = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

        public async Task<Patient> GetPatientAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task AddAsync(Patient entity)
        {
            if (entity != null)
            {
                await _context.Patients.AddAsync(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Patient entity)
        {
            if (entity != null)
            {
                _context.Patients.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.Patients.FindAsync(id);
            if (entity != null)
            {
                _context.Patients.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
