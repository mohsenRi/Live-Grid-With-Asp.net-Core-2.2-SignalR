using System.Collections.Generic;
using System.Threading.Tasks;
using SignalRLiveGrid.Data;

namespace SignalRLiveGrid.Service
{
    public interface IPersonService
    {
        Task<Person> AddPersonAsync(Person person);
        Task<Person> EditPersonAsync(Person person);
        Task<Person> GetPersonByIdAsync(int personId);
        Task<List<Person>> GetPeopleAsync();
        void DeletePerson(Person person);
    }
}