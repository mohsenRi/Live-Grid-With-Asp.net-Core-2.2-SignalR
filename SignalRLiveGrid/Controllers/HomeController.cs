using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRLiveGrid.Data;
using SignalRLiveGrid.Models;
using SignalRLiveGrid.Service;

namespace SignalRLiveGrid.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonService _personService;

        public HomeController(IPersonService personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPeople() =>Json(await _personService.GetPeopleAsync());
        
        [HttpPost]
        public async Task<IActionResult> UpdatePerson(int personId, string name, decimal salary)
        {
            try
            {
                var editPerson = new Person
                {
                    PersonId = personId,
                    Name = name,
                    Salary = salary
                };
                await _personService.EditPersonAsync(editPerson);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
