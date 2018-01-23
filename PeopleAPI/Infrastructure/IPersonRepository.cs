using PeopleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleAPI.Infrastructure
{
    public interface IPersonRepository
    {
        List<PersonDTO> GetPeople();
    }
}
