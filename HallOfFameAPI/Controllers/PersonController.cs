using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HallOfFameAPI.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HallOfFameAPI.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonController : Controller
    {
        HallOfFameContext _context;

        public PersonController(HallOfFameContext context)
        {
            _context = context;
            if (!_context.Person.Any())
            {
                var persons = new Person[]
                {
                new Person{Name = "Алексей Терентьев"},
                new Person{Name = "Семен Семенов"},
                new Person{Name = "Иван Павлов"},
                new Person{Name = "Борис Ступин"},
                };
                foreach (var person in persons)
                {
                    _context.Person.Add(person);
                }
                _context.SaveChanges();

                var skills = new Skill[]
                {
                new Skill{Name = "ReactJS", Level = 2, PersonID = 1},
                new Skill{Name = "Angilar", Level = 3, PersonID = 1},
                new Skill{Name = "Xamarin", Level = 5, PersonID = 1},
                new Skill{Name = "Python", Level = 1, PersonID = 2},
                new Skill{Name = "JavaScript", Level = 2, PersonID = 2},
                new Skill{Name = "YellowBoy", Level = 4, PersonID = 3},
                new Skill{Name = "Java", Level = 4, PersonID = 4},
                new Skill{Name = "ASP.Net", Level = 4, PersonID = 4}
                };
                foreach (var skill in skills)
                {
                    _context.Skill.Add(skill);
                }
                _context.SaveChanges();
            }
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            var person = _context.Person
                .Include(c => c.Skills);
            return person.ToList();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var person = _context.Person
                .Include(s => s.Skills)
            .FirstOrDefault(m => m.ID == id);

            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post(Person person)
        {
            if (person == null)
            {
                ModelState.AddModelError("", "Не указаны данные для пользователя");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personToAdd = new Person() { Name = person.Name, Skills = new List<Skill>()};
            personToAdd.Skills.AddRange(person.Skills);
            _context.Person.Add(personToAdd);
            _context.SaveChanges();
            return Ok(person);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, Person person)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var personToUpdate = _context.Person
                .Include(s=>s.Skills)
                .FirstOrDefault(m => m.ID == id);

            if (personToUpdate==null)
            {
                return NotFound();
            }

            foreach (var skill in person.Skills)
            {
                if (!personToUpdate.Skills.Select(s=>s.Name).Contains(skill.Name))
                {
                    personToUpdate.Skills.Add(new Skill() { Name = skill.Name, Level = skill.Level });
                }
                else
                {
                    personToUpdate.Skills.First(s=>s.Name==skill.Name).Level = skill.Level;
                }
            }
            if (personToUpdate.Name!=person.Name)
            {
                personToUpdate.Name = person.Name;
            }
            _context.Update(personToUpdate);
            _context.SaveChanges();
            return Ok(personToUpdate);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Person person = _context.Person
                .Include(s => s.Skills)
                .SingleOrDefault(m => m.ID == id);

            if (person == null)
            {
                return NotFound();
            }
            _context.Person.Remove(person);
            _context.SaveChanges();
            return Ok(person);
        }
    }
}
