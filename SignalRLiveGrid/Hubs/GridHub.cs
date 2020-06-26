using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using SignalRLiveGrid.Data;
using SignalRLiveGrid.Service;

namespace SignalRLiveGrid.Hubs
{
    public class GridHub : Hub
    {
        private readonly IPersonService _personService;
        //Why i used ConcurrentDictionary? Answer is here -> https://stackoverflow.com/questions/42013226/when-should-i-use-concurrentdictionary-and-dictionary
        private static readonly ConcurrentDictionary<string, List<int>> ControlConnections = new ConcurrentDictionary<string, List<int>>();

        public GridHub(IPersonService personService)
        {
            _personService = personService;
        }

        public override async Task OnConnectedAsync()
        {
            ControlConnections.TryAdd(Context.ConnectionId, new List<int>());
            await base.OnConnectedAsync();
        }


        public async Task LockPerson(int personId)
        {
            try
            {

                var person = await _personService.GetPersonByIdAsync(personId);
                person.IsLock = true;
                await _personService.EditPersonAsync(person);
                ControlConnections[Context.ConnectionId].Add(personId);
                await Clients.Others.SendAsync("lockPerson", person.PersonId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task UnlockPerson(int personId)
        {
            var person = await _personService.GetPersonByIdAsync(personId);
            person.IsLock = false;
            await _personService.EditPersonAsync(person);
            ControlConnections[Context.ConnectionId].Remove(personId);
            await Clients.Others.SendAsync("unlockPerson", person.PersonId, person.Name, person.Salary);
        }

        public async Task UpdatePersonInfo(int personId, string name, decimal salary)
        {
            var editPerson = new Person
            {
                PersonId = personId,
                Name = name,
                Salary = salary,
                IsLock = false
            };
            await _personService.EditPersonAsync(editPerson);
            await Clients.Others.SendAsync("updatePersonInfo", editPerson.PersonId, editPerson.Name,
                                                            editPerson.Salary);
        }

        public async Task DeletePerson(int personId)
        {
            var person = await _personService.GetPersonByIdAsync(personId);
            _personService.DeletePerson(person);
            await Clients.All.SendAsync("removePerson", personId);

        }
    }
}