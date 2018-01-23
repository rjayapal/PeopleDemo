using Microsoft.AspNetCore.Mvc;
using Moq;
using PeopleAPI.Controllers;
using PeopleAPI.Infrastructure;
using System;
using Xunit;
using System.Collections.Generic;
using System.Net;

namespace PeopleAPI.Tests
{
    public class PeopleControllerTest
    {
        [Fact]
        public void Get_ShouldReturnOkObjectResult_WhenResultIsNotNull()
        {
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(repo => repo.GetPeople()).Returns(GetPeople());
            
            var controller = new PeopleController(mockRepo.Object);

            var actionResult = controller.Get();

            Assert.IsType(typeof(OkObjectResult), actionResult);
        }

        [Fact]
        public void Get_ShouldReturnStatusCode500_WhenResultIsNull()
        {
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(repo => repo.GetPeople()).Returns((List<PersonDTO>)null);

            var controller = new PeopleController(mockRepo.Object);

            var actionResult = controller.Get();

            Assert.Equal((int)HttpStatusCode.InternalServerError, 
                            ((StatusCodeResult)actionResult).StatusCode);
        }

        private List<PersonDTO> GetPeople()
        {
            var persons = new List<PersonDTO> {new PersonDTO { Gender = "Male",
                                                               PetName = new List<string> { "Garfield"} }} ;

            return persons;
        }
    }
}
