using SignalRLiveGrid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SignalRLiveGrid.Service
{
    public class PersonService : IPersonService, IDisposable
    {
        private readonly AppDbContext _db;

        public PersonService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Person> AddPersonAsync(Person person)
        {
            await _db.AddAsync(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<Person> EditPersonAsync(Person person)
        {
            _db.People.Update(person);
            await _db.SaveChangesAsync();
            return person;
        }

        public async Task<List<Person>> GetPeopleAsync()
            => await _db.People.Where(q=>!q.IsLock).AsNoTracking().ToListAsync();


        public async Task<Person> GetPersonByIdAsync(int personId)
                     => await _db.People.AsNoTracking().SingleOrDefaultAsync(q => q.PersonId == personId);


        public void DeletePerson(Person person)
        {
            _db.People.Remove(person);
            _db.SaveChanges();
        }
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}