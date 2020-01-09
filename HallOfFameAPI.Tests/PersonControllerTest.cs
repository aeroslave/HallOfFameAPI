using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using HallOfFameAPI.Controllers;
using HallOfFameAPI.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HallOfFameAPI.Tests
{
    public class PersonControllerTest
    {
        [Fact]
        public async void GetAllTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HallOfFameContext>()
            .UseInMemoryDatabase("userdbstore");

            using (var context = new HallOfFameContext(optionsBuilder.Options))
            {
                PersonController controller = new PersonController(context);
                int contextCount = await context.Person.CountAsync();

                List<Person> result = controller.Get() as List<Person>;

                Assert.Equal(result.Count.ToString(), contextCount.ToString());
            }
        }
        [Fact]
        public void GetOneTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HallOfFameContext>()
            .UseInMemoryDatabase("userdbstore");

            using (var context = new HallOfFameContext(optionsBuilder.Options))
            {
                PersonController controller = new PersonController(context);

                var result = controller.Get(1);
                var result2 = controller.Get(10);

                Assert.IsType<OkObjectResult>(result);
                Assert.IsType<NotFoundResult>(result2);
            }
        }
        [Fact]
        public async void PostTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HallOfFameContext>()
            .UseInMemoryDatabase("userdbstore");

            using (var context = new HallOfFameContext(optionsBuilder.Options))
            {
                PersonController controller = new PersonController(context);

                var result = controller.Post(new Person()
                {
                    Name = "Александр Макаров",
                    Skills = new List<Skill>() { new Skill() { Name = "Фехотование", Level = 1 } }
                });
                var lastAdded = await context.Person.LastAsync();
                var redirectToActionResult = Assert.IsType<OkObjectResult>(result);
                Person person = redirectToActionResult.Value as Person;
                Assert.Equal(lastAdded.Name, person.Name);
            }
        }
        [Fact]
        public async void PutTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HallOfFameContext>()
            .UseInMemoryDatabase("userdbstore");

            using (var context = new HallOfFameContext(optionsBuilder.Options))
            {
                PersonController controller = new PersonController(context);
                long id = 1;
                var result = controller.Put(id, new Person()
                {
                    Name = "Александр Макаров",
                    Skills = new List<Skill>() { new Skill() { Name = "Фехотование", Level = 1 } }
                });
                var updatedPerson = await context.Person.FindAsync(id);
                var redirectToActionResult = Assert.IsType<OkObjectResult>(result);
                Person person = redirectToActionResult.Value as Person;
                Assert.Equal(updatedPerson.Name, person.Name);
                Assert.Equal(updatedPerson.Skills.Count, person.Skills.Count);
            }
        }

        [Fact]
        public async void DeleteTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<HallOfFameContext>()
            .UseInMemoryDatabase("userdbstore");

            using (var context = new HallOfFameContext(optionsBuilder.Options))
            {
                PersonController controller = new PersonController(context);
                int contextCount = await context.Person.CountAsync();
                int id = 1;
                controller.Delete(id);
                var result = controller.Get(id);

                Assert.IsType<NotFoundResult>(result);
            }
        }
    }
}